using Dapper;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using OPTICIP.Entities.DataEntities;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Implementation
{
    public class DeclarationQueries : IDeclarationQueries
    {
        private string _connectionString = string.Empty;
        private readonly IDbConnection _coreDBConnection;
        private readonly IDbConnection _appDBConnection;
        private readonly IRepositoryFactory _repositoryFactory;

        public DeclarationQueries(string constr, IDbConnection coreDBConnection, SqlConnection appDBConnection, IRepositoryFactory repositoryFactory)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
            _coreDBConnection = coreDBConnection ?? throw new ArgumentNullException(nameof(coreDBConnection));
            _appDBConnection = appDBConnection ?? throw new ArgumentNullException(nameof(appDBConnection));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        public async Task<IEnumerable<CompteViewModel>> GetComptesAsync(string agence, string etat="")
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                //V_ListeComptes
                connection.Open();
                try
                {
                    if (string.IsNullOrEmpty(agence))
                    {
                        if (!string.IsNullOrEmpty(etat))
                        {
                            if (etat == PreparationQueries._codeToutCompte)
                                return await connection.QueryAsync<CompteViewModel>(@"select * from V_ListeComptes order by Date_Ouverture desc");
                            else
                                return await connection.QueryAsync<CompteViewModel>(@"select * from V_ListeComptes where Etat=@etat order by Date_Ouverture desc", new { etat });
                        }
                        return await connection.QueryAsync<CompteViewModel>(@"select * from V_ListeComptesRetires order by Date_Ouverture desc");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(etat))
                        {
                            if (etat == PreparationQueries._codeToutCompte)
                                return await connection.QueryAsync<CompteViewModel>(@"select * from V_ListeComptes where substring(RIB, 6, 5) = @agence order by Date_Ouverture desc", new { agence });
                            else
                                return await connection.QueryAsync<CompteViewModel>(@"select * from V_ListeComptes where substring(RIB, 6, 5) = @agence and etat=@etat order by Date_Ouverture desc", new { agence, etat });
                        }
                        return await connection.QueryAsync<CompteViewModel>(@"select * from V_ListeComptesRetires where substring(RIB, 6, 5) = @agence order by Date_Ouverture desc", new { agence });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }              
            }
        }

        public async Task<IEnumerable<PersPhysiqueViewModel>> GetPersPhysiquesAsync(string agence, string etat = "")
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                //V_ListePersPhysiques
                connection.Open();
                try
                {
                    if (string.IsNullOrEmpty(agence))
                    {
                        if (!string.IsNullOrEmpty(etat))
                        {
                            if (etat == PreparationQueries._codeToutCompte)
                                return await connection.QueryAsync<PersPhysiqueViewModel>(@"select * from V_ListePersPhysiques");
                            else
                                return await connection.QueryAsync<PersPhysiqueViewModel>(@"select * from V_ListePersPhysiques where Etat=@etat ", new { etat });

                        }
                        return await connection.QueryAsync<PersPhysiqueViewModel>(@"select * from V_ListePersPhysiquesRetires");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(etat))
                        {
                            if (etat == PreparationQueries._codeToutCompte)
                                return await connection.QueryAsync<PersPhysiqueViewModel>(@"select * from V_ListePersPhysiques where substring(RIB, 6, 5) = @agence", new { agence });
                            else
                                return await connection.QueryAsync<PersPhysiqueViewModel>(@"select * from V_ListePersPhysiques where substring(RIB, 6, 5) = @agence and etat=@etat", new { agence, etat });
                        }
                        return await connection.QueryAsync<PersPhysiqueViewModel>(@"select * from V_ListePersPhysiquesRetires where substring(RIB, 6, 5) = @agence", new { agence });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<PersPhysiqueErreurViewModel>> GetPersPhysiquesErreursAsync(string agence)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    if (string.IsNullOrEmpty(agence))
                    {
                        return await connection.QueryAsync<PersPhysiqueErreurViewModel>(@"select * from V_ListePersPhysiqueErreurs");
                    }
                    else
                    {
                        return await connection.QueryAsync<PersPhysiqueErreurViewModel>(@"select * from V_ListePersPhysiqueErreurs where substring(RIB, 6, 5) = @agence", new { agence });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<PersMoraleViewModel>> GetPersMoralesAsync(string agence, string etat = "")
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    if (string.IsNullOrEmpty(agence))
                    {
                        if (!string.IsNullOrEmpty(etat))
                        {
                            if (etat == PreparationQueries._codeToutCompte)
                                return await connection.QueryAsync<PersMoraleViewModel>(@"select * from V_ListePersMorales");
                        else
                                return await connection.QueryAsync<PersMoraleViewModel>(@"select * from V_ListePersMorales where Etat=@etat ", new { etat });
                        }
                        return await connection.QueryAsync<PersMoraleViewModel>(@"select * from V_ListePersMoralesRetires");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(etat))
                        {
                            if (etat == PreparationQueries._codeToutCompte)
                                return await connection.QueryAsync<PersMoraleViewModel>(@"select * from V_ListePersMorales where substring(RIB, 6, 5) = @agence", new { agence });
                        else
                                return await connection.QueryAsync<PersMoraleViewModel>(@"select * from V_ListePersMorales where substring(RIB, 6, 5) = @agence and etat=@etat", new { agence, etat });
                        }
                        return await connection.QueryAsync<PersMoraleViewModel>(@"select * from V_ListePersMoralesRetires where substring(RIB, 6, 5) = @agence", new { agence });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<PersMoraleErreurViewModel>> GetPersMoralesErreursAsync(string agence)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    if (string.IsNullOrEmpty(agence))
                    {
                        return await connection.QueryAsync<PersMoraleErreurViewModel>(@"select * from V_ListePersMoraleErreurs");
                    }
                    else
                    {
                        return await connection.QueryAsync<PersMoraleErreurViewModel>(@"select * from V_ListePersMoraleErreurs where substring(RIB, 6, 5) = @agence", new { agence });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<CartesViewModel>> GetCartesAsync(string agence, string etat = "")
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    if (string.IsNullOrEmpty(agence))
                    {
                        if (!string.IsNullOrEmpty(etat))
                            return await connection.QueryAsync<CartesViewModel>(@"select * from V_ListeCartes where Etat=@etat ", new { etat });
                        return await connection.QueryAsync<CartesViewModel>(@"select * from V_ListeCartesRetires");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(etat))
                            return await connection.QueryAsync<CartesViewModel>(@"select * from V_ListeCartes where substring(RIB, 6, 5) = @agence and etat=@etat", new { agence, etat });
                        return await connection.QueryAsync<CartesViewModel>(@"select * from V_ListeCartesRetires where substring(RIB, 6, 5) = @agence", new { agence });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public async Task<IEnumerable<ErreurEnregistrementViewModel>> GetListErreurs(string enregistrementID, string enregistrementTable)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    return await connection.QueryAsync<ErreurEnregistrementViewModel>(@"select * from ErreurEnregistrement where EnregistrementID=@enregistrementID and EnregistrementTable=@enregistrementTable ", new { enregistrementID, enregistrementTable });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<IncidentsChequesViewModel>> GetIncidentsChequesAsync(string agence, string etat = "")
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                //V_ListeIncidentsCheques
                connection.Open();
                try
                {
                    if (string.IsNullOrEmpty(agence))
                    {
                        if (!string.IsNullOrEmpty(etat))
                        {
                            if (etat == PreparationQueries._codeToutCompte)
                                return await connection.QueryAsync<IncidentsChequesViewModel>(@"select * from V_ListeIncidentsCheques order by Date_Emission desc");
                        else
                                return await connection.QueryAsync<IncidentsChequesViewModel>(@"select * from V_ListeIncidentsCheques where Etat=@etat order by Date_Emission desc", new { etat });
                        }
                        return await connection.QueryAsync<IncidentsChequesViewModel>(@"select * from V_ListeIncidentsChequesRetires order by Date_Emission desc");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(etat))
                        {
                            if (etat == PreparationQueries._codeToutCompte)
                                return await connection.QueryAsync<IncidentsChequesViewModel>(@"select * from V_ListeIncidentsCheques where substring(RIB, 6, 5) = @agence order by Date_Emission desc", new { agence });
                        else
                                return await connection.QueryAsync<IncidentsChequesViewModel>(@"select * from V_ListeIncidentsCheques where substring(RIB, 6, 5) = @agence and etat=@etat order by Date_Emission desc", new { agence, etat });
                        }
                        return await connection.QueryAsync<IncidentsChequesViewModel>(@"select * from V_ListeIncidentsChequesRetires where substring(RIB, 6, 5) = @agence order by Date_Emission desc", new { agence });
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<ChequesIrreguliersViewModel>> GetChequesIrreguliersAsync(string agence, string etat = "")
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                //V_ListeChequesIrreguliers
                connection.Open();
                try
                {
                    if (string.IsNullOrEmpty(agence))
                    {
                        if (!string.IsNullOrEmpty(etat))
                        {
                            if (etat == PreparationQueries._codeToutCompte)
                                return await connection.QueryAsync<ChequesIrreguliersViewModel>(@"select * from V_ListeChequesIrreguliers order by Date_Opposition desc");
                        else
                                return await connection.QueryAsync<ChequesIrreguliersViewModel>(@"select * from V_ListeChequesIrreguliers where Etat=@etat order by Date_Opposition desc", new { etat });
                        }
                        return await connection.QueryAsync<ChequesIrreguliersViewModel>(@"select * from V_ListeChequesIrreguliersRetires  by Date_Opposition desc");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(etat))
                        {
                            if (etat == PreparationQueries._codeToutCompte)
                                return await connection.QueryAsync<ChequesIrreguliersViewModel>(@"select * from V_ListeChequesIrreguliers where substring(RIB, 6, 5) = @agence order by Date_Opposition desc", new { agence });
                        else
                                return await connection.QueryAsync<ChequesIrreguliersViewModel>(@"select * from V_ListeChequesIrreguliers where substring(RIB, 6, 5) = @agence and etat=@etat order by Date_Opposition desc", new { agence, etat });
                        }
                        return await connection.QueryAsync<ChequesIrreguliersViewModel>(@"select * from V_ListeChequesIrreguliersRetires where substring(RIB, 6, 5) = @agence order by Date_Opposition desc", new { agence });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public async Task<IEnumerable<IncidentsEffetsViewModel>> GetIncidentsEffetsAsync(string agence, string etat = "")
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                //V_ListeIncidentsEffets
                connection.Open();
                try
                {
                    if (string.IsNullOrEmpty(agence))
                    {
                        if (!string.IsNullOrEmpty(etat))
                        {
                            if (etat == PreparationQueries._codeToutCompte)
                                return await connection.QueryAsync<IncidentsEffetsViewModel>(@"select * from V_ListeIncidentsEffets order by Date_Refus_Paiement desc");
                            else
                                return await connection.QueryAsync<IncidentsEffetsViewModel>(@"select * from V_ListeIncidentsEffets where Etat=@etat order by Date_Refus_Paiement desc", new { etat });
                        }
                        return await connection.QueryAsync<IncidentsEffetsViewModel>(@"select * from V_ListeIncidentsEffetsRetires order by Date_Refus_Paiement desc");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(etat))
                        {
                            if (etat == PreparationQueries._codeToutCompte)
                                return await connection.QueryAsync<IncidentsEffetsViewModel>(@"select * from V_ListeIncidentsEffets where substring(RIB, 6, 5) = @agence order by Date_Refus_Paiement desc", new { agence });
                        else
                                return await connection.QueryAsync<IncidentsEffetsViewModel>(@"select * from V_ListeIncidentsEffets where substring(RIB, 6, 5) = @agence and etat=@etat order by Date_Refus_Paiement desc", new { agence, etat });
                        }
                        return await connection.QueryAsync<IncidentsEffetsViewModel>(@"select * from V_ListeIncidentsEffetsRetires where substring(RIB, 6, 5) = @agence order by Date_Refus_Paiement desc", new { agence });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }   

        public async Task<IEnumerable<DeclarationsViewModel>> GetDeclarationsAsync(String Agences, String NbreCompte, bool decInitiale=false)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<DeclarationsViewModel>(@"SP_DONNEES_A_DECLARER", new { DateDeclaration = DateTime.UtcNow.Date, Nombre_Compte_CIP = int.Parse(NbreCompte), Agence=Agences, declarationInitiale = decInitiale == true ? "1":"0" }, commandType: CommandType.StoredProcedure);
            }
        }

        public int GetNbreCompteETC(String Agence, int bInitialisation)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    return connection.QueryFirstOrDefault<int>(@$"select dbo.SP_CALCUL_NBRE_CPT_ETC('{Agence}', {bInitialisation})");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<DeclarationsViewModel> GetDeclarationsSync(String Agences, String NbreCompte, bool decInitiale=false)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

              //  return connection.Query<DeclarationsViewModel>(@"select texte as data from v_donnees_a_declarer order by ordre");
                return connection.Query<DeclarationsViewModel>(@"SP_DONNEES_A_DECLARER", new { DateDeclaration = DateTime.UtcNow.Date, Nombre_Compte_CIP = int.Parse(NbreCompte), Agence=Agences, declarationInitiale = decInitiale == true ? "1":"0" }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<FileDeclarationInfoViewModel> GetFileDeclarationInfoAsync()
        {
            FileDeclarationInfoViewModel file = new FileDeclarationInfoViewModel();

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var result = await connection.QueryAsync<FileDeclarationInfoViewModel>(@"SP_GES_FILENAME", new { DateJour = DateTime.UtcNow.Date }, commandType: CommandType.StoredProcedure);
                    return result.FirstOrDefault();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
            }
        }

        public async Task<FileDeclarationInfoViewModel> PostHistorisationDeclarationInfoAsync(String FileName,String Nbre_Compte_CIP,  Guid DeclarePar, String Agences, String ZipFolderName="")
        {
            FileDeclarationInfoViewModel file = new FileDeclarationInfoViewModel();
            Agences = String.IsNullOrEmpty(Agences) ? "" : Agences;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<FileDeclarationInfoViewModel>(@"SP_HISTORISATION_DECLARATION", new { DateDeclaration = DateTime.UtcNow.Date, NOM_FICHIER = FileName, Nombre_Compte_CIP = Nbre_Compte_CIP, DECLARE_PAR = DeclarePar.ToString(), Agence = Agences, ZipFolder = ZipFolderName }, commandType: CommandType.StoredProcedure);
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex )
            {
                throw ex;
            }
           
        }

        public async Task<IEnumerable<HistorisationDeclarationsViewModel>> GetListHistorisationDeclarationInfoSync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return await connection.QueryAsync<HistorisationDeclarationsViewModel>(@"select * from V_ListeHistoDeclaration order by DateDeclaration desc");
            }
        }

        public  IEnumerable<HistorisationDeclarationsViewModel> GetListHistorisationDeclarationInfo(String Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return  connection.Query<HistorisationDeclarationsViewModel>(@"select * from V_ListeHistoDeclaration where Id=@Id order by DateDeclaration desc", new { Id } );
            }
        }

        public IEnumerable<DeclarationsViewModel> GetDetailListHistorisationDeclarationInfo(String Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                return connection.Query<DeclarationsViewModel>(@"select Ordre as Numero, Text as Data from V_Donnees_Declarees where Id_Declaration=@Id order by Ordre", new { Id });
            }
        }

        private UsersViewModel MapUsersItems(dynamic result)
        {
            var user = new UsersViewModel();

            return user;
        }

        public async Task<IEnumerable<CompteErreurRetourViewModel>> GetComptesDeclaresAsync(string idFichierAller, string agence, string etat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string sql, tableSource = "Compte", vueSource = "V_ListeComptesErreursRetour";

                    if (String.IsNullOrEmpty(agence))
                    {
                        if (String.IsNullOrEmpty(etat))
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" 
                                + idFichierAller + "' and tablesource = '" + tableSource + "') order by Date_Ouverture desc";
                        }
                        else
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                "' and tablesource = '" + tableSource  + "') and etat = '" + etat + "' order by Date_Ouverture desc";
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(etat))
                        {
                            sql = "select * from " + vueSource +  " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                 "' and tablesource = '" + tableSource + "') and substring(RIB, 6, 5) = '" + agence + "' order by Date_Ouverture desc";
                        }
                        else
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                "' and tablesource = '" + tableSource  + "') and substring(RIB, 6, 5) = '" + agence + "' and etat = '" + etat + "' order by Date_Ouverture desc";
                        }
                    }

                     return await connection.QueryAsync<CompteErreurRetourViewModel>(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<PersPhysiqueErreurRetourViewModel>> GetPersPhysiquesDeclaresAsync(string idFichierAller, string agence, string etat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                try
                {
                    string sql, tableSource = "Pers_Physique", vueSource = "V_ListePersPhysiquesErreursRetour";

                    if (String.IsNullOrEmpty(agence))
                    {
                        if (String.IsNullOrEmpty(etat))
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '"
                                + idFichierAller + "' and tablesource = '" + tableSource + "')";
                        }
                        else
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                "' and tablesource = '" + tableSource + "') and etat = '" + etat + "'";
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(etat))
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                 "' and tablesource = '" + tableSource + "') and substring(RIB, 6, 5) = '" + agence + "'";
                        }
                        else
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                "' and tablesource = '" + tableSource + "') and substring(RIB, 6, 5) = '" + agence + "' and etat = '" + etat + "'";
                        }
                    }

                    return await connection.QueryAsync<PersPhysiqueErreurRetourViewModel>(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<PersMoraleErreurRetourViewModel>> GetPersMoralesDeclaresAsync(string idFichierAller, string agence, string etat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string sql, tableSource = "Pers_Morale", vueSource = "V_ListePersMoralesErreursRetour";

                    if (String.IsNullOrEmpty(agence))
                    {
                        if (String.IsNullOrEmpty(etat))
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '"
                                + idFichierAller + "' and tablesource = '" + tableSource + "')";
                        }
                        else
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                "' and tablesource = '" + tableSource + "') and etat = '" + etat + "'";
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(etat))
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                 "' and tablesource = '" + tableSource + "') and substring(RIB, 6, 5) = '" + agence + "'";
                        }
                        else
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                "' and tablesource = '" + tableSource + "') and substring(RIB, 6, 5) = '" + agence + "' and etat = '" + etat + "'";
                        }
                    }

                    return await connection.QueryAsync<PersMoraleErreurRetourViewModel>(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<IncidentsChequesErreurRetourViewModel>> GetIncidentsChequesDeclaresAsync(string idFichierAller, string agence, string etat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string sql, tableSource = "IncidentCheque", vueSource = "V_ListeIncidentsChequesErreursRetour";

                    if (String.IsNullOrEmpty(agence))
                    {
                        if (String.IsNullOrEmpty(etat))
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '"
                                + idFichierAller + "' and tablesource = '" + tableSource + "') order by Date_Refus_Paiement desc";
                        }
                        else
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                "' and tablesource = '" + tableSource + "') and etat = '" + etat + "' order by Date_Refus_Paiement desc";
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(etat))
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                 "' and tablesource = '" + tableSource + "') and substring(RIB, 6, 5) = '" + agence + "' order by Date_Refus_Paiement desc";
                        }
                        else
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                "' and tablesource = '" + tableSource + "') and substring(RIB, 6, 5) = '" + agence + "' and etat = '" + etat + "' order by Date_Refus_Paiement desc";
                        }
                    }

                    return await connection.QueryAsync<IncidentsChequesErreurRetourViewModel>(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<ChequesIrreguliersErreurRetourViewModel>> GetChequesIrreguliersDeclaresAsync(string idFichierAller, string agence, string etat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string sql, tableSource = "ChequeIrregulier", vueSource = "V_ListeChequesIrreguliersErreursRetour";

                    if (String.IsNullOrEmpty(agence))
                    {
                        if (String.IsNullOrEmpty(etat))
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '"
                                + idFichierAller + "' and tablesource = '" + tableSource + "') order by Date_Opposition desc";
                        }
                        else
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                "' and tablesource = '" + tableSource + "') and etat = '" + etat + "' order by Date_Opposition desc";
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(etat))
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                 "' and tablesource = '" + tableSource + "') and substring(RIB, 6, 5) = '" + agence + "' order by Date_Opposition desc";
                        }
                        else
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                "' and tablesource = '" + tableSource + "') and substring(RIB, 6, 5) = '" + agence + "' and etat = '" + etat + "' order by Date_Opposition desc";
                        }
                    }

                    return await connection.QueryAsync<ChequesIrreguliersErreurRetourViewModel>(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        
        public async Task<IEnumerable<IncidentsEffetsErreurRetourViewModel>> GetIncidentsEffetsDeclaresAsync(string idFichierAller, string agence, string etat)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string sql, tableSource = "IncidentEffet", vueSource = "V_ListeIncidentsEffetsErreursRetour";

                    if (String.IsNullOrEmpty(agence))
                    {
                        if (String.IsNullOrEmpty(etat))
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '"
                                + idFichierAller + "' and tablesource = '" + tableSource + "') order by Date_Refus_Paiement desc";
                        }
                        else
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                "' and tablesource = '" + tableSource + "') and etat = '" + etat + "' order by Date_Refus_Paiement desc";
                        }
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(etat))
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                 "' and tablesource = '" + tableSource + "') and substring(RIB, 6, 5) = '" + agence + "' order by Date_Refus_Paiement desc";
                        }
                        else
                        {
                            sql = "select * from " + vueSource + " where id in (select recordid from historique_declarations where id_declaration = '" + idFichierAller +
                                "' and tablesource = '" + tableSource + "') and substring(RIB, 6, 5) = '" + agence + "' and etat = '" + etat + "' order by Date_Refus_Paiement desc";
                        }
                    }

                    return await connection.QueryAsync<IncidentsEffetsErreurRetourViewModel>(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<TraitementAllerViewModel> GetResumeTraitementAllerAsync(string idFichierAller)
        {
            TraitementAllerViewModel traitementAllerViewModel = new TraitementAllerViewModel();

            List<CompteErreurRetourViewModel> comptesDeclares;
            List<PersPhysiqueErreurRetourViewModel> persPhysiquesDeclares;
            List<PersMoraleErreurRetourViewModel> persMoralesDeclares;

            List<IncidentsChequesErreurRetourViewModel> incidentsChequesDeclares;
            List<ChequesIrreguliersErreurRetourViewModel> chequesIrreguliersDeclares;
            List<IncidentsEffetsErreurRetourViewModel> incidentsEffetsDeclares;

            try
            {
                comptesDeclares = this.GetComptesDeclaresAsync(idFichierAller, "", "").Result.ToList();
                persPhysiquesDeclares = this.GetPersPhysiquesDeclaresAsync(idFichierAller, "", "").Result.ToList();
                persMoralesDeclares = this.GetPersMoralesDeclaresAsync(idFichierAller, "", "").Result.ToList();

                incidentsChequesDeclares = this.GetIncidentsChequesDeclaresAsync(idFichierAller, "", "").Result.ToList();
                chequesIrreguliersDeclares = this.GetChequesIrreguliersDeclaresAsync(idFichierAller, "", "").Result.ToList();
                incidentsEffetsDeclares = this.GetIncidentsEffetsDeclaresAsync(idFichierAller, "", "").Result.ToList();

                traitementAllerViewModel.ComptesDeclaresAcceptes = comptesDeclares.Where(c => c.Code == "D1" && c.Etat == "D").Count();
                traitementAllerViewModel.ComptesDeclaresRejetes = comptesDeclares.Where(c => c.Code == "D1" && c.Etat == "R").Count();
                traitementAllerViewModel.ComptesModifiesAcceptes = comptesDeclares.Where(c => c.Code == "M1" && c.Etat == "D").Count();
                traitementAllerViewModel.ComptesModifiesRejetes = comptesDeclares.Where(c => c.Code == "M1" && c.Etat == "R").Count();

                traitementAllerViewModel.PersPhysiquesDeclaresAcceptes = persPhysiquesDeclares.Where(c => c.Code == "D2" && c.Etat == "D").Count();
                traitementAllerViewModel.PersPhysiquesDeclaresRejetes = persPhysiquesDeclares.Where(c => c.Code == "D2" && c.Etat == "R").Count();
                traitementAllerViewModel.PersPhysiquesModifiesAcceptes = persPhysiquesDeclares.Where(c => c.Code == "M2" && c.Etat == "D").Count();
                traitementAllerViewModel.PersPhysiquesModifiesRejetes = persPhysiquesDeclares.Where(c => c.Code == "M2" && c.Etat == "R").Count();

                traitementAllerViewModel.PersMoralesDeclaresAcceptes = persMoralesDeclares.Where(c => c.Code == "D3" && c.Etat == "D").Count();
                traitementAllerViewModel.PersMoralesDeclaresRejetes = persMoralesDeclares.Where(c => c.Code == "D3" && c.Etat == "R").Count();
                traitementAllerViewModel.PersMoralesModifiesAcceptes = persMoralesDeclares.Where(c => c.Code == "M3" && c.Etat == "D").Count();
                traitementAllerViewModel.PersMoralesModifiesRejetes = persMoralesDeclares.Where(c => c.Code == "M3" && c.Etat == "R").Count();

                traitementAllerViewModel.IncidentsChequesDeclaresAcceptes = incidentsChequesDeclares.Where(c => c.Code == "D5" && c.Etat == "D").Count();
                traitementAllerViewModel.IncidentsChequesDeclaresRejetes = incidentsChequesDeclares.Where(c => c.Code == "D5" && c.Etat == "R").Count();
                traitementAllerViewModel.IncidentsChequesModifiesAcceptes = incidentsChequesDeclares.Where(c => c.Code == "M5" && c.Etat == "D").Count();
                traitementAllerViewModel.IncidentsChequesModifiesRejetes = incidentsChequesDeclares.Where(c => c.Code == "M5" && c.Etat == "R").Count();

                traitementAllerViewModel.ChequesIrreguliersDeclaresAcceptes = chequesIrreguliersDeclares.Where(c => c.Code == "D6" && c.Etat == "D").Count();
                traitementAllerViewModel.ChequesIrreguliersDeclaresRejetes = chequesIrreguliersDeclares.Where(c => c.Code == "D6" && c.Etat == "R").Count();
                traitementAllerViewModel.ChequesIrreguliersModifiesAcceptes = chequesIrreguliersDeclares.Where(c => c.Code == "M6" && c.Etat == "D").Count();
                traitementAllerViewModel.ChequesIrreguliersModifiesRejetes = chequesIrreguliersDeclares.Where(c => c.Code == "M6" && c.Etat == "R").Count();

                traitementAllerViewModel.IncidentsEffetsDeclaresAcceptes = incidentsEffetsDeclares.Where(c => c.Code == "D7" && c.Etat == "D").Count();
                traitementAllerViewModel.IncidentsEffetsDeclaresRejetes = incidentsEffetsDeclares.Where(c => c.Code == "D7" && c.Etat == "R").Count();
                traitementAllerViewModel.IncidentsEffetsModifiesAcceptes = incidentsEffetsDeclares.Where(c => c.Code == "M7" && c.Etat == "D").Count();
                traitementAllerViewModel.IncidentsEffetsModifiesRejetes = incidentsEffetsDeclares.Where(c => c.Code == "M7" && c.Etat == "R").Count();

                return traitementAllerViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<ErreurEnregistrementDeclareViewModel>> GetErreursEnregistrementsDeclaresAsync(string enregistrementID, string enregistrementTable)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                try
                {
                    string sql; ;

                    sql = "select * from V_ListeErreursEnregistrementsDeclares  where EnregistrementID = '" + enregistrementID + "' and EnregistrementTable = '" + enregistrementTable  + "';";
                        
                    return await connection.QueryAsync<ErreurEnregistrementDeclareViewModel>(sql);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task DeleteAllErrorsDatasAsync(string table, string agence)
        {
            if (table == "Pers_Morale")
            {
               var persMoraleViewModels = await this.GetPersMoralesAsync(agence, "E");

                foreach (var persMoraleViewModel in persMoraleViewModels)
                {
                    _coreDBConnection.Open();

                    IDbCommand dbCommand = _coreDBConnection.CreateCommand();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "PK_CIP.F_Delete_CIP3";

                    IDbDataParameter param2 = dbCommand.CreateParameter();
                    param2.ParameterName = "return_value";
                    param2.Direction = ParameterDirection.ReturnValue;
                    param2.DbType = DbType.String;
                    dbCommand.Parameters.Add(param2);

                    IDbDataParameter param1 = dbCommand.CreateParameter();
                    param1.ParameterName = "P_Numseq";
                    param1.DbType = DbType.String;
                    param1.Direction = ParameterDirection.Input;
                    param1.Value = persMoraleViewModel.Num_Enr;
                    dbCommand.Parameters.Add(param1);

                    dbCommand.ExecuteNonQuery();

                    _coreDBConnection.Close();

                    _appDBConnection.Close();
                    string sql = @"delete " + table + " where Num_Enr = '" + persMoraleViewModel.Num_Enr + "' ";
                    _appDBConnection.Execute(sql);
                    _appDBConnection.Close();

                    _appDBConnection.Close();
                    sql = @"delete ErreurEnregistrement where EnregistrementID in (select Id from " + table + " where Num_Enr = '" + persMoraleViewModel.Num_Enr + "') and EnregistrementTable = '" + table + "' ";
                    _appDBConnection.Execute(sql);
                    _appDBConnection.Close();

                    Guid guid = Guid.NewGuid();
                    var messageLog = "EnregistrementTable : " + table + ", EnregistrementID : " + persMoraleViewModel.Id;
                    TLog log = new TLog(guid, "Suppression d'une donnée en erreur", messageLog, guid, DateTime.Now);
                    _repositoryFactory.LogRepository.Add(log);
                    await _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
                }
            }

            if (table == "Pers_Physique")
            {
                var persPhysiqueViewModels = await this.GetPersPhysiquesAsync(agence, "E");

                foreach (var persPhysiqueViewModel in persPhysiqueViewModels)
                {

                    _coreDBConnection.Open();

                    IDbCommand dbCommand = _coreDBConnection.CreateCommand();
                    dbCommand.CommandType = CommandType.StoredProcedure;
                    dbCommand.CommandText = "PK_CIP.F_Delete_CIP2";

                    IDbDataParameter param2 = dbCommand.CreateParameter();
                    param2.ParameterName = "return_value";
                    param2.Direction = ParameterDirection.ReturnValue;
                    param2.DbType = DbType.String;
                    dbCommand.Parameters.Add(param2);

                    IDbDataParameter param1 = dbCommand.CreateParameter();
                    param1.ParameterName = "P_Numseq";
                    param1.DbType = DbType.String;
                    param1.Direction = ParameterDirection.Input;
                    param1.Value = persPhysiqueViewModel.Num_Enr;
                    dbCommand.Parameters.Add(param1);

                    dbCommand.ExecuteNonQuery();

                    _coreDBConnection.Close();

                    _appDBConnection.Close();
                    string sql = @"delete " + table + " where Num_Enr = '" + persPhysiqueViewModel.Num_Enr + "' ";
                    _appDBConnection.Execute(sql);
                    _appDBConnection.Close();

                    _appDBConnection.Close();
                    sql = @"delete ErreurEnregistrement where EnregistrementID in (select Id from " + table + " where Num_Enr = '" + persPhysiqueViewModel.Num_Enr + "') and EnregistrementTable = '" + table + "' ";
                    _appDBConnection.Execute(sql);
                    _appDBConnection.Close();

                    Guid guid = Guid.NewGuid();
                    var messageLog = "EnregistrementTable : " + table + ", EnregistrementID : " + persPhysiqueViewModel.Id;
                    TLog log = new TLog(guid, "Suppression d'une donnée en erreur", messageLog, guid, DateTime.Now);
                    _repositoryFactory.LogRepository.Add(log);
                    await _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
                }
            }
        }
    }
    
}
