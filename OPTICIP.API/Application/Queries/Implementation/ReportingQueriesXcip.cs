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
        //==> Pour la saisie des destinataires
        public async Task<IEnumerable<AvertViewModel>> GetChequesEnAvertissementFromXcip(String pCompte = null, String pNumChq = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    if (pCompte == null || pNumChq == null)
                        return await conn.QueryAsync<AvertViewModel>(@"select * from v_cip_ltavert");
                    else
                        return await conn.QueryAsync<AvertViewModel>(@$"select * from v_cip_ltavert Where compte = '{pCompte}' AND numcheq = '{pNumChq}'");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<InfraViewModel>> GetChequesEnInfractionFromXcip(String pCompte = null, String pNumChq = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    if (pCompte == null || pNumChq == null)
                        return await conn.QueryAsync<InfraViewModel>(@"select * from v_cip_ltinfra");
                    else
                        return await conn.QueryAsync<InfraViewModel>(@$"select * from v_cip_ltinfra Where compte = '{pCompte}' AND numcheq = '{pNumChq}'");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IEnumerable<InjViewModel>> GetChequesEnInjonctionFromXcip(String pCompte = null, String pNumChq = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    if (pCompte == null || pNumChq == null)
                        return await conn.QueryAsync<InjViewModel>(@"select * from v_cip_ltinj");
                    else
                        return await conn.QueryAsync<InjViewModel>(@$"select * from v_cip_ltinj Where compte = '{pCompte}' AND numcheq = '{pNumChq}'");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<InfMandViewModel>> GetMandatairesDesChequesEnInjonctionFromXcip(String pCompte = null, String pNumChq = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    if (pCompte == null || pNumChq == null)
                        return await conn.QueryAsync<InfMandViewModel>(@"select * from v_cip_ltinfomand where type_incident = '2'");
                    else
                        return await conn.QueryAsync<InfMandViewModel>(@$"select * from v_cip_ltinfomand where type_incident = '2' AND compte = '{pCompte}' AND numcheq = '{pNumChq}'");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<InfMandViewModel>> GetMandatairesDesChequesEnInfractionFromXcip(String pCompte = null, String pNumChq = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    if (pCompte == null || pNumChq == null)
                        return await conn.QueryAsync<InfMandViewModel>(@"select * from v_cip_ltinfomand where type_incident = '1'");
                    else
                        return await conn.QueryAsync<InfMandViewModel>(@$"select * from v_cip_ltinfomand where type_incident = '1' AND compte = '{pCompte}' AND numcheq = '{pNumChq}'");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RecupererDonneesIncidentsFromSIB()
        {
            await Task.Run(() =>
            {
                try
                {
                    CIPReportContext dbCtx = new CIPReportContext();
                    DateTime dateInsert = DateTime.Now;
                    //IEnumerable<AvertViewModel> chequesAvert = GetChequesEnAvertissement().Result;
                    List<DonneesIncidentChq> chequesAvertXcip  = GetIncidentChequesFromSIB<IEnumerable<AvertViewModel>>(GetChequesEnAvertissement(), 0 /*Avertissement*/, dateInsert);
                    //IEnumerable<InfraViewModel> chequesInfra = GetChequesEnInfraction().Result;
                    List<DonneesIncidentChq> chequesInfraXcip = GetIncidentChequesFromSIB<IEnumerable<InfraViewModel>>(GetChequesEnInfraction(), 1 /*Infraction*/, dateInsert);
                    //IEnumerable<InjViewModel> chequesInjonc = GetChequesEnInjonction().Result;
                    List<DonneesIncidentChq> chequesInjXcip = GetIncidentChequesFromSIB<IEnumerable<InjViewModel>>(GetChequesEnInjonction(), 2 /*Injonction*/, dateInsert);
                    //IEnumerable<InfMandViewModel> mandInfra = GetMandatairesDesChequesEnInfraction().Result;
                    List<DonneesMandataireIncident> mandInfraXcip = GetMandataireIncidentChequesFromSIB<IEnumerable<InfMandViewModel>>(GetMandatairesDesChequesEnInfraction(), 1 /*Infraction*/, dateInsert);
                    //IEnumerable<InfMandViewModel> mandInj = GetMandatairesDesChequesEnInjonction().Result;
                    List<DonneesMandataireIncident> mandInjXcip = GetMandataireIncidentChequesFromSIB<IEnumerable<InfMandViewModel>>(GetMandatairesDesChequesEnInjonction(), 2 /*Injonction*/, dateInsert);

                    ////IEnumerable<AttNonPaiementEffetViewModel> attNonPaieEffet = GetAttNonPaiementEffet().Result;
                    //List<AttNonPaiementEffet> attNonPaieEffetXcip = GetAttNonPaiementEffetFromSIB(dateInsert);
                    ////IEnumerable<AttPaiementChequesViewModel> attPaieChq = GetAttPaiementCheques().Result;
                    //List<AttNonPaiementCheque> attNonPaieCheqXcip = GetAttNonPaiementChequeFromSIB(dateInsert);
                    ////IEnumerable<CertNonPaiementViewModel> certNonPaie = GetDonneesCertNonPaiement().Result;
                    //List<CertificatNonPaiement> CertificatNonPaieXcip = GetCertificatNonPaiementFromSIB(dateInsert);

                    dbCtx.AddRange(chequesAvertXcip);
                    dbCtx.AddRange(chequesInfraXcip);
                    dbCtx.AddRange(chequesInjXcip);
                    dbCtx.AddRange(mandInfraXcip);
                    dbCtx.AddRange(mandInjXcip);

                    //dbCtx.AddRange(attNonPaieEffetXcip);
                    //dbCtx.AddRange(attNonPaieCheqXcip);
                    //dbCtx.AddRange(CertificatNonPaieXcip);

                    dbCtx.SaveChanges();

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            });
        }

        public List<DonneesIncidentChq> GetIncidentChequesFromSIB<T>(Task<T> pTask, int pTypeIncident, DateTime pDateInsert)
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

        public List<DonneesMandataireIncident> GetMandataireIncidentChequesFromSIB<T>(Task<T> pTask, int pTypeIncident, DateTime pDateInsert)
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

        public List<AttNonPaiementEffet> GetAttNonPaiementEffetFromSIB(DateTime pDateInsert)
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

        public List<AttNonPaiementCheque> GetAttNonPaiementChequeFromSIB(DateTime pDateInsert)
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

        public List<CertificatNonPaiement> GetCertificatNonPaiementFromSIB(DateTime pDateInsert)
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

        public async Task<IEnumerable<DonneesIncidentChq>> GetIncidentChequesFromXcip(Guid idIncident)
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

        public bool VerifierLettreExiste(string typeLettre, string compte, string cheque)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var iCount = connection.QuerySingle<int>(@$"select Count(*) from V_ListeLettres where Type_Lettre = '{typeLettre}' AND Numero_Compte = '{compte}' AND Numero_Cheque = '{cheque}'");
                    return (iCount > 0);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public bool VerifierLettreMandataireExiste(string typeLettre, string idp, string compte, string cheque)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var iCount = connection.QuerySingle<int>(@$"select Count(*) from V_ListeLettres where Type_Lettre = '{typeLettre}' AND Numero_Compte = '{compte}' AND Numero_Cheque = '{cheque}' AND idp = '{idp}'");
                    return (iCount > 0);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public bool VerifierLettreLotExiste(string typeLettre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    var iCount = connection.QuerySingle<int>(@$"select Count(*) from V_ListeLettresLot where Type_Lettre = '{typeLettre}' AND CONVERT(DATE, Date_Generation) = CONVERT(DATE, GETDATE())");
                    return (iCount > 0);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        //-----
        public async Task GenererLettreAvertissementFromXcip(String pCompte = null, String pNumChq = null)
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'avertissement
                    report.Dictionary.Connections[0].ConnectionString = _connectionString;
                    IEnumerable<AvertViewModel> chequesAvertissement = GetChequesEnAvertissementFromXcip(pCompte, pNumChq).Result; // Lettres à générées
                    //IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var chequeAve in chequesAvertissement)
                    {
                        //if (lettres.Where(l => (l.Numero_Compte == chequeAve.compte && l.Numero_Cheque == chequeAve.numcheq && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)  // Contrôle si la lettre n'a pas été déjà générée
                        if (pCompte!=null || !VerifierLettreExiste(typeLettre, chequeAve.compte, chequeAve.numcheq))
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

        public async Task GenererLotLettreAvertissementFromXcip()
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
                    Report report = new Report();

//                    IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    //if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    if (!VerifierLettreLotExiste(typeLettre)) //==> Pas de lot générer à la date du jour
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _connectionString;
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

        public async Task GenererLettreInjonctionFromXcip(String pCompte = null, String pNumChq = null)
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'injonction
                    report.Dictionary.Connections[0].ConnectionString = _connectionString;

                    IEnumerable<InjViewModel> chequesInj = GetChequesEnInjonctionFromXcip(pCompte ,pNumChq).Result;
                    //IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var chequeInj in chequesInj)
                    {
                        if (pCompte!=null || !VerifierLettreExiste(typeLettre, chequeInj.compte, chequeInj.numcheq))
                        //if (lettres.Where(l => l.Numero_Compte == chequeInj.compte && l.Numero_Cheque == chequeInj.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
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

        public async Task GenererLotLettreInjonctionFromXcip()
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
                    Report report = new Report();

                    //IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    //if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    if (!VerifierLettreLotExiste(typeLettre)) //==> Pas de lot générer à la date du jour
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _connectionString;
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

        public async Task GenererLettresEnInfractionFromXcip(String pCompte = null, String pNumChq = null)
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx"); // Chargement du template de la lettre d'injonction
                    report.Dictionary.Connections[0].ConnectionString = _connectionString;

                    IEnumerable<InfraViewModel> chequesInf = GetChequesEnInfractionFromXcip(pCompte, pNumChq).Result;
                    //IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var chequeInf in chequesInf)
                    {
                        //if (lettres.Where(l => l.Numero_Compte == chequeInf.compte && l.Numero_Cheque == chequeInf.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
                        if (pCompte!=null || !VerifierLettreExiste(typeLettre, chequeInf.compte, chequeInf.numcheq))
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

        public async Task GenererLotLettresEnInfractionFromXcip()
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
                    Report report = new Report();

                    //IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                    //if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                    if (!VerifierLettreLotExiste(typeLettre)) //==> Pas de lot générer à la date du jour
                    {
                        // Chargement du template de la lettre d'avertissement
                        report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                        report.Dictionary.Connections[0].ConnectionString = _connectionString;
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

        public async Task GenererLettreInfMandatairesInjFromXcip(String pCompte = null, String pNumChq = null)
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx");
                    report.Dictionary.Connections[0].ConnectionString = _connectionString;

                    IEnumerable<InfMandViewModel> mandsChequesInj = GetMandatairesDesChequesEnInjonctionFromXcip(pCompte, pNumChq).Result;
                    //IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var mandChequesInj in mandsChequesInj)
                    {
                        //if (lettres.Where(l => l.IDP == mandChequesInf.idp && l.Numero_Compte == mandChequesInf.compte
                        //                    && l.Numero_Cheque == mandChequesInf.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
                        if (pCompte!=null || !VerifierLettreMandataireExiste(typeLettre, mandChequesInj.idp, mandChequesInj.compte, mandChequesInj.numcheq))
                        {
                            report.SetParameterValue("MyReportParameter", mandChequesInj.numcheq);
                            report.SetParameterValue("MyReportParameterIDP", mandChequesInj.idp);
                            report.Prepare();

                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                PDFSimpleExport pdfExport = new PDFSimpleExport();
                                pdfExport.Export(report.Report, memoryStream);

                                fileName = typeLettre + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + mandChequesInj.compte.Substring(1, 6) + "_"
                                            + mandChequesInj.idp + "_" + mandChequesInj.numcheq + ".pdf";
                                filePath = lettreDirectory + fileName;

                                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                                {
                                    memoryStream.WriteTo(fileStream);
                                }
                            }

                            var lettre = new TLettre(Guid.NewGuid(), mandChequesInj.nooper, mandChequesInj.compte.Substring(1, 6), mandChequesInj.compte, mandChequesInj.numcheq,
                                                        mandChequesInj.montchq, mandChequesInj.datinc, DateTime.Now, filePath, typeLettre, fileName, mandChequesInj.idp);
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

        public async Task GenererLettreInfMandatairesInfFromXcip(String pCompte = null, String pNumChq = null)
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

                    FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
                    Report report = new Report();
                    report.Load(_reportingDirectory + typeLettre + ".frx");
                    report.Dictionary.Connections[0].ConnectionString = _connectionString;

                    IEnumerable<InfMandViewModel> mandsChequesInf = GetMandatairesDesChequesEnInfractionFromXcip(pCompte, pNumChq).Result;
                    //IEnumerable<LettreViewModel> lettres = GetLettres(typeLettre).Result;

                    foreach (var mandChequesInf in mandsChequesInf)
                    {
                        //if (lettres.Where(l => l.IDP == mandChequesInf.idp && l.Numero_Compte == mandChequesInf.compte
                        //                    && l.Numero_Cheque == mandChequesInf.numcheq && l.Type_Lettre == typeLettre).FirstOrDefault() == null)
                        if (pCompte!=null || !VerifierLettreMandataireExiste(typeLettre, mandChequesInf.idp, mandChequesInf.compte, mandChequesInf.numcheq))
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

        public async Task GenererLotLettreInfMandatairesInfFromXcip()
        {
            await Task.Run(() =>
            {
                LotLettreInfMandatairesInfFromXcip();
            });
        }

        private void LotLettreInfMandatairesInfFromXcip()
        {
            try
            {
                string typeLettre = "InfMandataireInf";
                String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                string filePath, fileName;

                if (!Directory.Exists(lettreDirectory))
                    Directory.CreateDirectory(lettreDirectory);

                FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
                Report report = new Report();

                //IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                //if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                if (!VerifierLettreLotExiste(typeLettre))
                {
                    // Chargement du template de la lettre d'avertissement
                    report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                    report.Dictionary.Connections[0].ConnectionString = _connectionString;
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

        public async Task GenererLotLettreInfMandatairesInjFromXcip()
        {
            await Task.Run(() =>
            {
                LotLettreInfMandatairesInjFromXcip();
            });
        }

        private void LotLettreInfMandatairesInjFromXcip()
        {
            try
            {
                string typeLettre = "InfMandataireInj";
                String lettreDirectory = _reportingDirectory + typeLettre + @"\";
                string filePath, fileName;

                if (!Directory.Exists(lettreDirectory))
                    Directory.CreateDirectory(lettreDirectory);

                FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
                Report report = new Report();

                //IEnumerable<LettreLotViewModel> lettres = GetLettresLot(typeLettre).Result;

                //if (lettres.Where(l => (l.Date_Generation.ToShortDateString() == DateTime.Now.ToShortDateString() && l.Type_Lettre == typeLettre)).FirstOrDefault() == null)
                if (! VerifierLettreLotExiste(typeLettre))
                {
                    // Chargement du template de la lettre d'avertissement
                    report.Load(_reportingDirectory + typeLettre + "Lot.frx");
                    report.Dictionary.Connections[0].ConnectionString = _connectionString;
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
