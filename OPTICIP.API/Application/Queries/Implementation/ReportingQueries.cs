using Dapper;
using FastReport;
using FastReport.Data;
using FastReport.Export.PdfSimple;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using OPTICIP.BusinessLogicLayer.Utilitaire;
using OPTICIP.DataAccessLayer.EntityConfigurations;
using OPTICIP.Entities.DataEntities;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OPTICIP.DataAccessLayer.Models;

namespace OPTICIP.API.Application.Queries.Implementation
{
    public partial class ReportingQueries : IReportingQueries
    {
        private readonly IDbConnection _dbConnection;
        private readonly IRepositoryFactory _repositoryFactory;
        private string _connectionString = string.Empty;
        private string _oracleConnectionString = string.Empty;
        string _reportingDirectory = string.Empty;
        int _DelaiLettre = 0;
        private ParametresQuerie _parametresQueries;

        public ReportingQueries(IDbConnection dbConnection, IRepositoryFactory repositoryFactory, string constr, string reportingDirectory, int iDelaiLettre)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
            _reportingDirectory = reportingDirectory;
            _oracleConnectionString = _dbConnection.ConnectionString;
            _DelaiLettre = iDelaiLettre;
            _parametresQueries = new ParametresQuerie(constr);
            var param = _parametresQueries.GetParametreByCodeAsync("TailleBlockDeclaration");
            if (param != null)
            {
                int.TryParse(param.Libelle, out _DelaiLettre);
            }

        }

