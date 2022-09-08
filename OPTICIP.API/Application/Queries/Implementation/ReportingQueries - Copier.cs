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
        string _reportingDirectory = string.Empty;

        public ReportingQueries(IDbConnection dbConnection, IRepositoryFactory repositoryFactory, string constr, string reportingDirectory)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
            _reportingDirectory = reportingDirectory;
        }

        public async Task<IEnumerable<AvertViewModel>> GetChequesEnAvertissement()
        {
            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return await _dbConnection.QueryAsync<AvertViewModel>(@"select * from v_cip_ltavert");
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
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return await _dbConnection.QueryAsync<InjViewModel>(@"select * from v_cip_ltinj");
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
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return await _dbConnection.QueryAsync<InfMandViewModel>(@"select * from v_cip_ltinfomand where type_incident = 'INJ'");
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
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return await _dbConnection.QueryAsync<InfMandViewModel>(@"select * from v_cip_ltinfomand where type_incident = 'INF'");
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
                                    
                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();         
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'avertissement
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
                    IEnumerable<AvertViewModel> chequesAvertissement = GetChequesEnAvertissement().Result; // Lettres à générées
                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var chequeAve in chequesAvertissement)
                    {                  
                        if (lettres.Where(l => (l.Numero_Compte == chequeAve.compte && l.Numero_Cheque == chequeAve.numcheq && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)  // Contrôle si la lettre n'a pas été déjà générée
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'injonction
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;

                    IEnumerable<InjViewModel> chequesInj = GetChequesEnInjonction().Result;
                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var chequeInj in chequesInj)
                    {
                        if (lettres.Where(l => l.Numero_Compte == chequeInj.compte && l.Numero_Cheque == chequeInj.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'injonction
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;

                    IEnumerable<InfraViewModel> chequesInf = GetChequesEnInfraction().Result;
                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var chequeInf in chequesInf)
                    {
                        if (lettres.Where(l => l.Numero_Compte == chequeInf.compte && l.Numero_Cheque == chequeInf.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx");
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;

                    IEnumerable<InfMandViewModel> mandsChequesInf = GetMandatairesDesChequesEnInjonction().Result;
                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var mandChequesInf in mandsChequesInf)
                    {
                        if (lettres.Where(l => l.IDP == mandChequesInf.idp && l.Numero_Compte == mandChequesInf.compte 
                                            && l.Numero_Cheque == mandChequesInf.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx");
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;

                    IEnumerable<InfMandViewModel> mandsChequesInf = GetMandatairesDesChequesEnInfraction().Result;
                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var mandChequesInf in mandsChequesInf)
                    {
                        if (lettres.Where(l => l.IDP == mandChequesInf.idp && l.Numero_Compte == mandChequesInf.compte
                                            && l.Numero_Cheque == mandChequesInf.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
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

                FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                Report report = new Report();

                IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                {
                    // Chargement du template de la lettre d'avertissement
                    report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
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

                FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                Report report = new Report();

                IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                {
                    // Chargement du template de la lettre d'avertissement
                    report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx");
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;

                    IEnumerable<CertNonPaiementViewModel> enrAGeneres = GetDonneesCertNonPaiement().Result;
                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var enr in enrAGeneres)
                    {
                        if (lettres.Where(l => l.Numero_Compte == enr.compte && l.Numero_Cheque == enr.chqref && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
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

        public async Task GenererLotCertNonPaiements()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "CertNonPaiement";
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
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

        public async Task GenererAttNonPaiementEffet()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "AttNonPaiementEffet";

                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'injonction
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;

                    IEnumerable<AttNonPaiementEffetViewModel> attNonPaiementEffets = GetAttNonPaiementEffet().Result;
                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var attNonPaiementEffet in attNonPaiementEffets)
                    {
                        if (lettres.Where(l => l.Numero_Compte == attNonPaiementEffet.compte && l.Numero_Cheque == attNonPaiementEffet.nooper && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
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

        public async Task GenererLotAttNonPaiementEffet()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "AttNonPaiementEffet";
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
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

        public async Task GenererLotAttPaiementCheques()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "AttPaiementCheque";
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
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

        public async Task GenererAttPaiementCheques()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "AttPaiementCheque";

                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'injonction
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;

                    IEnumerable<AttPaiementChequesViewModel> attPaiementCheques = GetAttPaiementCheques().Result;
                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var attPaiementCheque in attPaiementCheques)
                    {
                        if (lettres.Where(l => l.Numero_Compte == attPaiementCheque.compte && l.Numero_Cheque == attPaiementCheque.nooper && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
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
        
        public async Task<IEnumerable<InfraViewModel>> GetChequesEnInfraction()
        {
            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return await _dbConnection.QueryAsync<InfraViewModel>(@"select * from v_cip_ltinfra");
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

        //==> Pour la saisie des destinataires
        public async Task RecupererDonneesIncidents()
        {
            await Task.Run(() =>
            {
                try
                {
                    CIPReportContext dbCtx = new CIPReportContext();
                    DateTime dateInsert = DateTime.Now;
                    //IEnumerable<AvertViewModel> chequesAvert = GetChequesEnAvertissement().Result;
                    List<DonneesIncidentChq> chequesAvertXcip  = GetIncidentChequesPourXcip<IEnumerable<AvertViewModel>>(GetChequesEnAvertissement(), 0 /*Avertissement*/, dateInsert);
                    //IEnumerable<InfraViewModel> chequesInfra = GetChequesEnInfraction().Result;
                    List<DonneesIncidentChq> chequesInfraXcip = GetIncidentChequesPourXcip<IEnumerable<InfraViewModel>>(GetChequesEnInfraction(), 1 /*Infraction*/, dateInsert);
                    //IEnumerable<InjViewModel> chequesInjonc = GetChequesEnInjonction().Result;
                    List<DonneesIncidentChq> chequesInjXcip = GetIncidentChequesPourXcip<IEnumerable<InjViewModel>>(GetChequesEnInjonction(), 2 /*Injonction*/, dateInsert);
                    //IEnumerable<InfMandViewModel> mandInfra = GetMandatairesDesChequesEnInfraction().Result;
                    List<DonneesMandataireIncident> mandInfraXcip = GetMandataireIncidentChequesPourXcip<IEnumerable<InfMandViewModel>>(GetMandatairesDesChequesEnInfraction(), 1 /*Infraction*/, dateInsert);
                    //IEnumerable<InfMandViewModel> mandInj = GetMandatairesDesChequesEnInjonction().Result;
                    List<DonneesMandataireIncident> mandInjXcip = GetMandataireIncidentChequesPourXcip<IEnumerable<InfMandViewModel>>(GetMandatairesDesChequesEnInjonction(), 2 /*Injonction*/, dateInsert);

                    //IEnumerable<AttNonPaiementEffetViewModel> attNonPaieEffet = GetAttNonPaiementEffet().Result;
                    List<AttNonPaiementEffet> attNonPaieEffetXcip = GetAttNonPaiementEffetPourXcip(dateInsert);
                    //IEnumerable<AttPaiementChequesViewModel> attPaieChq = GetAttPaiementCheques().Result;
                    List<AttNonPaiementCheque> attNonPaieCheqXcip = GetAttNonPaiementChequePourXcip(dateInsert);
                    //IEnumerable<CertNonPaiementViewModel> certNonPaie = GetDonneesCertNonPaiement().Result;
                    List<CertificatNonPaiement> CertificatNonPaieXcip = GetCertificatNonPaiementPourXcip(dateInsert);

                    dbCtx.AddRange(chequesAvertXcip);
                    dbCtx.AddRange(chequesInfraXcip);
                    dbCtx.AddRange(chequesInjXcip);
                    dbCtx.AddRange(mandInfraXcip);
                    dbCtx.AddRange(mandInjXcip);

                    dbCtx.AddRange(attNonPaieEffetXcip);
                    dbCtx.AddRange(attNonPaieCheqXcip);
                    dbCtx.AddRange(CertificatNonPaieXcip);

                    dbCtx.SaveChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            });
        }

        public List<DonneesIncidentChq> GetIncidentChequesPourXcip<T>(Task<T> pTask, int pTypeIncident, DateTime pDateInsert)
        {
            //IEnumerable<AvertViewModel> chequesAvert = GetChequesEnAvertissement().Result;
            T incidentCheques = pTask.Result;
            List<DonneesIncidentChq> incidChequeXcip = Utilitaires.CastObject<List<DonneesIncidentChq>, T>(incidentCheques);
            //chequesAvertXcip.Select(c => { c.Id = Guid.NewGuid(); c.TypeIncident = 0; c.DateInsertion = dateInsert; return c; }).ToList();
            for (int i = incidChequeXcip.Count - 1; i >= 0; i--)
            {
                DonneesIncidentChq data = incidChequeXcip[i];
                if (CheckExists($@"Select count(*) FROM DonneesIncidentChq Where Compte = '{data.compte}' AND Numcheq = '{data.numcheq}' AND TypeIncident = {pTypeIncident}") > 0)
                    incidChequeXcip.RemoveAt(i);
                else
                {
                    data.id = Guid.NewGuid();
                    data.typeIncident = pTypeIncident;
                    data.dateInsertion = pDateInsert;
                }
            }
            return incidChequeXcip;
        }

        public List<DonneesMandataireIncident> GetMandataireIncidentChequesPourXcip<T>(Task<T> pTask, int pTypeIncident, DateTime pDateInsert)
        {
            //IEnumerable<AvertViewModel> chequesAvert = GetChequesEnAvertissement().Result;
            T incidentCheques = pTask.Result;
            List<DonneesMandataireIncident> mandIncXcip = Utilitaires.CastObject<List<DonneesMandataireIncident>, T>(incidentCheques);
            //chequesAvertXcip.Select(c => { c.Id = Guid.NewGuid(); c.TypeIncident = 0; c.DateInsertion = dateInsert; return c; }).ToList();
            for (int i = mandIncXcip.Count - 1; i >= 0; i--)
            {
                DonneesMandataireIncident data = mandIncXcip[i];
                if (CheckExists($@"Select count(*) FROM DonneesMandataireIncident Where Compte = '{data.Compte}' AND Numcheq = '{data.Numcheq}' AND Type_Incident = '{pTypeIncident}'") > 0)
                    mandIncXcip.RemoveAt(i);
                else
                {
                    data.Id = Guid.NewGuid();
                    data.TypeIncident = pTypeIncident.ToString();
                    data.DateInsertion = pDateInsert;
                }
            }
            return mandIncXcip;
        }

        public List<AttNonPaiementEffet> GetAttNonPaiementEffetPourXcip(DateTime pDateInsert)
        {
            IEnumerable<AttNonPaiementEffetViewModel> attNonPaieEffet = GetAttNonPaiementEffet().Result;
            List<AttNonPaiementEffet> attNonPaieEffetXcip = Utilitaires.CastObject<List<AttNonPaiementEffet>, IEnumerable<AttNonPaiementEffetViewModel>>(attNonPaieEffet);
            for (int i = attNonPaieEffetXcip.Count - 1; i >= 0; i--)
            {
                AttNonPaiementEffet data = attNonPaieEffetXcip[i];
                if (CheckExists($@"Select count(*) FROM AttNonPaiementEffet Where Compte = '{data.Compte}' AND Nooper = '{data.Nooper}'") > 0)
                    attNonPaieEffetXcip.RemoveAt(i);
                else
                {
                    data.Id = Guid.NewGuid();
                    data.DateInsertion = pDateInsert;
                }
            }
            return attNonPaieEffetXcip;
        }

        public List<AttNonPaiementCheque> GetAttNonPaiementChequePourXcip(DateTime pDateInsert)
        {
            IEnumerable<AttPaiementChequesViewModel> attNonPaieCheq = GetAttPaiementCheques().Result;
            List<AttNonPaiementCheque> attNonPaieCheqXcip = Utilitaires.CastObject<List<AttNonPaiementCheque>, IEnumerable<AttPaiementChequesViewModel>>(attNonPaieCheq);
            for (int i = attNonPaieCheqXcip.Count - 1; i >= 0; i--)
            {
                AttNonPaiementCheque data = attNonPaieCheqXcip[i];
                if (CheckExists($@"Select count(*) FROM AttNonPaiementCheque Where Compte = '{data.Compte}' AND numcheq = '{data.Numcheq}'") > 0)
                    attNonPaieCheqXcip.RemoveAt(i);
                else
                {
                    data.Id = Guid.NewGuid();
                    data.DateInsertion = pDateInsert;
                }
            }
            return attNonPaieCheqXcip;
        }

        public List<CertificatNonPaiement> GetCertificatNonPaiementPourXcip(DateTime pDateInsert)
        {
            IEnumerable<CertNonPaiementViewModel> certNonPaie = GetDonneesCertNonPaiement().Result;
            List<CertificatNonPaiement> certNonPaieXcip = Utilitaires.CastObject<List<CertificatNonPaiement>, IEnumerable<CertNonPaiementViewModel>>(certNonPaie);
            for (int i = certNonPaieXcip.Count - 1; i >= 0; i--)
            {
                CertificatNonPaiement data = certNonPaieXcip[i];
                if (CheckExists($@"Select count(*) FROM CertificatNonPaiement Where Compte = '{data.Compte}' AND Nooper = '{data.Nooper}'") > 0)
                    certNonPaieXcip.RemoveAt(i);
                else
                {
                    data.Id = Guid.NewGuid();
                    data.DateInsertion = pDateInsert;
                }
            }
            return certNonPaieXcip;
        }

        public int CheckExists(String pQuery)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    int iret = (int)connection.ExecuteScalar(pQuery);
                    return iret;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public async Task GenererLettresIncident(Guid idIncident)
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "Avertissement";
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'avertissement
                    //report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
                    IEnumerable<DonneesIncidentChq> chequesAvertissement = GetIncidentChequesXcip(idIncident).Result; // Lettres à générées
                    //IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;
                    report.Dictionary.Clear();
                    report.RegisterData(chequesAvertissement, "D");
                    //report.Prepare();
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PDFSimpleExport pdfExport = new PDFSimpleExport();
                        pdfExport.Export(report.Report, memoryStream);
                        fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + idIncident.ToString() + ".pdf";
                        filePath = lettreDirectory + fileName;

                        using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                        {
                            memoryStream.WriteTo(fileStream);
                        }
                    }
                
                    //foreach (var chequeAve in chequesAvertissement)
                    //{
                    //    //if (lettres.Where(l => (l.Numero_Compte == chequeAve.compte && l.Numero_Cheque == chequeAve.numcheq && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)  // Contrôle si la lettre n'a pas été déjà générée
                    //    //{
                    //    //    report.SetParameterValue("MyReportParameter", chequeAve.numcheq);
                    //    //    report.Prepare();
                    //    report.RegisterData(chequeAve, "Employees");
                    //    // Génération de la lettre d'avertissement
                    //    using (MemoryStream memoryStream = new MemoryStream())
                    //        {
                    //            PDFSimpleExport pdfExport = new PDFSimpleExport();
                    //            pdfExport.Export(report.Report, memoryStream);

                    //            fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + chequeAve.compte.Substring(1, 6) + "_" + chequeAve.numcheq + ".pdf";
                    //            filePath = lettreDirectory + fileName;

                    //            using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                    //            {
                    //                memoryStream.WriteTo(fileStream);
                    //            }
                    //        }

                    //        // Enregistrement en base de données de l'information sur la lettre générée
                    //        var lettre = new TLettre(Guid.NewGuid(), chequeAve.nooper, chequeAve.compte.Substring(1, 6), chequeAve.compte, chequeAve.numcheq, chequeAve.montchq, chequeAve.datinc, DateTime.Now, filePath, typeLettre, fileName, "");
                    //        _repositoryFactory.LettreRepository.Add(lettre);
                    //        bool res = _repositoryFactory.LettreRepository.UnitOfWork.SaveEntitiesAsync().Result;
                    //    //}
                    //}
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            });
        }

        public async Task<IEnumerable<DonneesIncidentChq>> GetIncidentChequesXcip(Guid idIncident)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    return await conn.QueryAsync<DonneesIncidentChq>(@$"select * from DonneesIncidentChq Where Id='{idIncident}'");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool VerifierLettreExiste(int typeLettre, string compte, string cheque)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var iCount = connection.QuerySingle<int>(@$"select Count(*) from V_ListeLettres where Type_Lettre = {typeLettre} AND compte = '{compte}' AND numcheq = '{cheque}' order by Date_Generation desc");
                    return (iCount > 0);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }


        public async Task GenererLettreAvertissementFromXcip()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "Avertissement";
                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;
                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(DataConnectionBase));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'avertissement
                    report.Dictionary.Connections[0].ConnectionString = _connectionString;
                    IEnumerable<AvertViewModel> chequesAvertissement = GetChequesEnAvertissement().Result; // Lettres à générées
                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var chequeAve in chequesAvertissement)
                    {
                        if (lettres.Where(l => (l.Numero_Compte == chequeAve.compte && l.Numero_Cheque == chequeAve.numcheq && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)  // Contrôle si la lettre n'a pas été déjà générée
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'injonction
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;

                    IEnumerable<InjViewModel> chequesInj = GetChequesEnInjonction().Result;
                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var chequeInj in chequesInj)
                    {
                        if (lettres.Where(l => l.Numero_Compte == chequeInj.compte && l.Numero_Cheque == chequeInj.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'injonction
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;

                    IEnumerable<InfraViewModel> chequesInf = GetChequesEnInfraction().Result;
                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var chequeInf in chequesInf)
                    {
                        if (lettres.Where(l => l.Numero_Compte == chequeInf.compte && l.Numero_Cheque == chequeInf.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();

                    IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx");
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;

                    IEnumerable<InfMandViewModel> mandsChequesInf = GetMandatairesDesChequesEnInjonction().Result;
                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var mandChequesInf in mandsChequesInf)
                    {
                        if (lettres.Where(l => l.IDP == mandChequesInf.idp && l.Numero_Compte == mandChequesInf.compte
                                            && l.Numero_Cheque == mandChequesInf.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
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

        public async Task GenererLettreInfMandatairesInf()
        {
            await Task.Run(() =>
            {
                try
                {
                    string typeLettre = "InfMandataireInf";

                    String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                    string filePath, fileName;

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx");
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;

                    IEnumerable<InfMandViewModel> mandsChequesInf = GetMandatairesDesChequesEnInfraction().Result;
                    IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var mandChequesInf in mandsChequesInf)
                    {
                        if (lettres.Where(l => l.IDP == mandChequesInf.idp && l.Numero_Compte == mandChequesInf.compte
                                            && l.Numero_Cheque == mandChequesInf.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
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

                FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                Report report = new Report();

                IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                {
                    // Chargement du template de la lettre d'avertissement
                    report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
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

                FastReport.Utils.RegisteredObjects.AddConnection(typeof(OracleDataConnection));
                Report report = new Report();

                IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                {
                    // Chargement du template de la lettre d'avertissement
                    report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                    report.Dictionary.Connections[0].ConnectionString = _dbConnection.ConnectionString;
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
        //==
    }
}
