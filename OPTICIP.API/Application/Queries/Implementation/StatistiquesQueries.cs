using Dapper;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Implementation
{

    public class StatistiquesQueries : IStatistiquesQueries
    {
        private string _connectionString = string.Empty;
        private readonly IDbConnection _coreDBConnection;
        private readonly IDbConnection _appDBConnection;

        public StatistiquesQueries(string constr, IDbConnection coreDBConnection, SqlConnection appDBConnection)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
            _coreDBConnection = coreDBConnection ?? throw new ArgumentNullException(nameof(coreDBConnection));
            _appDBConnection = appDBConnection ?? throw new ArgumentNullException(nameof(appDBConnection));
        }

        public async Task<IEnumerable<CompteBancaireViewModel>> GetComptesBancairesDeclares(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string appSql = @"select *  from Compte where Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'";
                    var comptesDeclares = await connection.QueryAsync<CompteViewModel>(appSql);
                    string coreSql = "";
                    List<CompteBancaireViewModel> result = new List<CompteBancaireViewModel>();
                    CompteBancaireViewModel comptesDeclareCoreDB;

                    foreach (var comptesDeclare in comptesDeclares)
                    {

                        coreSql = @"select a.Numseq, a.Codbnq||a.codgch||'0'||a.compte||a.clerib rib,b.nom nom_compte,a.Datouv,a.Datfrm, DECODE(a.TYpe,'N','DECLARATION','M','MODIFICATION') Type_Declaration,a.Datmaj,a.Datenv "
                         + "from V_CIP1 a, V_CIP_CPT b "
                         + "where a.Compte=b.Compte and a.State = 'E' "
                         + "and a.numseq in ('" + comptesDeclare.Num_Enr + "')";

                        _coreDBConnection.Open();
                        comptesDeclareCoreDB =  _coreDBConnection.Query<CompteBancaireViewModel>(coreSql).FirstOrDefault();

                        if (comptesDeclareCoreDB !=null)
                        {
                            comptesDeclareCoreDB.datdecl = comptesDeclare.Date_Declaration;
                            comptesDeclareCoreDB.statut = comptesDeclare.Etat;
                            result.Add(comptesDeclareCoreDB);
                        }

                        _coreDBConnection.Close();

                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }              
            }
        }

        public async Task<IEnumerable<PersPhysiqueDeclareeViewModel>> GetPersPhysiquesDeclarees(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    var persPhysiquesDeclarees = await connection.QueryAsync<PersPhysiqueViewModel>(@"select *  from Pers_Physique where Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'");
                    string coreSql = "";
                    List<PersPhysiqueDeclareeViewModel> result = new List<PersPhysiqueDeclareeViewModel>();
                    PersPhysiqueDeclareeViewModel persPhysiqueDeclareCoreDB;

                    foreach (var persPhysiqueDeclaree in persPhysiquesDeclarees)
                    {
                        coreSql = @"SELECT A.Numseq, A.Codbnq||a.codgch||'0'||A.compte||A.clerib rib,b.nom nom_compte,A.NomNais,A.Prenom,A.NomMari, A.datnais, DECODE(A.TYpe,'N','DECLARATION','M','MODIFICATION') Type_Declaration ,A.Datmaj,A.Datenv "
                           + "from V_CIP2 a, V_CIP_CPT B "
                             + "WHERE A.Compte=B.Compte AND A.State='E' "
                             + "and a.numseq in ('" + persPhysiqueDeclaree.Num_Enr + "')";

                        _coreDBConnection.Open();

                        persPhysiqueDeclareCoreDB = _coreDBConnection.Query<PersPhysiqueDeclareeViewModel>(coreSql).FirstOrDefault();
                        
                        if (persPhysiqueDeclareCoreDB != null)
                        {
                            persPhysiqueDeclareCoreDB.datdecl = persPhysiqueDeclaree.Date_Declaration;
                            persPhysiqueDeclareCoreDB.statut = persPhysiqueDeclaree.Etat;
                            result.Add(persPhysiqueDeclareCoreDB);
                        }

                        _coreDBConnection.Close();

                    }
                
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<PersMoraleDeclareeViewModel>> GetPersMoralesDeclarees(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    var persMoraleDeclarees = await connection.QueryAsync<PersMoraleViewModel>(@"select *  from Pers_Morale where Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'");
                    string coreSql = "";
                    PersMoraleDeclareeViewModel persMoraleDeclareeCoreDB;
                    List<PersMoraleDeclareeViewModel> result = new List<PersMoraleDeclareeViewModel>();

                    foreach (var persMoraleDeclaree in persMoraleDeclarees)
                    {
                        coreSql = @"SELECT A.Numseq, A.Codbnq||a.codgch||'0'||A.compte||A.clerib rib,b.nom nom_compte,A.Forme,A.RCSNO, DECODE(A.TYpe,'N','DECLARATION','M','MODIFICATION') Type_Declaration ,A.Datmaj,A.Datenv "
                                  + "FROM V_CIP3 a, V_CIP_CPT B "
                                  + "WHERE A.Compte=B.Compte AND A.State='E' "
                                  + "and a.numseq in ('" + persMoraleDeclaree.Num_Enr + "')";

                        _coreDBConnection.Open();
                       
                        persMoraleDeclareeCoreDB = _coreDBConnection.Query<PersMoraleDeclareeViewModel>(coreSql).FirstOrDefault();

                        if (persMoraleDeclareeCoreDB != null)
                        {
                            persMoraleDeclareeCoreDB.datdecl = persMoraleDeclaree.Date_Declaration;
                            persMoraleDeclareeCoreDB.statut = persMoraleDeclaree.Etat;
                            result.Add(persMoraleDeclareeCoreDB);
                        }

                        _coreDBConnection.Close();
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /*
        public async Task<IEnumerable<CompteMandataireViewModel>> GetComptesMandataires(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
              
                try
                {
                    var comptesDeclares = await connection.QueryAsync<CompteViewModel>(@"select *  from Compte where Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'");
                    string coreSql = "";

                    List<CompteMandataireViewModel> result = new List<CompteMandataireViewModel>();
                    CompteMandataireViewModel comptesDeclareCoreDB;

                    foreach (var comptesDeclare in comptesDeclares)
                    {
                        coreSql = @"select A.Numseq, A.Codbnq || a.codgch || '0' || A.compte || A.clerib rib,b.nom nom_compte, A.Datouv,A.Datfrm, DECODE(A.TYpe, 'N', 'DECLARATION', 'M', 'MODIFICATION') Type_Declaration ,A.Datmaj,A.Datenv "
                          + "FROM V_CIP1 a, V_CIP_CPT B "
                          + "WHERE A.Compte = B.Compte AND A.State = 'E' AND NOT EXISTS(SELECT P.Numseq FROM V_CIP2 P WHERE P.State = 'E' AND A.Compte = P.Compte UNION SELECT M.Numseq FROM V_CIP3 M WHERE M.State = 'E'  AND A.Compte = M.Compte) "
                          + "and a.numseq in ('" + comptesDeclare.Num_Enr + "')";

                        _coreDBConnection.Open();

                        comptesDeclareCoreDB = _coreDBConnection.Query<CompteMandataireViewModel>(coreSql).FirstOrDefault();
                      
                        if (comptesDeclareCoreDB != null)
                        {
                            comptesDeclareCoreDB.datdecl = comptesDeclare.Date_Declaration;
                            comptesDeclareCoreDB.statut = comptesDeclare.Etat;
                            result.Add(comptesDeclareCoreDB);
                        }         

                        _coreDBConnection.Close();
                    }
                          
                   return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        */
        public async Task<IEnumerable<CompteMandataireViewModel>> GetComptesMandataires(String dateDebut, String dateFin)
        {
            try
            {
                string coreSql = "";

                coreSql = @"select A.Numseq, A.Codbnq || a.codgch || '0' || A.compte || A.clerib rib,b.nom nom_compte, A.Datouv,A.Datfrm, DECODE(A.TYpe, 'N', 'DECLARATION', 'M', 'MODIFICATION') Type_Declaration ,A.Datmaj,A.Datenv "
                     + "FROM V_CIP1 a, V_CIP_CPT B "
                     + "WHERE A.Compte = B.Compte AND A.State = 'E' AND NOT EXISTS(SELECT P.Numseq FROM V_CIP2 P WHERE P.State = 'E' AND A.Compte = P.Compte UNION SELECT M.Numseq FROM V_CIP3 M WHERE M.State = 'E'  AND A.Compte = M.Compte) ";

                _coreDBConnection.Open();

                var result = await _coreDBConnection.QueryAsync<CompteMandataireViewModel>(coreSql);

                _coreDBConnection.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<IncidentDePaiementChequeViewModel>> GetIncidentsDePaiementCheques(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    var incidentsDeclares = await connection.QueryAsync<IncidentsChequesViewModel>(@"select *  from IncidentCheque where Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'");
                    string coreSql = "";
                    List<IncidentDePaiementChequeViewModel> result = new List<IncidentDePaiementChequeViewModel>();
                    IncidentDePaiementChequeViewModel incidentDeclareCoreDB;

                    foreach (var incidentDeclare in incidentsDeclares)
                    {
                        coreSql = @"SELECT A.Numseq, A.Codbnq||a.codgch||'0'||A.compte||A.clerib rib,b.nom,a.chqref Nocheque,a.mntfrf Montant,a.datchq Date_Emis,A.Datinc, a.datpre Date_Presentation, "
                                   +@"DECODE(A.typinc,'0','AVERTISSEMENT','1','INJONCTION','INFRACTION')TYPE_INCIDENT, "
                                   +@"DECODE (A.TypINC,'0','AVERTISSEMENT','1','INCIDENT SIMPLE','2','INTERDIT BANCAIRE CHEQUE PAYE','3','INTERDIT BANCAIRE CHEQUE IMPAYE', '4','INTERDIT JUDICIAIRE CHEQUE PAYE','5','INTERDIT JUDICIAIRE CHEQUE IMPAYE','6','COMPTE CLOTURE') DETAIL_INCIDENT, "
                                   +@"DECODE(A.TYpe,'N','DECLARATION','M','MODIFICATION') Type_Declaration,A.Datmaj,A.Datenv "
                                    + @"FROM V_CIP5 a, V_CIP_CPT B "
                                    + @"WHERE A.Compte=B.Compte and a.datreg is null and A.State='E' "
                                 + @"and a.numseq in ('" + incidentDeclare.Num_Enr + "')";

                        _coreDBConnection.Open();
                        incidentDeclareCoreDB = _coreDBConnection.Query<IncidentDePaiementChequeViewModel>(coreSql).FirstOrDefault();

                        if (incidentDeclareCoreDB != null)
                        {
                            incidentDeclareCoreDB.statut = incidentDeclare.Etat;
                            incidentDeclareCoreDB.datdecl = incidentDeclare.Date_Declaration;
                            result.Add(incidentDeclareCoreDB);
                        }

                        _coreDBConnection.Close();
                    }               

                 
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<IncidentDePaiementEffetViewModel>> GetIncidentsDePaiementEffets(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    var incidentsDeclares = await connection.QueryAsync<IncidentsEffetsViewModel>(@"select *  from IncidentEffet where Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'");
                    string coreSql = "";
                    List<IncidentDePaiementEffetViewModel> result = new List<IncidentDePaiementEffetViewModel>();
                    IncidentDePaiementEffetViewModel incidentDeclareCoreDB;

                    foreach (var incidentDeclare in incidentsDeclares)
                    {
                        coreSql = @"SELECT A.Numseq, A.Codbnq||a.codgch||'0'||A.compte||A.clerib rib,b.nom, a.mnt,DECODE(A.Motif,'0','DEFAUT PROVISION','1','INSUFFISANCE PROVISION','2','ABSENCE DOMICILIATION') MOtif_Rejet, a.datech,DECODE(A.TYpe,'N','DECLARATION','M','MODIFICATION') Type_Declaration,A.Datmaj,A.Datenv "
                             + "FROM V_CIP7 a, V_CIP_CPT B "
                             + "WHERE A.Compte=B.Compte AND A.State='E' and a.numseq in ('" + incidentDeclare.Num_Enr + "')";

                        _coreDBConnection.Open();

                        incidentDeclareCoreDB = _coreDBConnection.Query<IncidentDePaiementEffetViewModel>(coreSql).FirstOrDefault();

                        if (incidentDeclareCoreDB != null)
                        {
                            incidentDeclareCoreDB.statut = incidentDeclare.Etat;
                            incidentDeclareCoreDB.datdecl = incidentDeclare.Date_Declaration;
                            result.Add(incidentDeclareCoreDB);
                        }

                        _coreDBConnection.Close();
                    }
                   
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /*
        public async Task<IEnumerable<DeclarationSusmentionneeViewModel>> GetDeclarationsSusmentionnees(String dateDebut, String dateFin)
        {
            throw new NotImplementedException();
        }*/

        public async Task<IEnumerable<IncidentDePaiementRegulariseViewModel>> GetIncidentsDePaiementRegularises(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    var incidentsPaiementRegularises = await connection.QueryAsync<IncidentsChequesViewModel>(@"select *  from IncidentCheque where Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'");
                    string coreSql = "";
                    List<IncidentDePaiementRegulariseViewModel> result = new List<IncidentDePaiementRegulariseViewModel>();
                    IncidentDePaiementRegulariseViewModel incidentPaiementRegulCoreDB;

                    foreach (var incidentPaiementRegularise in incidentsPaiementRegularises)
                    {

                        coreSql = @"SELECT A.Numseq, A.Codbnq || a.codgch || '0' || A.compte || A.clerib rib,b.nom,a.chqref Nocheque, a.mntfrf Montant, a.datchq Date_Emis, A.Datinc, a.datpre Date_Presentation, A.datreg, "
                                    +@"DECODE(A.typinc, '0', 'AVERTISSEMENT', '1', 'INJONCTION', 'INFRACTION')TYPE_INCIDENT, "
                                    +@"DECODE(A.TypINC, '0', 'AVERTISSEMENT', '1', 'INCIDENT SIMPLE', '2', 'INTERDIT BANCAIRE CHEQUE PAYE', '3', 'INTERDIT BANCAIRE CHEQUE IMPAYE', '4', 'INTERDIT JUDICIAIRE CHEQUE PAYE', '5', 'INTERDIT JUDICIAIRE CHEQUE IMPAYE', '6', 'COMPTE CLOTURE') DETAIL_INCIDENT, "
                                    +@"DECODE(A.TYpe, 'N', 'DECLARATION', 'M', 'MODIFICATION') Type_Declaration "
                                    + @"FROM V_CIP5 a, V_CIP_CPT B "
                                    + @"WHERE  A.Compte=B.Compte AND a.datreg is not null AND A.State='E' "
                                    + @"and a.numseq in ('" + incidentPaiementRegularise.Num_Enr + "')"; ;

                        _coreDBConnection.Open();
                        incidentPaiementRegulCoreDB = _coreDBConnection.Query<IncidentDePaiementRegulariseViewModel>(coreSql).FirstOrDefault();

                        if (incidentPaiementRegulCoreDB != null)
                        {
                            incidentPaiementRegulCoreDB.statut = incidentPaiementRegularise.Etat;
                            incidentPaiementRegulCoreDB.datdecl = incidentPaiementRegularise.Date_Declaration;
                            result.Add(incidentPaiementRegulCoreDB);
                        }

                        _coreDBConnection.Close();
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<CompteEnInterditBancaireViewModel>> GetComptesEnInterditBancaire(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    var comptesEnInterditBancaire = await connection.QueryAsync<IncidentsChequesViewModel>(@"select *  from IncidentCheque where Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'");
                    string coreSql = "";
                    List<CompteEnInterditBancaireViewModel> result = new List<CompteEnInterditBancaireViewModel>();
                    CompteEnInterditBancaireViewModel compteEnInterditBancaireCoreDB;

                    foreach (var compteEnInterditBancaire in comptesEnInterditBancaire)
                    {

                        coreSql = @"SELECT A.Numseq, A.Codbnq || a.codgch || '0' || A.compte || A.clerib rib,b.nom,a.chqref Nocheque, a.mntfrf Montant, a.datchq Date_Emis, A.Datinc, a.datpre Date_Presentation, "
                                  +@"DECODE(A.typinc, '0', 'AVERTISSEMENT', '1', 'INJONCTION', 'INFRACTION')TYPE_INCIDENT, "
                                  +@"DECODE(A.TypINC, '0', 'AVERTISSEMENT', '1', 'INCIDENT SIMPLE', '2', 'INTERDIT BANCAIRE CHEQUE PAYE', '3', 'INTERDIT BANCAIRE CHEQUE IMPAYE', '4', 'INTERDIT JUDICIAIRE CHEQUE PAYE', '5', 'INTERDIT JUDICIAIRE CHEQUE IMPAYE', '6', 'COMPTE CLOTURE') DETAIL_INCIDENT, "
                                  +@"DECODE(A.TYpe, 'N', 'DECLARATION', 'M', 'MODIFICATION') Type_Declaration,A.Datmaj,A.Datenv "
                                    + @"FROM V_CIP5 a, V_CIP_CPT B "
                                    + @"WHERE A.Compte=B.Compte AND a.datreg is null AND a.typinc IN ('2','3') AND A.State='E' "
                                    + @"and a.numseq in ('" + compteEnInterditBancaire.Num_Enr + "')";

                        _coreDBConnection.Open();

                        compteEnInterditBancaireCoreDB = _coreDBConnection.Query<CompteEnInterditBancaireViewModel>(coreSql).FirstOrDefault();

                        if (compteEnInterditBancaireCoreDB != null)
                        { 
                            compteEnInterditBancaireCoreDB.statut = compteEnInterditBancaire.Etat;
                            compteEnInterditBancaireCoreDB.datdecl = compteEnInterditBancaire.Date_Declaration;
                            result.Add(compteEnInterditBancaireCoreDB);
                        }

                        _coreDBConnection.Close();
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<CompteEnInterditionLeveeViewModel>> GetComptesEnInterditionLevee(String dateDebut, String dateFin)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    var comptesEnInterditionLevee = await connection.QueryAsync<IncidentsChequesViewModel>(@"select *  from IncidentCheque where Date_Declaration between '" + dateDebut + "' and '" + dateFin + "'");
                    string coreSql = "";
                    List<CompteEnInterditionLeveeViewModel> result = new List<CompteEnInterditionLeveeViewModel>();
                    CompteEnInterditionLeveeViewModel compteEnInterditionLeveeCoreDB;

                    foreach (var compteEnInterditionLevee in comptesEnInterditionLevee)
                    {

                        coreSql = @"SELECT A.Numseq, A.Codbnq || a.codgch || '0' || A.compte || A.clerib rib,b.nom,a.chqref Nocheque, a.mntfrf Montant, a.datchq Date_Emis, A.Datinc, a.datpre Date_Presentation, A.datreg, "
                                   +@"DECODE(A.typinc, '0', 'AVERTISSEMENT', '1', 'INJONCTION', 'INFRACTION')TYPE_INCIDENT, "
                                   +@"DECODE(A.TypINC, '0', 'AVERTISSEMENT', '1', 'INCIDENT SIMPLE', '2', 'INTERDIT BANCAIRE CHEQUE PAYE', '3', 'INTERDIT BANCAIRE CHEQUE IMPAYE', '4', 'INTERDIT JUDICIAIRE CHEQUE PAYE', '5', 'INTERDIT JUDICIAIRE CHEQUE IMPAYE', '6', 'COMPTE CLOTURE') DETAIL_INCIDENT, "
                                   +@"DECODE(A.TYpe, 'N', 'DECLARATION', 'M', 'MODIFICATION') Type_Declaration "
                                      + @"FROM V_CIP5 a, V_CIP_CPT B "
                                     + @"WHERE A.Compte=B.Compte and a.datreg is not null AND A.typinc in ('2','3') AND A.State='E' "
                                    + @"and a.numseq in ('" + compteEnInterditionLevee.Num_Enr + "')";

                        _coreDBConnection.Open();

                        compteEnInterditionLeveeCoreDB = _coreDBConnection.Query<CompteEnInterditionLeveeViewModel>(coreSql).FirstOrDefault();

                        if (compteEnInterditionLeveeCoreDB != null)
                        {
                            compteEnInterditionLeveeCoreDB.statut = compteEnInterditionLevee.Etat;
                            compteEnInterditionLeveeCoreDB.datdecl = compteEnInterditionLevee.Date_Declaration;
                            result.Add(compteEnInterditionLeveeCoreDB);
                        }

                        _coreDBConnection.Close();
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
