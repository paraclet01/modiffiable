using Dapper;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.DataEntities;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Implementation
{
    public class PreparationQueries : IPreparationQueries
    {
        private readonly IDbConnection _dbConnection;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IDeclarationQueries _declarationQueries;
        private readonly IParametresQuerie _parametresQueries;
        private readonly string _connectionString;
        private readonly string _prefixCompte;
        static public readonly string _codeToutCompte = "&!";
        private string _ProprietaireBdORA;

        public PreparationQueries(IDbConnection dbConnection, IRepositoryFactory repositoryFactory, IDeclarationQueries declarationQueries, string constr, IParametresQuerie parametresQueries)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _declarationQueries = declarationQueries ?? throw new ArgumentNullException(nameof(declarationQueries));
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
            _parametresQueries = parametresQueries ??  throw new ArgumentNullException(nameof(parametresQueries));
            _prefixCompte = getPrefixCompte();
            
            _ProprietaireBdORA = Startup.Configuration["ORA_PROPRIETAIRE"];
        }

        public string getPrefixCompte()
        {
            try
            {
                var parameter = _parametresQueries.GetParametresByIdAsync(4);
                return parameter.Result.Libelle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        bool champEstVide(string nomChamp, string valeurChamp, List<string> erreurs)
        {

            if (String.IsNullOrEmpty(valeurChamp))
            {
                erreurs.Add("Le champ " + nomChamp + " est vide");
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<TCompte>> LancerPreparationComptes(string userID)
        {
            IEnumerable<CIP1ViewModel> cip1ViewModels;
            List<TCompte> comptes = new List<TCompte>();

            await Task.Run(() => {
       
                // Mise à jour des données à préparer
                this.UpdateCIP1StateAsync("V", "P", DateTime.Now.Date);

                // Récupération des données à préparer
                cip1ViewModels = this.GetCIP1Async("P").Result;

                // Transformation et sauvegarde
                foreach (var cip1 in cip1ViewModels)
                {
                    string code = "";

                    switch (cip1.type)
                    {
                        case "N":
                            code = "D1";
                            break;
                        case "M":
                            code = "M1";
                            break;
                        case "S":
                            code = "S1";
                            break;
                        default:
                            break;
                    }

                    var compte = new TCompte(Guid.NewGuid(), code, cip1.numseq, cip1.codgch, (cip1.codbnq + cip1.codgch + _prefixCompte + cip1.compte), cip1.clerib, cip1.datouv, cip1.datfrm, "", "C", DateTime.Now, null);
                    comptes.Add(compte);
                    _repositoryFactory.CompteRepository.Add(compte);
                }

                if (_repositoryFactory.CompteRepository.UnitOfWork.SaveEntitiesAsync().Result)
                    this.UpdateCIP1StateAsync("P", "E", DateTime.Now.Date);
                else
                    this.UpdateCIP1StateAsync("P", "V", DateTime.Now.Date);

                // Logging
                Guid guidLog = Guid.NewGuid();

                Guid userGuid;
                if (String.IsNullOrEmpty(userID))
                    userGuid = Guid.NewGuid();
                else
                    userGuid = Guid.Parse(userID);

                TLog log = new TLog(guidLog, "Préparation des comptes", comptes.Count.ToString(), userGuid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            });

            return comptes;
        }

        public async Task<int[]> LancerPreparationComptes_V2(string userID)
        {
            DateTime dDateActon = DateTime.Now;
            //int[] ret = await GetDataFromOracleAsync<OPTICIP.Entities.DataEntities.Cip1Temp>(1, dDateActon, userID, "CIP1", "ORA_SEL_FOR_CIP1", "ORA_SEL_FOR_CIP1_WHERE_DATE_CLAUSE", "SQL_SEL_MAX_CIP1_DATE", "SQL_PS_INSERT_CIP1");
            //if ( ret.Length>=2 && ( ret[0] > 0 || ret[1] > 0))
            //{
            //    InsertXcipFromSqlPS("SQL_PS_INSERT_COMPTE", dDateActon, out ret[0], out ret[0]);
            //}

            int[] ret = await GetDataFromOracleAsync<OPTICIP.Entities.DataEntities.Cip2Temp>(1, dDateActon, userID, "CIP2", "ORA_SEL_FOR_CIP2", "ORA_SEL_FOR_CIP2_WHERE_DATE_CLAUSE", "SQL_SEL_MAX_CIP2_DATE", "SQL_PS_INSERT_CIP2");
            //if (ret.Length >= 2 && (ret[0] > 0 || ret[1] > 0))
            //{
            //    InsertXcipFromSqlPS("SQL_PS_INSERT_COMPTE", dDateActon, out ret[0], out ret[0]);
            //}
            return ret;
            //return await LancerPreparationComptes_V2_OLD(userID);
        }

        public async Task<int[]> GetDataFromOracleAsync<T>(int pTypeImportation, DateTime pDateAction, string userID, String pDestTable, String pQueryCode, String pQueryWhereCode, String pQueryWhereDateMaxCode , String pCodePSInsertionCip)
        where T : CIPEntity
        {
            int pNombreIns = 0;
            int pNombreUpd = 0;
            IEnumerable<T> data = null;
            try
            {
                RequetesDiverses reqDiv = new RequetesDiverses(_connectionString);
                var ret = await reqDiv.TruncateTable($"{pDestTable}_TEMP");
                await Task.Run(() =>
                {
                    String sQuery = Startup.Configuration[pQueryCode];

                    String sQueryFinal = String.Format(sQuery, _ProprietaireBdORA);
                    _dbConnection.Open();
                    if (pQueryWhereCode != null)
                    {
                        DateTime? dateRef = null;

                        dateRef = reqDiv.GetScalarDataByQuery<DateTime?>(Startup.Configuration[pQueryWhereDateMaxCode]);
                        if (dateRef == null || dateRef == DateTime.MinValue)
                            data = _dbConnection.Query<T>(sQueryFinal);
                        else
                        {
                            sQueryFinal = @$"{sQueryFinal} {Startup.Configuration[pQueryWhereCode]}";
                            data = _dbConnection.Query<T>(sQueryFinal, new { dateRef });
                        }
                    }
                    else
                        data = _dbConnection.Query<T>(sQueryFinal);

                    if (data.Count() > 0)
                    {
                        CIPContext cxt = new CIPContext();
                        cxt.Set<T>().AddRange(data);

                        if (cxt.SaveChanges() > 0)
                        {
                            InsertXcipFromSqlPS(pCodePSInsertionCip, pDateAction, out pNombreIns, out pNombreUpd);

                            SuiviImportation si = new SuiviImportation();
                            si.NombreLignesIns = pNombreIns;
                            si.NombreLignesMaj = pNombreUpd;
                            si.TypeImportation = pTypeImportation;
                            si.DateImportation = pDateAction;
                            cxt.Add(si);
                            cxt.SaveChanges();
                        }
                    }

                });

                return new int[] { pNombreIns, pNombreUpd };
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
                    _dbConnection.Close();
            }
        }
        private bool InsertXcipFromSqlPS(string pCodePSInsertionXCIP, DateTime pDateAction, out int iNombreInsert, out int iNombreUpdate)
        {
            using (SqlConnection cnn = new SqlConnection(_connectionString))
            {
                cnn.Open();

                SqlCommand dbCommand = cnn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = Startup.Configuration[pCodePSInsertionXCIP];

                IDbDataParameter pNombreInsert = dbCommand.CreateParameter();
                pNombreInsert.ParameterName = "NombreInsert";
                pNombreInsert.DbType = DbType.Int32;
                pNombreInsert.Direction = ParameterDirection.Output;

                IDbDataParameter pNombreUpdate = dbCommand.CreateParameter();
                pNombreUpdate.ParameterName = "NombreUpdate";
                pNombreUpdate.DbType = DbType.Int32;
                pNombreUpdate.Direction = ParameterDirection.Output;

                dbCommand.Parameters.Add(pNombreInsert);
                dbCommand.Parameters.Add(pNombreUpdate);
                dbCommand.Parameters.AddWithValue("DateInsertion", pDateAction);

                dbCommand.ExecuteNonQuery();
                iNombreInsert = (int)pNombreInsert.Value;
                iNombreUpdate = (int)pNombreUpdate.Value;
                return true;
            }
        }



        public async Task<IEnumerable<TPersPhysique>> LancerPreparationPersonnesPhysiques(string userID)
        {
            IEnumerable<CIP2ViewModel> cip2ViewModels;
            List<TPersPhysique> persPhysiques = new List<TPersPhysique>();
            List<string> erreurs;

            await Task.Run(() => {
                // Mise à jour des données à préparer
                this.UpdateCIP2StateAsync("V", "P", DateTime.Now.Date);

                // Récupération des données à préparer
                cip2ViewModels = this.GetCIP2Async("P").Result;

                // Transformation et sauvegarde
                foreach (var cip2 in cip2ViewModels)
                {
                    Guid guid = Guid.NewGuid();
                    string code = "", etatEnregistrement = "C", RIB, cleRIB, numeroEnregistrement, nomNaissance, prenoms, communeNaissance, nomMere, adresse, ville ;
                    erreurs = new List<string>();

                    switch (cip2.type)
                    {
                        case "N":
                            code = "D2";
                            break;
                        case "M":
                            code = "M2";
                            break;
                        case "S":
                            code = "S2";
                            break;
                        default:
                            break;
                    }

                    RIB = cip2.codbnq + cip2.codgch + _prefixCompte + cip2.compte;
                    cleRIB = cip2.clerib;

                    if (champEstVide("Numéro d'enregistrement", cip2.numseq, erreurs)) numeroEnregistrement = "";
                    else numeroEnregistrement = cip2.numseq;

                    if (champEstVide("Nom de naissance", cip2.nomnais, erreurs)) nomNaissance = "";
                    else nomNaissance = cip2.nomnais;

                    if (champEstVide("Prénoms", cip2.prenom, erreurs)) prenoms = "";
                    else prenoms = cip2.prenom;

                    if (champEstVide("Lieu de naissance", cip2.commnais, erreurs)) communeNaissance = "";
                    else communeNaissance = cip2.commnais;

                    if (champEstVide("Nom de la mère", cip2.nommere, erreurs)) nomMere = "";
                    else nomMere = cip2.nommere;

                    if (champEstVide("Adresse", cip2.adr, erreurs)) adresse = "";
                    else adresse = cip2.adr;

                    if (champEstVide("Ville", cip2.ville, erreurs)) ville = "";
                    else ville = cip2.ville;

                    if (erreurs.Count != 0)
                    {
                        etatEnregistrement = "E";

                        foreach (var erreur in erreurs)
                        {
                            var erreurEnregistrement = new TErreurEnregistrement(Guid.NewGuid(), guid, "Pers_Physique", erreur);
                            _repositoryFactory.ErreurEnregistrementRepository.Add(erreurEnregistrement);
                        }

                        _repositoryFactory.ErreurEnregistrementRepository.UnitOfWork.SaveEntitiesAsync();
                    }

                        var persPhysique = new TPersPhysique(
                        guid,
                        code,
                        numeroEnregistrement,
                        cip2.codgch,
                        RIB,
                        cleRIB,
                        nomNaissance,
                        prenoms,
                        communeNaissance,
                        cip2.datnais,
                        (cip2.nommari == null ? "" : cip2.nommari),
                        nomMere,
                        cip2.iso,
                        cip2.sexe,
                        cip2.residumoa,
                        cip2.numid != null ? cip2.numid.Replace(" ", "") : "",
                        cip2.resp,
                        cip2.mand,
                        adresse,
                        (cip2.payadr == null ? "" : cip2.payadr),
                        ville,
                        "",
                        "",
                        etatEnregistrement,
                        DateTime.Now,
                        null,

                        //==> CIP V2
                        cip2.EmailTitu,
                        cip2.NomContact,
                        cip2.PnomContact,
                        cip2.AdrContact,
                        cip2.EmailContact
                    );

                    _repositoryFactory.PersPhysiqueRepository.Add(persPhysique);
                   persPhysiques.Add(persPhysique);
                }

                if (_repositoryFactory.PersPhysiqueRepository.UnitOfWork.SaveEntitiesAsync().Result)
                    this.UpdateCIP2StateAsync("P", "E", DateTime.Now.Date);
                else
                    this.UpdateCIP2StateAsync("P", "V", DateTime.Now.Date);

                // Logging
                Guid guidLog = Guid.NewGuid();

                Guid userGuid;
                if (String.IsNullOrEmpty(userID))
                    userGuid = Guid.NewGuid();
                else
                    userGuid = Guid.Parse(userID);

                TLog log = new TLog(guidLog, "Préparation des personnes physiques", persPhysiques.Count.ToString(), userGuid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            });

            return persPhysiques;
        }

        public async Task<IEnumerable<TPersMorale>> LancerPreparationPersonnesMorales(string userID)
        {
            IEnumerable<CIP3ViewModel> cip3ViewModels;
            List<TPersMorale> persMorales = new List<TPersMorale>();
            List<string> erreurs;

            await Task.Run(() => {
                // Mise à jour des données à préparer
                this.UpdateCIP3StateAsync("V", "P", DateTime.Now.Date);

                // Récupération des données à préparer
                cip3ViewModels = this.GetCIP3Async("P").Result;

                // Transformation et sauvegarde
                foreach (var cip3 in cip3ViewModels)
                {
                    Guid guid = Guid.NewGuid();
                    string code = "", etatEnregistrement = "C", numeroEnregistrement, codePays, categoriePersMorale, identifiantPersMorale, RIB, 
                    cleRIB, raisonSociale, sigle, adresse, ville, codeActivite, responsable, mandataire;

                    erreurs = new List<string>();

                    switch (cip3.type)
                    {
                        case "N":
                            code = "D3";
                            break;
                        case "M":
                            code = "M3";
                            break;
                        case "S":
                            code = "S3";
                            break;
                        default:
                            break;
                    }

                    RIB = cip3.codbnq + cip3.codgch + _prefixCompte + cip3.compte;
                    cleRIB = cip3.clerib;
                    sigle = (cip3.sigle == null ? "" : cip3.sigle);
                    codeActivite = cip3.codape;
                    responsable = cip3.resp;
                    mandataire = cip3.mand;

                    if (champEstVide("Numéro d'enregistrement", cip3.numseq, erreurs)) 
                        numeroEnregistrement = "";
                    else 
                        numeroEnregistrement = cip3.numseq;

                    if (champEstVide("Code pays", cip3.iso, erreurs)) 
                        codePays = "";
                    else 
                        codePays = cip3.iso;

                    if (champEstVide("Catégorie personne morale", cip3.juricat, erreurs)) 
                        categoriePersMorale = "";
                    else 
                        categoriePersMorale = cip3.juricat;

                    if (champEstVide("Identifiant personne morale", cip3.rcsno, erreurs)) 
                        identifiantPersMorale = "";
                    else 
                        identifiantPersMorale = cip3.rcsno;

                    if (champEstVide("Raison sociale", cip3.forme, erreurs)) 
                        raisonSociale = "";
                    else 
                        raisonSociale = cip3.forme;

                    if (champEstVide("Adresse", cip3.adr, erreurs)) 
                        adresse = "";
                    else 
                        adresse = cip3.adr;

                    if (champEstVide("Ville", cip3.ville, erreurs)) 
                        ville = "";
                    else 
                        ville = cip3.ville;

                    if (erreurs.Count != 0)
                    {
                        etatEnregistrement = "E";

                        foreach (var erreur in erreurs)
                        {
                            var erreurEnregistrement = new TErreurEnregistrement(Guid.NewGuid(), guid, "Pers_Morale", erreur);
                            _repositoryFactory.ErreurEnregistrementRepository.Add(erreurEnregistrement);
                        }

                        _repositoryFactory.ErreurEnregistrementRepository.UnitOfWork.SaveEntitiesAsync();
                    };

                    var persMorale = new TPersMorale(
                        guid,
                        code,
                        numeroEnregistrement,
                        cip3.codgch,
                        RIB,
                        cleRIB,
                        codePays,
                        categoriePersMorale,
                        identifiantPersMorale,
                        raisonSociale,
                        sigle,
                        codeActivite,
                        responsable,
                        mandataire,
                        adresse,
                        ville,
                        "",
                        "",
                        etatEnregistrement,
                        DateTime.Now,
                        null,
                    //==> CIP V2
                        cip3.Email
                    );

                    _repositoryFactory.PersMoraleRepository.Add(persMorale);
                    persMorales.Add(persMorale);
                }

                if (_repositoryFactory.PersMoraleRepository.UnitOfWork.SaveEntitiesAsync().Result)
                    this.UpdateCIP3StateAsync("P", "E", DateTime.Now.Date);
                else
                    this.UpdateCIP3StateAsync("P", "V", DateTime.Now.Date);

                // Logging
                Guid guidLog = Guid.NewGuid();

                Guid userGuid;
                if (String.IsNullOrEmpty(userID))
                    userGuid = Guid.NewGuid();
                else
                    userGuid = Guid.Parse(userID);

                TLog log = new TLog(guidLog, "Préparation des personnes morales", persMorales.Count.ToString(), userGuid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            });

            return persMorales;
        }

        public async Task<IEnumerable<TCarte>> LancerPreparationCartes(string userID)
        {
            List<TCarte> cartes = new List<TCarte>();

            await Task.Run(() => {
            });

            return cartes;
        }

        public async Task<IEnumerable<TIncidentCheque>> LancerPreparationCheques(string userID)
        {
            IEnumerable<CIP5ViewModel> cip5ViewModels;
            List<TIncidentCheque> incidentCheques = new List<TIncidentCheque>();

            await Task.Run(() => {
                // Mise à jour des données à préparer
                this.UpdateCIP5StateAsync("V", "P", DateTime.Now.Date);

                // Récupération des données à préparer
                cip5ViewModels = this.GetCIP5Async("P").Result;

                // Transformation et sauvegarde
                foreach (var cip5 in cip5ViewModels)
                {
                    string code = "";

                    switch (cip5.type)
                    {
                        case "N":
                            code = "D5";
                            break;
                        case "M":
                            code = "M5";
                            break;
                        case "S":
                            code = "S5";
                            break;
                        default:
                            break;
                    }

                    var incidentCheque = new TIncidentCheque(
                        Guid.NewGuid(),
                        code,
                        cip5.numseq,
                        cip5.codgch,
                        cip5.codbnq + cip5.codgch + _prefixCompte + cip5.compte,
                        cip5.clerib,
                        cip5.typinc,
                        cip5.datchq,
                        cip5.datinc,
                        cip5.datpre,
                        cip5.datinc,
                        cip5.mntfrf,
                        cip5.mntrej,
                        cip5.chqref.Trim().Substring(0,7),
                        cip5.datreg,
                        cip5.numpen == null ?  "" : cip5.numpen,
                        cip5.numlig == null ? "" : cip5.numlig,
                         "C",
                        DateTime.Now,
                        null,
                        //==> CIP V2
                        cip5.MontPen,
                        cip5.BenefNom,
                        cip5.BenefPrenom,
                        cip5.MotifCode,
                        cip5.MotifDesc
                    );

                    _repositoryFactory.IncidentChequeRepository.Add(incidentCheque);
                    incidentCheques.Add(incidentCheque);
                }

                if (_repositoryFactory.IncidentChequeRepository.UnitOfWork.SaveEntitiesAsync().Result)
                    this.UpdateCIP5StateAsync("P", "E", DateTime.Now.Date);
                else
                    this.UpdateCIP5StateAsync("P", "V", DateTime.Now.Date);

                // Logging
                Guid guidLog = Guid.NewGuid();

                Guid userGuid;
                if (String.IsNullOrEmpty(userID))
                    userGuid = Guid.NewGuid();
                else
                    userGuid = Guid.Parse(userID);

                TLog log = new TLog(guidLog, "Préparation des incidents chèques", incidentCheques.Count.ToString(), userGuid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            });

            return incidentCheques;
        }

        public async Task<IEnumerable<TChequeIrregulier>> LancerPreparationChequesIrreguliers(string userID)
        {
            IEnumerable<CIP6ViewModel> cip6ViewModels;
            List<TChequeIrregulier> chequeIrreguliers = new List<TChequeIrregulier>();

            await Task.Run(() => {
                // Mise à jour des données à préparer
                this.UpdateCIP6StateAsync("V", "P", DateTime.Now.Date);

                // Récupération des données à préparer
                cip6ViewModels = this.GetCIP6Async("P").Result;

                // Transformation et sauvegarde
                foreach (var cip6 in cip6ViewModels)
                {
                    string code = "", typIrregularite = "";

                    switch (cip6.type)
                    {
                        case "N":
                            code = "D6";
                            break;
                        case "M":
                            code = "M6";
                            break;
                        case "S":
                            code = "S6";
                            break;
                        default:
                            break;
                    }


                    switch (cip6.motif)
                    {
                        case "P":
                            typIrregularite = "0";
                            break;
                        case "V":
                            typIrregularite = "1";
                            break;
                        case "O":
                            typIrregularite = "2";
                            break;
                        default:
                            typIrregularite = "2";
                            break;
                    }

                    var chequeIrregulier = new TChequeIrregulier(
                        Guid.NewGuid(),
                        code,
                        cip6.numseq,
                        cip6.codgch,
                        cip6.codbnq + cip6.codgch + _prefixCompte + cip6.compte,
                        cip6.clerib,
                        typIrregularite,
                        cip6.chqref1,
                        cip6.chqref2,
                        cip6.datoppo,
                        "",
                        "C",
                        DateTime.Now,
                        null
                    ) ;

                    _repositoryFactory.ChequeIrregulierRepository.Add(chequeIrregulier);
                    chequeIrreguliers.Add(chequeIrregulier);
                }

                if (_repositoryFactory.ChequeIrregulierRepository.UnitOfWork.SaveEntitiesAsync().Result)
                    this.UpdateCIP6StateAsync("P", "E", DateTime.Now.Date);
                else
                    this.UpdateCIP6StateAsync("P", "V", DateTime.Now.Date);

                // Logging
                Guid guidLog = Guid.NewGuid();

                Guid userGuid;
                if (String.IsNullOrEmpty(userID))
                    userGuid = Guid.NewGuid();
                else
                    userGuid = Guid.Parse(userID);

                TLog log = new TLog(guidLog, "Préparation des chèques irréguliers", chequeIrreguliers.Count.ToString(), userGuid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            });

            return chequeIrreguliers;
        }

        public async Task<IEnumerable<TIncidentEffet>> LancerPreparationEffets(string userID)
        {
            IEnumerable<CIP7ViewModel> cip7ViewModels;
            List<TIncidentEffet> incidentEffets = new List<TIncidentEffet>();

            await Task.Run(() => {
                // Mise à jour des données à préparer
                this.UpdateCIP7StateAsync("V", "P", DateTime.Now.Date);

                // Récupération des données à préparer
                cip7ViewModels = this.GetCIP7Async("P").Result;

                // Transformation et sauvegarde
                foreach (var cip7 in cip7ViewModels)
                {
                    string code = "";

                    switch (cip7.type)
                    {
                        case "N":
                            code = "D7";
                            break;
                        case "M":
                            code = "M7";
                            break;
                        case "S":
                            code = "S7";
                            break;
                        default:
                            break;
                    }

                    var incidentEffet = new TIncidentEffet(
                        Guid.NewGuid(),
                        code,
                        cip7.numseq,
                        cip7.codgch,
                        cip7.codbnq + cip7.codgch + _prefixCompte + cip7.compte,
                        cip7.clerib,
                        cip7.datech,
                        cip7.mnt,
                        cip7.datref,
                        cip7.typeff,
                        cip7.motif,
                        cip7.avidom,
                        cip7.ordpai,
                        "",
                        "C",
                        DateTime.Now,
                        null,
                        //==> CIP V2
                        cip7.MotifDesc
                    ) ;

                    _repositoryFactory.IncidentEffetRepository.Add(incidentEffet);
                    incidentEffets.Add(incidentEffet);
                }

                if (_repositoryFactory.IncidentEffetRepository.UnitOfWork.SaveEntitiesAsync().Result)
                    this.UpdateCIP7StateAsync("P", "E", DateTime.Now.Date);
                else
                    this.UpdateCIP7StateAsync("P", "V", DateTime.Now.Date);

                // Logging
                Guid guidLog = Guid.NewGuid();

                Guid userGuid;
                if (String.IsNullOrEmpty(userID))
                    userGuid = Guid.NewGuid();
                else
                    userGuid = Guid.Parse(userID);

                TLog log = new TLog(guidLog, "Préparation des incidents effets", incidentEffets.Count.ToString(), userGuid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            });

            return incidentEffets;
        }

        public async Task<IEnumerable<CIP1ViewModel>> LancerPreparationInitialeComptes()
        {
            IEnumerable<CIP1ViewModel> result = new List<CIP1ViewModel>() ;

            await Task.Run(() => {

                // Récupération des données à préparer
                result = this.GetCIP1Async("V").Result;

                // Mise à jour des données à préparer
                this.UpdateCIP1StateAsync("V", "E", DateTime.Now.Date);

            });

            return result;
        }

        public async Task<IEnumerable<CIP2ViewModel>> LancerPreparationInitialePersonnesPhysiques()
        {
            IEnumerable<CIP2ViewModel> result = new List<CIP2ViewModel>();

            await Task.Run(() => {

                // Récupération des données à préparer
                result = this.GetCIP2Async("V").Result;
                // Mise à jour des données à préparer
                this.UpdateCIP2StateAsync("V", "E", DateTime.Now.Date);

            });

            return result;
        }

        public async Task<IEnumerable<CIP3ViewModel>> LancerPreparationInitialePersonnesMorales()
        {
            IEnumerable<CIP3ViewModel> result = new List<CIP3ViewModel>();

            await Task.Run(() => {

                // Récupération des données à préparer
                result = this.GetCIP3Async("V").Result;
                // Mise à jour des données à préparer
                this.UpdateCIP3StateAsync("V", "E", DateTime.Now.Date);

            });

            return result;
        }

        public async Task<IEnumerable<CIP5ViewModel>> LancerPreparationInitialeCheques()
        {
            IEnumerable<CIP5ViewModel> result = new List<CIP5ViewModel>();

            await Task.Run(() => {

                // Récupération des données à préparer
                result = this.GetCIP5Async("V").Result;
                // Mise à jour des données à préparer
                this.UpdateCIP5StateAsync("V", "E", DateTime.Now.Date);

            });

            return result;
        }

        public async Task<IEnumerable<CIP6ViewModel>> LancerPreparationInitialeChequesIrreguliers()
        {
            IEnumerable<CIP6ViewModel> result = new List<CIP6ViewModel>();

            await Task.Run(() => {

                // Récupération des données à préparer
                result = this.GetCIP6Async("V").Result;
                // Mise à jour des données à préparer
                this.UpdateCIP6StateAsync("V", "E", DateTime.Now.Date);

            });

            return result;
        }

        public async Task<IEnumerable<CIP7ViewModel>> LancerPreparationInitialeEffets()
        {
            IEnumerable<CIP7ViewModel> result = new List<CIP7ViewModel>();

            await Task.Run(() => {
                // Récupération des données à préparer
                result = this.GetCIP7Async("V").Result;
                // Mise à jour des données à préparer
                this.UpdateCIP7StateAsync("V", "E", DateTime.Now.Date);
            });

            return result;
        }

        //public async Task<IEnumerable<TDonnees_A_Declarer>> LancerPreparationDonnees(string agence)
        public async Task<IEnumerable<TDonnees_A_Declarer>> LancerPreparationDonnees(string agence, bool bInitialisation = false)
        {
            List<TDonnees_A_Declarer> donnees_A_Declarer = new List<TDonnees_A_Declarer>();
            TDonnees_A_Declarer donnee_A_Declarer;

            List<CompteViewModel> comptesDeclares = new List<CompteViewModel>();
            List<PersPhysiqueViewModel> persPhysiquesDeclarees = new List<PersPhysiqueViewModel>();
            List<PersMoraleViewModel> persMoralesDeclarees = new List<PersMoraleViewModel>();
            List<IncidentsChequesViewModel> incidentsChequesDeclares = new List<IncidentsChequesViewModel>();
            List<ChequesIrreguliersViewModel> chequesIrreguliersDeclares = new List<ChequesIrreguliersViewModel>();
            List<IncidentsEffetsViewModel> incidentsEffetsDeclares = new List<IncidentsEffetsViewModel>();

            try
            {
                var Parametres = _parametresQueries.GetParametreByCodeAsync("VERSION_CIP");

                IEnumerable<CompteViewModel> comptes = new List<CompteViewModel>();
                IEnumerable<PersPhysiqueViewModel> persPhysiques = new List<PersPhysiqueViewModel>();
                IEnumerable<PersPhysiqueViewModel> persPhysiquesErreurs = new List<PersPhysiqueErreurViewModel>();
                IEnumerable<PersMoraleViewModel> persMorales = new List<PersMoraleViewModel>();
                IEnumerable<PersMoraleViewModel> persMoralesErreurs = new List<PersMoraleErreurViewModel>();
                IEnumerable<IncidentsChequesViewModel> incidentsCheques = new List<IncidentsChequesViewModel>();
                IEnumerable<ChequesIrreguliersViewModel> incidentsChequesIrreguliers = new List<ChequesIrreguliersViewModel>();
                IEnumerable<IncidentsEffetsViewModel> incidentsEffets = new List<IncidentsEffetsViewModel>();

                //var comptes = await _declarationQueries.GetComptesAsync(agence, "C");
                //var persPhysiques = await _declarationQueries.GetPersPhysiquesAsync(agence, "C");
                //var persPhysiquesErreurs = await _declarationQueries.GetPersPhysiquesAsync(agence, "E");
                //var persMorales = await _declarationQueries.GetPersMoralesAsync(agence, "C");
                //var persMoralesErreurs = await _declarationQueries.GetPersMoralesAsync(agence, "E");
                //var incidentsCheques = await _declarationQueries.GetIncidentsChequesAsync(agence, "C");
                //var incidentsChequesIrreguliers = await _declarationQueries.GetChequesIrreguliersAsync(agence, "C");
                //var incidentsEffets = await _declarationQueries.GetIncidentsEffetsAsync(agence, "C");

                if (bInitialisation == true) //==> Récupérer toutes les lignes sans considérer les erreurs
                {
                    comptes = await _declarationQueries.GetComptesAsync(agence, PreparationQueries._codeToutCompte);
                    persPhysiques = await _declarationQueries.GetPersPhysiquesAsync(agence, PreparationQueries._codeToutCompte);
                    persMorales = await _declarationQueries.GetPersMoralesAsync(agence, PreparationQueries._codeToutCompte);
                    incidentsCheques = await _declarationQueries.GetIncidentsChequesAsync(agence, PreparationQueries._codeToutCompte);
                    incidentsChequesIrreguliers = await _declarationQueries.GetChequesIrreguliersAsync(agence, PreparationQueries._codeToutCompte);
                    incidentsEffets = await _declarationQueries.GetIncidentsEffetsAsync(agence, PreparationQueries._codeToutCompte);
                }
                else
                {
                    comptes = await _declarationQueries.GetComptesAsync(agence, "C");
                    persPhysiques = await _declarationQueries.GetPersPhysiquesAsync(agence, "C");
                    persPhysiquesErreurs = await _declarationQueries.GetPersPhysiquesAsync(agence, "E");
                    persMorales = await _declarationQueries.GetPersMoralesAsync(agence, "C");
                    persMoralesErreurs = await _declarationQueries.GetPersMoralesAsync(agence, "E");
                    incidentsCheques = await _declarationQueries.GetIncidentsChequesAsync(agence, "C");
                    incidentsChequesIrreguliers = await _declarationQueries.GetChequesIrreguliersAsync(agence, "C");
                    incidentsEffets = await _declarationQueries.GetIncidentsEffetsAsync(agence, "C");
                }

                string texte = "";
                int compteur = 1;

                // Préparation des comptes, personnes physiques et personnes morales
                foreach (var compte in comptes)
                {

                    var persPhysiquesErreursLieesAuCompte = persPhysiquesErreurs.Where(p => p.RIB == compte.RIB);
                    var persMoralesErreursLieesAuCompte = persMoralesErreurs.Where(p => p.RIB == compte.RIB);

                    if (persPhysiquesErreursLieesAuCompte.Count() == 0 && persMoralesErreursLieesAuCompte.Count() == 0)
                    {
                        /*Préparation du compte*/
                        texte = compte.Code + ";"
                            + compte.Num_Enr + ";"
                            + compte.RIB + ";"
                            + compte.Cle_RIB + ";"
                            + compte.Date_Ouverture.ToString("dd/MM/yyyy") + ";"
                            + (compte.Date_Fermerture == DateTime.MinValue ? "          " : compte.Date_Fermerture.ToString("dd/MM/yyyy")) + ";"
                            + (compte.Num_Ligne_Erreur == "" ? "     " : compte.Num_Ligne_Erreur) + ";";

                        donnee_A_Declarer = new TDonnees_A_Declarer(Guid.NewGuid(), texte, compteur, agence, compte.Id, "Compte");
                        _repositoryFactory.Donnees_A_DeclarerRepository.Add(donnee_A_Declarer);
                        donnees_A_Declarer.Add(donnee_A_Declarer);

                        comptesDeclares.Add(compte);

                        compteur++;

                        /*Préparation de la personne physique*/
                        var persPhysiquesLieesAuCompte = persPhysiques.Where(p => p.RIB == compte.RIB);
                        foreach (var persPhysique in persPhysiquesLieesAuCompte)
                        {
                            texte = persPhysique.Code + ";"
                                    + persPhysique.Num_Enr + ";"
                                    + persPhysique.RIB + ";"
                                    + persPhysique.Cle_RIB + ";"
                                    + persPhysique.Nom_Naissance.PadRight(30, ' ') + ";"
                                    + persPhysique.Prenoms.PadRight(30, ' ') + ";"
                                    + persPhysique.Lieu_Naissance.PadRight(30, ' ') + ";"
                                    + persPhysique.Date_Naissance.ToString("dd/MM/yyyy").PadRight(10, ' ') + ";"
                                    + persPhysique.Nom_Mari.PadRight(30, ' ') + ";"
                                    + persPhysique.Nom_Naissance_Mere.PadRight(30, ' ') + ";"
                                    + persPhysique.Nationalite + ";"
                                    + persPhysique.Sexe + ";"
                                    + persPhysique.Resident_UEMOA + ";"
                                    + (persPhysique.Num_Carte_Iden.Length> 11 ? persPhysique.Num_Carte_Iden.Substring(0, 11) : persPhysique.Num_Carte_Iden).PadRight(11, ' ') + ";"
                                    + persPhysique.Responsable + ";"
                                    + persPhysique.Mandataire + ";"
                                    + persPhysique.Adresse.PadRight(50, ' ') + ";"
                                    + persPhysique.Code_Pays + ";"
                                    + persPhysique.Ville.PadRight(30, ' ') + ";"
                                    + persPhysique.Num_Reg_Com.PadRight(50, ' ') + ";";

                            //==> CIP V2
                            if (Parametres != null && Parametres.Libelle == "2")
                            {
                                texte += (persPhysique.EmailTitu??"").Replace(';',',').PadRight(30, ' ') + ";"
                                + (persPhysique.NomContact??"").PadRight(30, ' ') + ";"
                                + (persPhysique.PnomContact??"").PadRight(30, ' ') + ";"
                                + (persPhysique.AdrContact??"").PadRight(50, ' ') + ";"
                                + (persPhysique.EmailContact??"").Replace(';', ',').PadRight(30, ' ') + ";";
                            }
                            texte += (persPhysique.Num_Ligne_Erreur == "" ? "     " : persPhysique.Num_Ligne_Erreur) + ";";

                            donnee_A_Declarer = new TDonnees_A_Declarer(Guid.NewGuid(), texte, compteur, agence, persPhysique.Id, "Pers_Physique");
                            _repositoryFactory.Donnees_A_DeclarerRepository.Add(donnee_A_Declarer);
                            donnees_A_Declarer.Add(donnee_A_Declarer);

                            persPhysiquesDeclarees.Add(persPhysique);

                            compteur++;
                        }

                        /*Préparation de la personne morale*/
                        var persMoralesLieesAuCompte = persMorales.Where(p => p.RIB == compte.RIB);
                        foreach (var persMorale in persMoralesLieesAuCompte)
                        {
                            texte = persMorale.Code + ";"
                                    + persMorale.Num_Enr + ";"
                                    + persMorale.RIB + ";"
                                    + persMorale.Cle_RIB + ";"
                                    + persMorale.Code_Pays + ";"
                                    + persMorale.Cat_Personne + ";"
                                    + persMorale.Iden_Personne.PadRight(50, ' ') + ";"
                                    + persMorale.Raison_Soc.PadRight(50, ' ') + ";"
                                    + persMorale.Sigle.PadRight(15, ' ') + ";"
                                    + persMorale.Code_Activite.PadRight(8, ' ') + ";"
                                    + persMorale.Responsable + ";"
                                    + persMorale.Mandataire + ";"
                                    + persMorale.Adresse.PadRight(50, ' ') + ";"
                                    + persMorale.Code_Pays + ";"
                                    + persMorale.Ville.PadRight(30, ' ') + ";"
                                    + persMorale.Iden_BCEAO.PadRight(10, ' ') + ";";

                            //==> CIP V2
                            if (Parametres != null && Parametres.Libelle == "2")
                            {
                                texte += (persMorale.Email??"").Replace(';', ',').PadRight(30, ' ') + ";";
                            }
                            //
                            texte += (persMorale.Num_Ligne_Erreur == "" ? "     " : persMorale.Num_Ligne_Erreur) + ";";

                            donnee_A_Declarer = new TDonnees_A_Declarer(Guid.NewGuid(), texte, compteur, agence, persMorale.Id, "Pers_Morale");
                            _repositoryFactory.Donnees_A_DeclarerRepository.Add(donnee_A_Declarer);
                            donnees_A_Declarer.Add(donnee_A_Declarer);

                            persMoralesDeclarees.Add(persMorale);

                            compteur++;
                        }
                    }                   
                }

                //Préparation des incidents chèques
                foreach (var incidentCheque in incidentsCheques)
                {
                    texte = incidentCheque.Code + ";"
                        + incidentCheque.Num_Enr + ";"
                        + incidentCheque.RIB + ";"
                        + incidentCheque.Cle_RIB + ";"
                        + incidentCheque.Type_Incident + ";"
                        + (incidentCheque.Date_Emission == DateTime.MinValue ? "          " : incidentCheque.Date_Emission.ToString("dd/MM/yyyy")) + ";"
                        + (incidentCheque.Date_Refus_Paiement == DateTime.MinValue ? "          " : incidentCheque.Date_Refus_Paiement.ToString("dd/MM/yyyy")) + ";"
                        + (incidentCheque.Date_Presentation == DateTime.MinValue ? "          " : incidentCheque.Date_Presentation.ToString("dd/MM/yyyy")) + ";"
                        + (incidentCheque.Point_Depart == DateTime.MinValue ? "          " : incidentCheque.Point_Depart.ToString("dd/MM/yyyy")) + ";"
                        + incidentCheque.Montant_Nominal.ToString().PadLeft(12, ' ') + ";"
                        + incidentCheque.Montant_Insuffisance.ToString().PadLeft(12, ' ') + ";"
                        + incidentCheque.Numero_Cheque + ";"
                        + (incidentCheque.Date_Regularisation == DateTime.MinValue ? "          " : incidentCheque.Date_Regularisation.ToString("dd/MM/yyyy")) + ";"
                        + ((incidentCheque.Identifiant == null || incidentCheque.Identifiant == "" || incidentCheque.Identifiant == "expire" || incidentCheque.Identifiant == "0") ?
                            "".ToString().PadRight(20, ' ') : incidentCheque.Identifiant.PadRight(20, ' ')) + ";";

                    //==> CIP V2
                    if (Parametres != null && Parametres.Libelle == "2")
                    {
                        texte += (incidentCheque.MontPen == null ? "": incidentCheque.MontPen.ToString()).PadLeft(12, ' ') + ";"
                        + (incidentCheque.BenefNom??"").PadRight(30, ' ') + ";"
                        + (incidentCheque.BenefPrenom??"").PadRight(30, ' ') + ";"
                        + (incidentCheque.MotifCode==null?"":incidentCheque.MotifCode.ToString()).PadRight(1, ' ') + ";"
                        + (incidentCheque.MotifDesc??"").PadRight(50, ' ') + ";";
                    }
                    //

                    texte += (incidentCheque.Num_Ligne_Erreur == "" ? "     " : incidentCheque.Num_Ligne_Erreur) + ";";

                    donnee_A_Declarer = new TDonnees_A_Declarer(Guid.NewGuid(), texte, compteur, agence, incidentCheque.Id, "IncidentCheque");
                    _repositoryFactory.Donnees_A_DeclarerRepository.Add(donnee_A_Declarer);
                    donnees_A_Declarer.Add(donnee_A_Declarer);

                    incidentsChequesDeclares.Add(incidentCheque);

                    compteur++;
                }

                // Préparation des chèques irréguliers
                foreach (var chequeIrregulier in incidentsChequesIrreguliers)
                {
                    texte = chequeIrregulier.Code + ";"
                        + chequeIrregulier.Num_Enr + ";"
                        + chequeIrregulier.RIB + ";"
                        + chequeIrregulier.Cle_RIB + ";"
                        + chequeIrregulier.Type_Irregularite + ";"
                        + (chequeIrregulier.Debut_Lot == "" ? "       " : chequeIrregulier.Debut_Lot) + ";"
                        + (chequeIrregulier.Fin_Lot == "" ? "       " : chequeIrregulier.Fin_Lot) + ";"
                        + (chequeIrregulier.Date_Opposition == DateTime.MinValue ? "          " : chequeIrregulier.Date_Opposition.ToString("dd/MM/yyyy")) + ";"
                        + (chequeIrregulier.Num_Ligne_Erreur == "" ? "     " : chequeIrregulier.Num_Ligne_Erreur) + ";";

                    donnee_A_Declarer = new TDonnees_A_Declarer(Guid.NewGuid(), texte, compteur, agence, chequeIrregulier.Id, "ChequeIrregulier");
                    _repositoryFactory.Donnees_A_DeclarerRepository.Add(donnee_A_Declarer);
                    donnees_A_Declarer.Add(donnee_A_Declarer);

                    chequesIrreguliersDeclares.Add(chequeIrregulier);

                    compteur++;
                }

                // Préparation des incidents effets
                foreach (var incidentEffet in incidentsEffets)
                {
                    texte = incidentEffet.Code + ";"
                        + incidentEffet.Num_Enr + ";"
                        + incidentEffet.RIB + ";"
                        + incidentEffet.Cle_RIB + ";"
                        + (incidentEffet.Echeance == DateTime.MinValue ? "          " : incidentEffet.Echeance.ToString("dd/MM/yyyy")) + ";"
                        + incidentEffet.Montant.ToString().PadLeft(12, ' ') + ";"
                        + (incidentEffet.Date_Refus_Paiement == DateTime.MinValue ? "          " : incidentEffet.Date_Refus_Paiement.ToString("dd/MM/yyyy")) + ";"
                        + incidentEffet.Type_Effet + ";"
                        + incidentEffet.Motif_Non_Paiement + ";"
                        + incidentEffet.Avis_Domiciliation + ";"
                        + incidentEffet.Ordre_Paiement_Perm + ";";

                    //==> CIP V2
                    if (Parametres != null && Parametres.Libelle == "2")
                    {
                        texte += (incidentEffet.MotifDesc??"").PadRight(50, ' ') + ";";
                    }
                    //

                    texte += (incidentEffet.Num_Ligne_Erreur == "" ? "     " : incidentEffet.Num_Ligne_Erreur) + ";";

                    donnee_A_Declarer = new TDonnees_A_Declarer(Guid.NewGuid(), texte, compteur, agence, incidentEffet.Id, "IncidentEffet");
                    _repositoryFactory.Donnees_A_DeclarerRepository.Add(donnee_A_Declarer);
                    donnees_A_Declarer.Add(donnee_A_Declarer);

                    incidentsEffetsDeclares.Add(incidentEffet);

                    compteur++;
                }

                if (donnees_A_Declarer.Count > 0)
                {
                    // Persistence des données préparées
                    await _repositoryFactory.Donnees_A_DeclarerRepository.UnitOfWork.SaveEntitiesAsync();

                    // Changement de statut des données préparées C --> P
                    foreach (var compteDeclare in comptesDeclares)
                        this.UpdateCompteStateAsync(compteDeclare, "P", DateTime.Now);

                    foreach (var persPhysiqueDeclaree in persPhysiquesDeclarees)
                        this.UpdatePersPhysiqueStateAsync(persPhysiqueDeclaree, "P", DateTime.Now);

                    foreach (var persMoraleDeclaree in persMoralesDeclarees)
                        this.UpdatePersMoraleStateAsync(persMoraleDeclaree, "P", DateTime.Now);

                    foreach (var incidentChequeDeclare in incidentsChequesDeclares)
                        this.UpdateIncidentChequeStateAsync(incidentChequeDeclare, "P", DateTime.Now);

                    foreach (var chequeIrregulierDeclare in chequesIrreguliersDeclares)
                        this.UpdateChequeIrregulierStateAsync(chequeIrregulierDeclare, "P", DateTime.Now);

                    foreach (var incidentEffetDeclare in incidentsEffetsDeclares)
                        this.UpdateIncidentEffetStateAsync(incidentEffetDeclare, "P", DateTime.Now);
                }

                // Logging
                Guid guid = Guid.NewGuid();
                string message = "Agence: " + agence + " ,Comptes: " + comptesDeclares.Count() + " ,PP: " + persPhysiquesDeclarees.Count() + " ,PM: " + persMoralesDeclarees.Count()
                    + " ,Chèques: " + incidentsChequesDeclares.Count() + " ,Chèques irréguliers: " + chequesIrreguliersDeclares.Count() + " ,Effets: " + incidentsEffetsDeclares.Count();
                TLog log = new TLog(guid, "Préparation des données à déclarer",  message, guid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                await _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }          

            return donnees_A_Declarer;
        }

        private int UpdateCompteStateAsync(CompteViewModel compteDeclare, string newState, DateTime date)
        {
            int result;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = @"update compte set Etat = '" + newState + "', Date_Declaration = '" + date.ToString("yyyy/MM/dd") + "' where Id = '" + compteDeclare.Id + "' and Id not in ( select  d.IdItem from dbo.DonneeRetire d where d.Table_Item='Compte')";
                    result = connection.Execute(sql);
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        private int UpdatePersPhysiqueStateAsync(PersPhysiqueViewModel persPhysiqueDeclaree, string newState, DateTime date)
        {
            int result;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = @"update pers_physique set Etat = '" + newState + "', Date_Declaration = '" + date.ToString("yyyy/MM/dd") + "' where Id = '" + persPhysiqueDeclaree.Id + "' and Id not in ( select  d.IdItem from dbo.DonneeRetire d where d.Table_Item='Pers_Physique')";
                    result = connection.Execute(sql);
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private int UpdatePersMoraleStateAsync(PersMoraleViewModel persMoraleDeclaree, string newState, DateTime date)
        {
            int result;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = @"update pers_morale set Etat = '" + newState + "', Date_Declaration = '" + date.ToString("yyyy/MM/dd") + "' where Id = '" + persMoraleDeclaree.Id + "' and Id not in ( select  d.IdItem from dbo.DonneeRetire d where d.Table_Item='Pers_Morale')";
                    result = connection.Execute(sql);
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        private int UpdateIncidentChequeStateAsync(IncidentsChequesViewModel incidentChequeDeclare, string newState, DateTime date)
        {
            int result;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = @"update incidentcheque set Etat = '" + newState + "', Date_Declaration = '" + date.ToString("yyyy/MM/dd") + "' where Id = '" + incidentChequeDeclare.Id + "' and Id not in ( select  d.IdItem from dbo.DonneeRetire d where d.Table_Item='IncidentCheque')";
                    result = connection.Execute(sql);
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        private int UpdateChequeIrregulierStateAsync(ChequesIrreguliersViewModel chequeIrregulierDeclare, string newState, DateTime date)
        {
            int result;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = @"update chequeirregulier set Etat = '" + newState + "', Date_Declaration = '" + date.ToString("yyyy/MM/dd") + "' where Id = '" + chequeIrregulierDeclare.Id + "' and Id not in ( select  d.IdItem from dbo.DonneeRetire d where d.Table_Item='ChequeIrregulier')";
                    result = connection.Execute(sql);
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        private int UpdateIncidentEffetStateAsync(IncidentsEffetsViewModel incidentEffetDeclare, string newState, DateTime date)
        {
            int result;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string sql = @"update incidenteffet set Etat = '" + newState + "', Date_Declaration = '" + date.ToString("yyyy/MM/dd") + "' where Id = '" + incidentEffetDeclare.Id + "' and Id not in ( select  d.IdItem from dbo.DonneeRetire d where d.Table_Item='IncidentEffet')";
                    result = connection.Execute(sql);
                    connection.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public async Task<IEnumerable<CIP1ViewModel>> GetCIP1Async(string state)
        {
            try
            {

                _dbConnection.Open();
                IEnumerable<CIP1ViewModel> result = await _dbConnection.QueryAsync<CIP1ViewModel>(@"select * from v_cip1 where state = '" + state + "' ");
                _dbConnection.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int UpdateCIP1StateAsync(string oldstate, string newstate, DateTime date)
        {
            int result = 0;

            try
            {
                _dbConnection.Open();

                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PK_CIP.F_Upd_StateCIP1";

                IDbDataParameter param4 = dbCommand.CreateParameter();
                param4.ParameterName = "return_value";
                param4.Direction = ParameterDirection.ReturnValue;
                param4.DbType = DbType.String;
                dbCommand.Parameters.Add(param4);

                IDbDataParameter param1 = dbCommand.CreateParameter();
                param1.ParameterName = "P_OldState";
                param1.DbType = DbType.String;
                param1.Direction = ParameterDirection.Input;
                param1.Value = oldstate;
                dbCommand.Parameters.Add(param1);

                IDbDataParameter param2 = dbCommand.CreateParameter();
                param2.ParameterName = "P_NewState";
                param2.DbType = DbType.String;
                param2.Direction = ParameterDirection.Input;
                param2.Value = newstate;
                dbCommand.Parameters.Add(param2);

                IDbDataParameter param3 = dbCommand.CreateParameter();
                param3.ParameterName = "P_Datenv";
                param3.DbType = DbType.DateTime;
                param3.Direction = ParameterDirection.Input;
                param3.Value = date;
                dbCommand.Parameters.Add(param3);

                result = dbCommand.ExecuteNonQuery();

                _dbConnection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
          

            

            return result;
        }

        public async Task<IEnumerable<CIP2ViewModel>> GetCIP2Async(string state)
        {
            try
            {

                _dbConnection.Open();
                IEnumerable<CIP2ViewModel> result = await _dbConnection.QueryAsync<CIP2ViewModel>(@"select * from v_cip2 where state = '" + state + "' ");
                _dbConnection.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateCIP2StateAsync(string oldstate, string newstate, DateTime date)
        {
            int result = 0;
            try
            {
                _dbConnection.Open();

                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PK_CIP.F_Upd_StateCIP2";

                IDbDataParameter param4 = dbCommand.CreateParameter();
                param4.ParameterName = "return_value";
                param4.Direction = ParameterDirection.ReturnValue;
                param4.DbType = DbType.String;
                dbCommand.Parameters.Add(param4);

                IDbDataParameter param1 = dbCommand.CreateParameter();
                param1.ParameterName = "P_OldState";
                param1.DbType = DbType.String;
                param1.Direction = ParameterDirection.Input;
                param1.Value = oldstate;
                dbCommand.Parameters.Add(param1);

                IDbDataParameter param2 = dbCommand.CreateParameter();
                param2.ParameterName = "P_NewState";
                param2.DbType = DbType.String;
                param2.Direction = ParameterDirection.Input;
                param2.Value = newstate;
                dbCommand.Parameters.Add(param2);

                IDbDataParameter param3 = dbCommand.CreateParameter();
                param3.ParameterName = "P_Datenv";
                param3.DbType = DbType.Date;
                param3.Direction = ParameterDirection.Input;
                param3.Value = date;
                dbCommand.Parameters.Add(param3); 

                result = dbCommand.ExecuteNonQuery();

                _dbConnection.Close();          
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<IEnumerable<CIP3ViewModel>> GetCIP3Async(string state)
        {
            try
            {

                _dbConnection.Open();
                IEnumerable<CIP3ViewModel> result = await _dbConnection.QueryAsync<CIP3ViewModel>(@"select * from v_cip3 where state = '" + state + "' ");
                _dbConnection.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateCIP3StateAsync(string oldstate, string newstate, DateTime date)
        {
            int result = 0;
            try
            {
                _dbConnection.Open();

                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PK_CIP.F_Upd_StateCIP3";

                IDbDataParameter param4 = dbCommand.CreateParameter();
                param4.ParameterName = "return_value";
                param4.Direction = ParameterDirection.ReturnValue;
                param4.DbType = DbType.String;
                dbCommand.Parameters.Add(param4);

                IDbDataParameter param1 = dbCommand.CreateParameter();
                param1.ParameterName = "P_OldState";
                param1.DbType = DbType.String;
                param1.Direction = ParameterDirection.Input;
                param1.Value = oldstate;
                dbCommand.Parameters.Add(param1);

                IDbDataParameter param2 = dbCommand.CreateParameter();
                param2.ParameterName = "P_NewState";
                param2.DbType = DbType.String;
                param2.Direction = ParameterDirection.Input;
                param2.Value = newstate;
                dbCommand.Parameters.Add(param2);

                IDbDataParameter param3 = dbCommand.CreateParameter();
                param3.ParameterName = "P_Datenv";
                param3.DbType = DbType.Date;
                param3.Direction = ParameterDirection.Input;
                param3.Value = date;
                dbCommand.Parameters.Add(param3);

                result = dbCommand.ExecuteNonQuery();

                _dbConnection.Close();            
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<IEnumerable<CIP4ViewModel>> GetCIP4Async(string state)
        {
            using (_dbConnection)
            {
                _dbConnection.Open();
                try
                {
                    return await _dbConnection.QueryAsync<CIP4ViewModel>(@"select * from v_cip4 where state=@state", new { state });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<CIP5ViewModel>> GetCIP5Async(string state)
        {
            try
            {

                _dbConnection.Open();
                IEnumerable<CIP5ViewModel> result = await _dbConnection.QueryAsync<CIP5ViewModel>(@"select * from v_cip5 where state = '" + state + "' ");
                _dbConnection.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateCIP5StateAsync(string oldstate, string newstate, DateTime date)
        {
            int result = 0;
            try
            {
                _dbConnection.Open();

                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PK_CIP.F_Upd_StateCIP5";

                IDbDataParameter param4 = dbCommand.CreateParameter();
                param4.ParameterName = "return_value";
                param4.Direction = ParameterDirection.ReturnValue;
                param4.DbType = DbType.String;
                dbCommand.Parameters.Add(param4);

                IDbDataParameter param1 = dbCommand.CreateParameter();
                param1.ParameterName = "P_OldState";
                param1.DbType = DbType.String;
                param1.Direction = ParameterDirection.Input;
                param1.Value = oldstate;
                dbCommand.Parameters.Add(param1);

                IDbDataParameter param2 = dbCommand.CreateParameter();
                param2.ParameterName = "P_NewState";
                param2.DbType = DbType.String;
                param2.Direction = ParameterDirection.Input;
                param2.Value = newstate;
                dbCommand.Parameters.Add(param2);

                IDbDataParameter param3 = dbCommand.CreateParameter();
                param3.ParameterName = "P_Datenv";
                param3.DbType = DbType.Date;
                param3.Direction = ParameterDirection.Input;
                param3.Value = date;
                dbCommand.Parameters.Add(param3);

                result = dbCommand.ExecuteNonQuery();

                _dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<IEnumerable<CIP6ViewModel>> GetCIP6Async(string state)
        {
            try
            {

                _dbConnection.Open();
                IEnumerable<CIP6ViewModel> result = await _dbConnection.QueryAsync<CIP6ViewModel>(@"select * from v_cip6 where state = '" + state + "' ");
                _dbConnection.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateCIP6StateAsync(string oldstate, string newstate, DateTime date)
        {
            int result = 0;
            try
            {
                _dbConnection.Open();

                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PK_CIP.F_Upd_StateCIP6";

                IDbDataParameter param4 = dbCommand.CreateParameter();
                param4.ParameterName = "return_value";
                param4.Direction = ParameterDirection.ReturnValue;
                param4.DbType = DbType.String;
                dbCommand.Parameters.Add(param4);

                IDbDataParameter param1 = dbCommand.CreateParameter();
                param1.ParameterName = "P_OldState";
                param1.DbType = DbType.String;
                param1.Direction = ParameterDirection.Input;
                param1.Value = oldstate;
                dbCommand.Parameters.Add(param1);

                IDbDataParameter param2 = dbCommand.CreateParameter();
                param2.ParameterName = "P_NewState";
                param2.DbType = DbType.String;
                param2.Direction = ParameterDirection.Input;
                param2.Value = newstate;
                dbCommand.Parameters.Add(param2);

                IDbDataParameter param3 = dbCommand.CreateParameter();
                param3.ParameterName = "P_Datenv";
                param3.DbType = DbType.Date;
                param3.Direction = ParameterDirection.Input;
                param3.Value = date;
                dbCommand.Parameters.Add(param3);

                result = dbCommand.ExecuteNonQuery();

                _dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<IEnumerable<CIP7ViewModel>> GetCIP7Async(string state)
        {
            try
            {

                _dbConnection.Open();
                IEnumerable<CIP7ViewModel> result = await _dbConnection.QueryAsync<CIP7ViewModel>(@"select * from v_cip7 where state = '" + state + "' ");
                _dbConnection.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateCIP7StateAsync(string oldstate, string newstate, DateTime date)
        {
            int result = 0;
            try
            {
                _dbConnection.Open();

                IDbCommand dbCommand = _dbConnection.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PK_CIP.F_Upd_StateCIP7";

                IDbDataParameter param4 = dbCommand.CreateParameter();
                param4.ParameterName = "return_value";
                param4.Direction = ParameterDirection.ReturnValue;
                param4.DbType = DbType.String;
                dbCommand.Parameters.Add(param4);

                IDbDataParameter param1 = dbCommand.CreateParameter();
                param1.ParameterName = "P_OldState";
                param1.DbType = DbType.String;
                param1.Direction = ParameterDirection.Input;
                param1.Value = oldstate;
                dbCommand.Parameters.Add(param1);

                IDbDataParameter param2 = dbCommand.CreateParameter();
                param2.ParameterName = "P_NewState";
                param2.DbType = DbType.String;
                param2.Direction = ParameterDirection.Input;
                param2.Value = newstate;
                dbCommand.Parameters.Add(param2);

                IDbDataParameter param3 = dbCommand.CreateParameter();
                param3.ParameterName = "P_Datenv";
                param3.DbType = DbType.Date;
                param3.Direction = ParameterDirection.Input;
                param3.Value = date;
                dbCommand.Parameters.Add(param3);

                result = dbCommand.ExecuteNonQuery();

                _dbConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