        public DateTime GetMaxDateIncident(string typeLettre)
        {
            DateTime? maxDateInc = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    maxDateInc = connection.ExecuteScalar<DateTime?>(@$"select Max(DatInc) from DonneesIncidentChq Where TypeIncident={typeLettre}");
                    if (maxDateInc == null)
                        return DateTime.Today.AddDays(-_DelaiLettre);
                    else
                        return maxDateInc.Value.Date;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DateTime GetMaxDateIncidentMandataire(string typeLettre)
        {
            DateTime? maxDateInc = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    maxDateInc = connection.ExecuteScalar<DateTime?>(@$"select Max(DatInc) from DonneesMandataireIncident Where Type_Incident={typeLettre}");
                    if (maxDateInc == null)
                        return DateTime.Today.AddDays(-_DelaiLettre);
                    else
                        return maxDateInc.Value.Date;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<AvertViewModel>> GetChequesEnAvertissement()
        {
            try
            {
                DateTime dMaxDate = GetMaxDateIncident("0");
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return await _dbConnection.QueryAsync<AvertViewModel>(@"select * from v_cip_ltavert where datinc >= :MaxDate", new { MaxDate = dMaxDate });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
                    _dbConnection.Close();
            }
        }

        public async Task<IEnumerable<InjViewModel>> GetChequesEnInjonction()
        {
            try
            {
                DateTime dMaxDate = GetMaxDateIncident("1");
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return await _dbConnection.QueryAsync<InjViewModel>(@"select * from v_cip_ltinj where datinc >= :MaxDate", new { MaxDate = dMaxDate });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
                    _dbConnection.Close();
            }
        }

        public async Task<IEnumerable<InfraViewModel>> GetChequesEnInfraction()
        {
            try
            {
                DateTime dMaxDate = GetMaxDateIncident("2");
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return await _dbConnection.QueryAsync<InfraViewModel>(@"select * from v_cip_ltinfra where datinc >= :MaxDate", new { MaxDate = dMaxDate });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
                    _dbConnection.Close();
            }

        }

        public async Task<IEnumerable<InfMandViewModel>> GetMandatairesDesChequesEnInjonction()
        {
            try
            {
                DateTime dMaxDate = GetMaxDateIncidentMandataire("1");
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return await _dbConnection.QueryAsync<InfMandViewModel>(@"select * from v_cip_ltinfomand where type_incident = 'INJ' and datinc >= :MaxDate", new { MaxDate = dMaxDate });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
                    _dbConnection.Close();
            }
        }

        public async Task<IEnumerable<InfMandViewModel>> GetMandatairesDesChequesEnInfraction()
        {
            try
            {
                DateTime dMaxDate = GetMaxDateIncidentMandataire("2");
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return await _dbConnection.QueryAsync<InfMandViewModel>(@"select * from v_cip_ltinfomand where type_incident = 'INF' and datinc >= :MaxDate", new { MaxDate = dMaxDate });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
                    _dbConnection.Close();
            }
        }

        public async Task<IEnumerable<CertNonPaiementViewModel>> GetDonneesCertNonPaiement()
        {
            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return await _dbConnection.QueryAsync<CertNonPaiementViewModel>(@"select * from v_cip_ltcertifnonpaie");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
                    _dbConnection.Close();
            }
        }

        public async Task GenererLettreAvertissement()
        {
            await Task.Run(() =>
            {             
                try
                {
                    string typeLettre = "Avertissement";
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();         
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'avertissement
                    report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;
                    IEnumerable<AvertViewModel> chequesAvertissement = GetChequesEnAvertissement().Result; // Lettres à générées
                    //IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var chequeAve in chequesAvertissement)
                    {
                        //if (lettres.Where(l => (l.Numero_Compte == chequeAve.compte && l.Numero_Cheque == chequeAve.numcheq && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)  // Contrôle si la lettre n'a pas été déjà générée
                        if (!VerifierLettreExiste(typeLettre, chequeAve.compte, chequeAve.numcheq))
                        {
                            report.SetParameterValue("MyReportParameter", chequeAve.numcheq);
                            report.Prepare();

                            // Génération de la lettre d'avertissement
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                PDFSimpleExport pdfExport = new PDFSimpleExport();
                                pdfExport.Export(report.Report, memoryStream);

                                fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + chequeAve.compte.Substring(1, 6) + "_" + chequeAve.numcheq + ".pdf";
                                filePath = lettreDirectory + fileName;

                                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                                {
                                    memoryStream.WriteTo(fileStream);
                                }
                            }

                            // Enregistrement en base de données de l'information sur la lettre générée
                            var lettre = new TLettre(Guid.NewGuid(), chequeAve.nooper, chequeAve.compte.Substring(1, 6), chequeAve.compte, chequeAve.numcheq, chequeAve.montchq, chequeAve.datinc, DateTime.Now, filePath, typeLettre, fileName, "");
                            _repositoryFactory.LettreRepository.Add(lettre);
                            bool res = _repositoryFactory.LettreRepository.UnitOfWork.SaveEntitiesAsync().Result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                           
            });
        }

        public async Task GenererLotLettreAvertissement()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "Avertissement";
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    //IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    //if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    if (!VerifierLettreLotExiste(typeLettre))
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;
                        report.Prepare();

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            PDFSimpleExport pdfExport = new PDFSimpleExport();
                            pdfExport.Export(report.Report, memoryStream);

                            fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
                            filePath = lettreDirectory + fileName;

                            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                            {
                                memoryStream.WriteTo(fileStream);
                            }
                        }

                        // Enregistrement en base de données de l'information sur la lettre générée
                        var lettre = new TLettreLot(Guid.NewGuid(), DateTime.Now, filePath, typeLettre, fileName);
                        _repositoryFactory.LettreLotRepository.Add(lettre);
                        bool res = _repositoryFactory.LettreLotRepository.UnitOfWork.SaveEntitiesAsync().Result;
                    }
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task GenererLettreInjonction()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "Injonction";
                   
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'injonction
                    report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;

                    IEnumerable<InjViewModel> chequesInj = GetChequesEnInjonction().Result;
                    //IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var chequeInj in chequesInj)
                    {
                        //if (lettres.Where(l => l.Numero_Compte == chequeInj.compte && l.Numero_Cheque == chequeInj.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
                        if (!VerifierLettreExiste(typeLettre, chequeInj.compte, chequeInj.numcheq))
                        {
                            report.SetParameterValue("MyReportParameter", chequeInj.numcheq);
                            report.Prepare();

                            // Génération de la lettre d'injonction
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                PDFSimpleExport pdfExport = new PDFSimpleExport();
                                pdfExport.Export(report.Report, memoryStream);

                                fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + chequeInj.compte.Substring(1, 6) + "_" + chequeInj.numcheq + ".pdf";
                                filePath = lettreDirectory + fileName;

                                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                                {
                                    memoryStream.WriteTo(fileStream);
                                }
                            }

                            // Enregistrement en base de données de l'information sur la lettre générée
                            var lettre = new TLettre(Guid.NewGuid(), chequeInj.nooper, chequeInj.compte.Substring(1, 6), chequeInj.compte, chequeInj.numcheq, chequeInj.montchq, chequeInj.datinc, DateTime.Now, filePath, typeLettre, fileName, "");
                            _repositoryFactory.LettreRepository.Add(lettre);
                            bool res = _repositoryFactory.LettreRepository.UnitOfWork.SaveEntitiesAsync().Result;
                        }                    
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            });
        }

        public async Task GenererLotLettreInjonction()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "Injonction";
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    //IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    //if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    if (!VerifierLettreLotExiste(typeLettre))
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;
                        report.Prepare();

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            PDFSimpleExport pdfExport = new PDFSimpleExport();
                            pdfExport.Export(report.Report, memoryStream);

                            fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
                            filePath = lettreDirectory + fileName;

                            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                            {
                                memoryStream.WriteTo(fileStream);
                            }
                        }

                        // Enregistrement en base de données de l'information sur la lettre générée
                        var lettre = new TLettreLot(Guid.NewGuid(), DateTime.Now, filePath, typeLettre, fileName);
                        _repositoryFactory.LettreLotRepository.Add(lettre);
                        bool res = _repositoryFactory.LettreLotRepository.UnitOfWork.SaveEntitiesAsync().Result;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task GenererLettresEnInfraction()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "Infraction";

                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'injonction
                    report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;

                    IEnumerable<InfraViewModel> chequesInf = GetChequesEnInfraction().Result;
                    //IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var chequeInf in chequesInf)
                    {
                        //if (lettres.Where(l => l.Numero_Compte == chequeInf.compte && l.Numero_Cheque == chequeInf.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
                        if (!VerifierLettreExiste(typeLettre, chequeInf.compte, chequeInf.numcheq))
                        {
                            report.SetParameterValue("MyReportParameter", chequeInf.numcheq);
                            report.Prepare();

                            // Génération de la lettre d'injonction
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                PDFSimpleExport pdfExport = new PDFSimpleExport();
                                pdfExport.Export(report.Report, memoryStream);

                                fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + chequeInf.compte.Substring(1, 6) + "_" + chequeInf.numcheq + ".pdf";
                                filePath = lettreDirectory + fileName;

                                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                                {
                                    memoryStream.WriteTo(fileStream);
                                }
                            }

                            // Enregistrement en base de données de l'information sur la lettre générée
                            var lettre = new TLettre(Guid.NewGuid(), chequeInf.nooper, chequeInf.compte.Substring(1, 6), chequeInf.compte, chequeInf.numcheq, chequeInf.montchq, chequeInf.datinc, DateTime.Now, filePath, typeLettre, fileName, "");
                            _repositoryFactory.LettreRepository.Add(lettre);
                            bool res = _repositoryFactory.LettreRepository.UnitOfWork.SaveEntitiesAsync().Result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            });
        }

        public async Task GenererLotLettresEnInfraction()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "Infraction";
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    //IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    //if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    if (!VerifierLettreLotExiste(typeLettre))
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;
                        report.Prepare();

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            PDFSimpleExport pdfExport = new PDFSimpleExport();
                            pdfExport.Export(report.Report, memoryStream);

                            fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
                            filePath = lettreDirectory + fileName;

                            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                            {
                                memoryStream.WriteTo(fileStream);
                            }
                        }

                        // Enregistrement en base de données de l'information sur la lettre générée
                        var lettre = new TLettreLot(Guid.NewGuid(), DateTime.Now, filePath, typeLettre, fileName);
                        _repositoryFactory.LettreLotRepository.Add(lettre);
                        bool res = _repositoryFactory.LettreLotRepository.UnitOfWork.SaveEntitiesAsync().Result;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public async Task GenererLettreInfMandatairesInj()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "InfMandataireInj";

                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx");
                    report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;

                    IEnumerable<InfMandViewModel> mandsChequesInf = GetMandatairesDesChequesEnInjonction().Result;
                    //IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var mandChequesInf in mandsChequesInf)
                    {
                        //if (lettres.Where(l => l.IDP == mandChequesInf.idp && l.Numero_Compte == mandChequesInf.compte 
                        //                    && l.Numero_Cheque == mandChequesInf.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
                        if (!VerifierLettreMandataireExiste(typeLettre, mandChequesInf.idp, mandChequesInf.compte, mandChequesInf.numcheq))
                        {
                            report.SetParameterValue("MyReportParameter", mandChequesInf.numcheq);
                            report.SetParameterValue("MyReportParameterIDP", mandChequesInf.idp);
                            report.Prepare();

                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                PDFSimpleExport pdfExport = new PDFSimpleExport();
                                pdfExport.Export(report.Report, memoryStream);

                                fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + mandChequesInf.compte.Substring(1, 6) + "_" 
                                            + mandChequesInf.idp +  "_" + mandChequesInf.numcheq + ".pdf";
                                filePath = lettreDirectory + fileName;

                                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                                {
                                    memoryStream.WriteTo(fileStream);
                                }
                            }

                            var lettre = new TLettre(Guid.NewGuid(), mandChequesInf.nooper, mandChequesInf.compte.Substring(1, 6), mandChequesInf.compte, mandChequesInf.numcheq, 
                                                        mandChequesInf.montchq, mandChequesInf.datinc, DateTime.Now, filePath, typeLettre, fileName, mandChequesInf.idp);
                            _repositoryFactory.LettreRepository.Add(lettre);
                            bool res = _repositoryFactory.LettreRepository.UnitOfWork.SaveEntitiesAsync().Result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            });
        }

        public async Task GenererLettreInfMandatairesInf()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "InfMandataireInf";

                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx");
                    report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;

                    IEnumerable<InfMandViewModel> mandsChequesInf = GetMandatairesDesChequesEnInfraction().Result;
                    //IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var mandChequesInf in mandsChequesInf)
                    {
                        //if (lettres.Where(l => l.IDP == mandChequesInf.idp && l.Numero_Compte == mandChequesInf.compte
                        //                    && l.Numero_Cheque == mandChequesInf.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
                        if (!VerifierLettreMandataireExiste(typeLettre, mandChequesInf.idp, mandChequesInf.compte, mandChequesInf.numcheq))
                        {
                            report.SetParameterValue("MyReportParameter", mandChequesInf.numcheq);
                            report.SetParameterValue("MyReportParameterIDP", mandChequesInf.idp);
                            report.Prepare();

                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                PDFSimpleExport pdfExport = new PDFSimpleExport();
                                pdfExport.Export(report.Report, memoryStream);

                                fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + mandChequesInf.compte.Substring(1, 6) + "_"
                                            + mandChequesInf.idp + "_" + mandChequesInf.numcheq + ".pdf";
                                filePath = lettreDirectory + fileName;

                                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                                {
                                    memoryStream.WriteTo(fileStream);
                                }
                            }

                            var lettre = new TLettre(Guid.NewGuid(), mandChequesInf.nooper, mandChequesInf.compte.Substring(1, 6), mandChequesInf.compte, mandChequesInf.numcheq,
                                                        mandChequesInf.montchq, mandChequesInf.datinc, DateTime.Now, filePath, typeLettre, fileName, mandChequesInf.idp);
                            _repositoryFactory.LettreRepository.Add(lettre);
                            bool res = _repositoryFactory.LettreRepository.UnitOfWork.SaveEntitiesAsync().Result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            });
        }
       
        public async Task GenererLotLettreInfMandatairesInf()
        {
            await Task.Run(() =>
            {
                LotLettreInfMandatairesInf();
            });
        }

        private void LotLettreInfMandatairesInf()
        {
            try
            {
                string typeLettre = "InfMandataireInf";
                String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                string filePath, fileName;

                if (!Directory.Exists(lettreDirectory))
                    Directory.CreateDirectory(lettreDirectory);

                FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                Report report = new Report();

                //IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                //if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                if (!VerifierLettreLotExiste(typeLettre))
                {
                    // Chargement du template de la lettre d'avertissement
                    report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                    report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;
                    report.Prepare();

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PDFSimpleExport pdfExport = new PDFSimpleExport();
                        pdfExport.Export(report.Report, memoryStream);

                        fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
                        filePath = lettreDirectory + fileName;

                        using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                        {
                            memoryStream.WriteTo(fileStream);
                        }
                    }

                    // Enregistrement en base de données de l'information sur la lettre générée
                    var lettre = new TLettreLot(Guid.NewGuid(), DateTime.Now, filePath, typeLettre, fileName);
                    _repositoryFactory.LettreLotRepository.Add(lettre);
                    bool res = _repositoryFactory.LettreLotRepository.UnitOfWork.SaveEntitiesAsync().Result;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task GenererLotLettreInfMandatairesInj()
        {
            await Task.Run(() =>
            {
                LotLettreInfMandatairesInj();
            });
        }

        private void LotLettreInfMandatairesInj()
        {
            try
            {
                string typeLettre = "InfMandataireInj";
                String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                string filePath, fileName;

                if (!Directory.Exists(lettreDirectory))
                    Directory.CreateDirectory(lettreDirectory);

                FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                Report report = new Report();

                //IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                //if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                if (!VerifierLettreLotExiste(typeLettre))
                {
                    // Chargement du template de la lettre d'avertissement
                    report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                    report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;
                    report.Prepare();

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PDFSimpleExport pdfExport = new PDFSimpleExport();
                        pdfExport.Export(report.Report, memoryStream);

                        fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
                        filePath = lettreDirectory + fileName;

                        using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                        {
                            memoryStream.WriteTo(fileStream);
                        }
                    }

                    // Enregistrement en base de données de l'information sur la lettre générée
                    var lettre = new TLettreLot(Guid.NewGuid(), DateTime.Now, filePath, typeLettre, fileName);
                    _repositoryFactory.LettreLotRepository.Add(lettre);
                    bool res = _repositoryFactory.LettreLotRepository.UnitOfWork.SaveEntitiesAsync().Result;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task GenererCertNonPaiements()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "CertNonPaiement";
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx");
                    report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;

                    IEnumerable<CertNonPaiementViewModel> enrAGeneres = GetDonneesCertNonPaiement().Result;
//                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var enr in enrAGeneres)
                    {
//                        if (lettres.Where(l => l.Numero_Compte == enr.compte && l.Numero_Cheque == enr.chqref && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
                        if (!VerifierLettreExiste(typeLettre, enr.compte, enr.chqref))
                        {
                            report.SetParameterValue("MyReportParameter", enr.chqref);
                            report.Prepare();

                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                PDFSimpleExport pdfExport = new PDFSimpleExport();
                                pdfExport.Export(report.Report, memoryStream);

                                fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + enr.compte.Substring(1, 6)  + "_" + enr.chqref + ".pdf";
                                filePath = lettreDirectory + fileName;

                                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                                {
                                    memoryStream.WriteTo(fileStream);
                                }
                            }

                            var lettre = new TLettre(Guid.NewGuid(), enr.nooper, enr.compte.Substring(1, 6), enr.compte, enr.chqref, enr.mntrej.ToString(), enr.datinc, DateTime.Now, filePath, typeLettre, fileName, "");
                            _repositoryFactory.LettreRepository.Add(lettre);
                            bool res = _repositoryFactory.LettreRepository.UnitOfWork.SaveEntitiesAsync().Result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            });
        }

        public async Task<List<String>> GenererLotCertNonPaiements()
        {
            List<string> result = new List<string>();
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "CertNonPaiement";
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    //IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;
                    //if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    if (!VerifierLettreLotExiste(typeLettre))
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;
                        report.Prepare();

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            PDFSimpleExport pdfExport = new PDFSimpleExport();
                            pdfExport.Export(report.Report, memoryStream);

                            fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
                            filePath = lettreDirectory + fileName;
                            result.Add($@"{typeLettre}\{fileName}");

                            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                            {
                                memoryStream.WriteTo(fileStream);
                            }
                        }

                        // Enregistrement en base de données de l'information sur la lettre générée
                        var lettre = new TLettreLot(Guid.NewGuid(), DateTime.Now, filePath, typeLettre, fileName);
                        _repositoryFactory.LettreLotRepository.Add(lettre);
                        bool res = _repositoryFactory.LettreLotRepository.UnitOfWork.SaveEntitiesAsync().Result;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return result;
        }

        public async Task GenererAttNonPaiementEffet()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "AttNonPaiementEffet";

                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'injonction
                    report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;

                    IEnumerable<AttNonPaiementEffetViewModel> attNonPaiementEffets = GetAttNonPaiementEffet().Result;
                    //IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var attNonPaiementEffet in attNonPaiementEffets)
                    {
                        //if (lettres.Where(l => l.Numero_Compte == attNonPaiementEffet.compte && l.Numero_Cheque == attNonPaiementEffet.nooper && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
                        if (!VerifierLettreExiste(typeLettre, attNonPaiementEffet.compte, attNonPaiementEffet.nooper))
                        {
                            report.SetParameterValue("MyReportParameter", attNonPaiementEffet.nooper);
                            report.Prepare();

                            // Génération de la lettre d'injonction
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                PDFSimpleExport pdfExport = new PDFSimpleExport();
                                pdfExport.Export(report.Report, memoryStream);

                                fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + attNonPaiementEffet.compte.Substring(1, 6) + "_" + attNonPaiementEffet.nooper + ".pdf";
                                filePath = lettreDirectory + fileName;

                                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                                {
                                    memoryStream.WriteTo(fileStream);
                                }
                            }

                            // Enregistrement en base de données de l'information sur la lettre générée
                            var lettre = new TLettre(Guid.NewGuid(), attNonPaiementEffet.nooper, attNonPaiementEffet.compte.Substring(1, 6), attNonPaiementEffet.compte, attNonPaiementEffet.nooper,
                                attNonPaiementEffet.montant, attNonPaiementEffet.datrej, DateTime.Now, filePath, typeLettre, fileName, "");

                            _repositoryFactory.LettreRepository.Add(lettre);

                            bool res = _repositoryFactory.LettreRepository.UnitOfWork.SaveEntitiesAsync().Result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            });
        }

        public async Task<List<String>> GenererLotAttNonPaiementEffet()
        {
            List<string> result = new List<string>();
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "AttNonPaiementEffet";
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    //IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    //if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    if (!VerifierLettreLotExiste(typeLettre))
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;
                        report.Prepare();

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            PDFSimpleExport pdfExport = new PDFSimpleExport();
                            pdfExport.Export(report.Report, memoryStream);

                            fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
                            filePath = lettreDirectory + fileName;
                            result.Add($@"{typeLettre}\{fileName}");

                            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                            {
                                memoryStream.WriteTo(fileStream);
                            }
                        }

                        // Enregistrement en base de données de l'information sur la lettre générée
                        var lettre = new TLettreLot(Guid.NewGuid(), DateTime.Now, filePath, typeLettre, fileName);
                        _repositoryFactory.LettreLotRepository.Add(lettre);
                        bool res = _repositoryFactory.LettreLotRepository.UnitOfWork.SaveEntitiesAsync().Result;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return result;
        }

        public async Task<List<String>> GenererLotAttPaiementCheques()
        {
            List<string> result = new List<string>();
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "AttPaiementCheque";
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    //IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    //if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    if (!VerifierLettreLotExiste(typeLettre))
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;
                        report.Prepare();

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            PDFSimpleExport pdfExport = new PDFSimpleExport();
                            pdfExport.Export(report.Report, memoryStream);

                            fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
                            filePath = lettreDirectory + fileName;
                            result.Add($@"{typeLettre}\{fileName}");

                            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                            {
                                memoryStream.WriteTo(fileStream);
                            }
                        }

                        // Enregistrement en base de données de l'information sur la lettre générée
                        var lettre = new TLettreLot(Guid.NewGuid(), DateTime.Now, filePath, typeLettre, fileName);
                        _repositoryFactory.LettreLotRepository.Add(lettre);
                        bool res = _repositoryFactory.LettreLotRepository.UnitOfWork.SaveEntitiesAsync().Result;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
            return result;
        }

        public async Task GenererAttPaiementCheques()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "AttPaiementCheque";

                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    if (!Directory.Exists(lettreDirectory))
                        Directory.CreateDirectory(lettreDirectory);

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'injonction
                    report.Dictionary.Connections[0].ConnectionString = _oracleConnectionString;

                    IEnumerable<AttPaiementChequesViewModel> attPaiementCheques = GetAttPaiementCheques().Result;
                    //IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var attPaiementCheque in attPaiementCheques)
                    {
                        //if (lettres.Where(l => l.Numero_Compte == attPaiementCheque.compte && l.Numero_Cheque == attPaiementCheque.nooper && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
                        if (!VerifierLettreExiste(typeLettre, attPaiementCheque.compte, attPaiementCheque.nooper))
                        {
                            report.SetParameterValue("MyReportParameter", attPaiementCheque.nooper);
                            report.Prepare();

                            // Génération de la lettre d'injonction
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                PDFSimpleExport pdfExport = new PDFSimpleExport();
                                pdfExport.Export(report.Report, memoryStream);

                                fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + attPaiementCheque.compte.Substring(1, 6) + "_" + attPaiementCheque.nooper + ".pdf";
                                filePath = lettreDirectory + fileName;

                                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                                {
                                    memoryStream.WriteTo(fileStream);
                                }
                            }

                            // Enregistrement en base de données de l'information sur la lettre générée
                            var lettre = new TLettre(Guid.NewGuid(), attPaiementCheque.nooper, attPaiementCheque.compte.Substring(1, 6), attPaiementCheque.compte, attPaiementCheque.nooper,
                                attPaiementCheque.montchq, attPaiementCheque.date_regularisation, DateTime.Now, filePath, typeLettre, fileName, "");

                            _repositoryFactory.LettreRepository.Add(lettre);

                            bool res = _repositoryFactory.LettreRepository.UnitOfWork.SaveEntitiesAsync().Result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            });
        }

        public async Task <IEnumerable<LettreViewModel>> GetLettres(string typeLettre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    if (String.IsNullOrEmpty(typeLettre))
                    {
                        return await connection.QueryAsync<LettreViewModel>(@"select * from V_ListeLettres order by Date_Generation desc");
                    }
                    else
                    {
                        return await connection.QueryAsync<LettreViewModel>(@"select * from V_ListeLettres where Type_Lettre = @typeLettre order by Date_Generation desc", new { typeLettre });
                    }                  
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
            }
        }

        public async Task<IEnumerable<LettreLotViewModel>> GetLettresLot(string typeLettre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    if (String.IsNullOrEmpty(typeLettre))
                    {
                        return await connection.QueryAsync<LettreLotViewModel>(@"select * from V_ListeLettresLot");
                    }
                    else
                    {
                        return await connection.QueryAsync<LettreLotViewModel>(@"select * from V_ListeLettresLot where Type_Lettre = @typeLettre", new { typeLettre });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
        
        public async Task<IEnumerable<AttNonPaiementEffetViewModel>> GetAttNonPaiementEffet()
        {
            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return await _dbConnection.QueryAsync<AttNonPaiementEffetViewModel>(@"select * from V_CIP_LTEFFCERTIFNONPAIE");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
                    _dbConnection.Close();
            }
        }

        public async Task<IEnumerable<AttPaiementChequesViewModel>> GetAttPaiementCheques()
        {
            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return await _dbConnection.QueryAsync<AttPaiementChequesViewModel>(@"select * from V_CIP_LTATTPAIE");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
                    _dbConnection.Close();
            }
        }
    }
}
