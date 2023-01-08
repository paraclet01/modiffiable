﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPTICIP.API.Application.Commands.DeclarationCommands;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

namespace OPTICIP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DeclarationApiController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDeclarationQueries _declarationQueries;
        private readonly IPreparationQueries _preparationQueries;
        private readonly IParametresQuerie _parametresQueries;
        private readonly IRetourQueries _retourQueries;
        private readonly IFileReader _fileReader;

        public DeclarationApiController(IMediator mediator, IDeclarationQueries declarationsQueries, IPreparationQueries preparationQueries,
            IParametresQuerie parametresQueries, IRetourQueries retourQueries, IFileReader fileReader)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _declarationQueries = declarationsQueries ?? throw new ArgumentNullException(nameof(declarationsQueries));
            _preparationQueries = preparationQueries ?? throw new ArgumentNullException(nameof(preparationQueries));
            _parametresQueries = parametresQueries ?? throw new ArgumentNullException(nameof(parametresQueries));
            _retourQueries = retourQueries ?? throw new ArgumentNullException(nameof(retourQueries));
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }

        private async Task<object> GetInfo(string agence, string table)
        {
            var retire = new object();
            var actif = new object();
            var erreur = new object();
            switch (table)
            {
                case "Compte":
                    retire = await _declarationQueries.GetComptesAsync(agence, "C");
                    actif = await _declarationQueries.GetComptesAsync(agence);
                    return new { retire, actif };
                case "Pers_Physique":
                    retire = await _declarationQueries.GetPersPhysiquesAsync(agence, "C");
                    actif = await _declarationQueries.GetPersPhysiquesAsync(agence);
                    erreur = await _declarationQueries.GetPersPhysiquesErreursAsync(agence);
                    return new { retire, actif, erreur };
                case "Pers_Morale":
                    retire = await _declarationQueries.GetPersMoralesAsync(agence, "C");
                    actif = await _declarationQueries.GetPersMoralesAsync(agence);
                    erreur = await _declarationQueries.GetPersMoralesErreursAsync(agence);
                    return new { retire, actif, erreur };
                case "IncidentCheque":
                    retire = await _declarationQueries.GetIncidentsChequesAsync(agence, "C");
                    actif = await _declarationQueries.GetIncidentsChequesAsync(agence);
                    return new { retire, actif };
                case "ChequeIrregulier":
                    retire = await _declarationQueries.GetChequesIrreguliersAsync(agence, "C");
                    actif = await _declarationQueries.GetChequesIrreguliersAsync(agence);
                    return new { retire, actif };
                case "IncidentEffet":
                    retire = await _declarationQueries.GetIncidentsEffetsAsync(agence, "C");
                    actif = await _declarationQueries.GetIncidentsEffetsAsync(agence);
                    return new { retire, actif };
                default:
                    return new { };
            }
        }

        private async Task<object> GetErrorsInfo(string agence, string table)
        {
            var data = new object();
            switch (table)
            {
                case "Pers_Physique":
                    data = await _declarationQueries.GetPersPhysiquesErreursAsync(agence);
                    return new { data };
                case "Pers_Morale":
                    data = await _declarationQueries.GetPersMoralesErreursAsync(agence);
                    return new { data };

                default:
                    return new { };
            }
        }


        [HttpGet]
        [Route("ListComptes")]
        [ProducesResponseType(typeof(IEnumerable<CompteViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListComptes(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetComptesAsync(agence, "C");
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListComptesRetires")]
        [ProducesResponseType(typeof(IEnumerable<CompteViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListComptesRetires(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetComptesAsync(agence);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListPersPhysiques")]
        [ProducesResponseType(typeof(IEnumerable<PersPhysiqueViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListPersPhysiques(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetPersPhysiquesAsync(agence, "C");
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListPersPhysiquesRetires")]
        [ProducesResponseType(typeof(IEnumerable<PersPhysiqueViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListPersPhysiquesRetires(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetPersPhysiquesAsync(agence);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListPersPhysiquesErreurs")]
        [ProducesResponseType(typeof(IEnumerable<PersPhysiqueErreurViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListPersPhysiquesErreurs(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetPersPhysiquesErreursAsync(agence);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetDonneeErreur")]
        [ProducesResponseType(typeof(IEnumerable<ErreurEnregistrementViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListErreurs(string enregistrementID, string enregistrementTable)
        {
            try
            {
                var result = await _declarationQueries.GetListErreurs(enregistrementID, enregistrementTable);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListPersMorales")]
        [ProducesResponseType(typeof(IEnumerable<PersMoraleViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListPersMorales(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetPersMoralesAsync(agence, "C");
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListPersMoralesEnErreur")]
        [ProducesResponseType(typeof(IEnumerable<PersMoraleErreurViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListPersMoralesEnErreur(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetPersMoralesErreursAsync(agence);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListPersMoralesRetires")]
        [ProducesResponseType(typeof(IEnumerable<PersMoraleViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListPersMoralesRetires(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetPersMoralesAsync(agence);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListCartes")]
        [ProducesResponseType(typeof(IEnumerable<CartesViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListCartes(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetCartesAsync(agence, "C");
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListCartesRetires")]
        [ProducesResponseType(typeof(IEnumerable<CartesViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListCartesRetires(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetCartesAsync(agence);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListIncidentsCheques")]
        [ProducesResponseType(typeof(IEnumerable<IncidentsChequesViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListIncidentsCheques(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetIncidentsChequesAsync(agence, "C");
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListIncidentsChequesRetires")]
        [ProducesResponseType(typeof(IEnumerable<IncidentsChequesViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListIncidentsChequesRetires(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetIncidentsChequesAsync(agence);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListChequesIrreguliers")]
        [ProducesResponseType(typeof(IEnumerable<ChequesIrreguliersViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListChequesIrreguliers(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetChequesIrreguliersAsync(agence, "C");
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListChequesIrreguliersRetires")]
        [ProducesResponseType(typeof(IEnumerable<ChequesIrreguliersViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListChequesIrreguliersRetires(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetChequesIrreguliersAsync(agence);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListIncidentsEffets")]
        [ProducesResponseType(typeof(IEnumerable<IncidentsEffetsViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListIncidentsEffets(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetIncidentsEffetsAsync(agence, "C");
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListIncidentsEffetsRetires")]
        [ProducesResponseType(typeof(IEnumerable<IncidentsEffetsViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListIncidentsEffetsRetires(string agence)
        {
            try
            {
                var result = await _declarationQueries.GetIncidentsEffetsAsync(agence);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("CreateDonneeRetire")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostCreateDonneeRetire([FromBody] CreateDonneeRetireCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                bool commandResult = false;
                //if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
                //{
                //    commandResult = await _mediator.Send(command);
                //}
                commandResult = await _mediator.Send(command);
                var retour = await GetInfo(command.Agence, command.Table_Item);

                return commandResult ? (IActionResult)Ok(retour) : (IActionResult)BadRequest();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("PurgerDonneeErreur")]
        [ProducesResponseType(typeof(IEnumerable<object>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAllErrorsData(string table, string agence)
        {
            try
            {
                await _declarationQueries.DeleteAllErrorsDatasAsync(table, agence);
                var retour = await GetErrorsInfo(agence, table);

                return Ok(retour);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("DeleteDonneeRetire")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostDeleteDonneeRetire([FromBody] DeleteDonneeRetireCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                bool commandResult = false;
                commandResult = await _mediator.Send(command);
                var retour = await GetInfo(command.Agence, command.Table_Item);

                return commandResult ? (IActionResult)Ok(retour) : (IActionResult)BadRequest();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("DeleteDonneeErreur")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostDeleteDonneeErreur([FromBody] DeleteDonneeErreurCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                bool commandResult = false;
                commandResult = await _mediator.Send(command);

                return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListDonneesADeclarer_Old")]
        [ProducesResponseType(typeof(IEnumerable<DeclarationsViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListDonneesADeclarer_Old(string agence, String nbrecompte, string userID, bool declarationInitiale = false)
        {
            try
            {
                int nbreCompteFinal;
                agence = String.IsNullOrEmpty(agence) ? "" : agence;

                if (agence != "ManyFiles")
                {
                    // nombre de comptes à déclarer calculé à partir des enregistrements préparés dans la base
                    nbreCompteFinal = _declarationQueries.GetNbreCompteETC(agence, declarationInitiale == false ? 0 : 1);

                    // ajout au nombre de compte déjà déclaré
                    nbreCompteFinal += Convert.ToInt32(nbrecompte);

                    // préparation des données à déclarer
                    var result = await _preparationQueries.LancerPreparationDonnees(agence, declarationInitiale);

                    // déclaration
                    var ListDonneesADeclarer = await _declarationQueries.GetDeclarationsAsync(agence, nbreCompteFinal.ToString(), declarationInitiale);

                    return Ok(ListDonneesADeclarer);
                }
                else
                {
                    string FolderName = string.Format("{0}_{1}{2}{3}{4}{5}{6}{7}", "XCIP_Declaration", DateTime.UtcNow.Day, DateTime.UtcNow.Month, DateTime.UtcNow.Year, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second, DateTime.UtcNow.Millisecond);
                    var Parametres = _parametresQueries.GetParametreByCodeAsync("ZipPath");
                    string fullPath = Path.Combine(@Parametres.Libelle, FolderName);

                    // création du répertoire de sortie
                    if (!Directory.Exists(fullPath))
                        Directory.CreateDirectory(fullPath);

                    // liste des agences
                    var lstAgence = await _parametresQueries.GetAgencesAsync();
                    string FolderNameZip = String.Format("{0}.zip", FolderName);

                    // nombre de comptes à déclarer calculé à partir des enregistrements préparés dans la base
                    nbreCompteFinal = _declarationQueries.GetNbreCompteETC("", declarationInitiale == false ? 0 : 1);

                    // ajout au nombre de compte déjà déclaré
                    nbreCompteFinal += Convert.ToInt32(nbrecompte);

                    foreach (var itemagence in lstAgence)
                    {
                        // préparation des données
                        var result = await _preparationQueries.LancerPreparationDonnees(itemagence.CodeAgencce, declarationInitiale);
                        // récupération des informations sur le fichier à générer
                        var FileDeclarationInfo = await _declarationQueries.GetFileDeclarationInfoAsync();
                        // récupération des données à déclarer
                        var ListDonneesADeclarer = await _declarationQueries.GetDeclarationsAsync(itemagence.CodeAgencce, nbreCompteFinal.ToString(), declarationInitiale);

                        string fullfilePath = Path.Combine(fullPath, FileDeclarationInfo.NomFicher);

                        using (StreamWriter file = new StreamWriter(fullfilePath, true))
                        {
                            foreach (var line in ListDonneesADeclarer)
                            {
                                file.WriteLine(line.Data);
                            }

                            Guid userGuid;
                            if (String.IsNullOrEmpty(userID))
                                userGuid = Guid.NewGuid();
                            else
                                userGuid = Guid.Parse(userID);

                            await _declarationQueries.PostHistorisationDeclarationInfoAsync(FileDeclarationInfo.NomFicher, nbrecompte, userGuid, itemagence.CodeAgencce, FolderNameZip);
                        }
                    }

                    ZipFile.CreateFromDirectory(fullPath, String.Format("{0}.zip", fullPath));
                    Directory.Delete(fullPath, true);

                    return Ok(new List<DeclarationsViewModel> { new DeclarationsViewModel { NomFichier = FolderNameZip } });
                }
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetNombreDeCompteADeclarer")]
        [ProducesResponseType(typeof(IEnumerable<DeclarationsViewModel>), (int)HttpStatusCode.OK)]
        public int GetNombreDeCompteADeclarer()
        {
            return _declarationQueries.GetNbreCompteFromSIB();
        }

        [HttpGet]
        [Route("ListDonneesADeclarer")]
        [ProducesResponseType(typeof(IEnumerable<DeclarationsViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListDonneesADeclarer(string agence, string userID, bool declarationInitiale = false)
        {
            try
            {
                int nbreCompteFinal;
                agence = String.IsNullOrEmpty(agence) ? "" : agence;

                nbreCompteFinal = _declarationQueries.GetNbreCompteFromSIB();

                if (agence != "ManyFiles")
                {
                    // nombre de comptes à déclarer calculé à partir des enregistrements préparés dans la base
                    //nbreCompteFinal = _declarationQueries.GetNbreCompteETC(agence, declarationInitiale == false ? 0 : 1);

                    //// ajout au nombre de compte déjà déclaré
                    //nbreCompteFinal += Convert.ToInt32(nbrecompte);

                    // préparation des données à déclarer
                    var result = await _preparationQueries.LancerPreparationDonnees(agence, declarationInitiale);

                    // déclaration
                    var ListDonneesADeclarer = await _declarationQueries.GetDeclarationsAsync(agence, nbreCompteFinal.ToString(), declarationInitiale);

                    return Ok(ListDonneesADeclarer);
                }
                else
                {
                    string FolderName = string.Format("{0}_{1}{2}{3}{4}{5}{6}{7}", "XCIP_Declaration", DateTime.UtcNow.Day, DateTime.UtcNow.Month, DateTime.UtcNow.Year, DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, DateTime.UtcNow.Second, DateTime.UtcNow.Millisecond);
                    var Parametres = _parametresQueries.GetParametreByCodeAsync("ZipPath");
                    string fullPath = Path.Combine(@Parametres.Libelle, FolderName);

                    // création du répertoire de sortie
                    if (!Directory.Exists(fullPath))
                        Directory.CreateDirectory(fullPath);

                    // liste des agences
                    var lstAgence = await _parametresQueries.GetAgencesAsync();
                    string FolderNameZip = String.Format("{0}.zip", FolderName);

                    //// nombre de comptes à déclarer calculé à partir des enregistrements préparés dans la base
                    //nbreCompteFinal = _declarationQueries.GetNbreCompteETC("", declarationInitiale == false ? 0 : 1);

                    //// ajout au nombre de compte déjà déclaré
                    //nbreCompteFinal += Convert.ToInt32(nbrecompte);

                    foreach (var itemagence in lstAgence)
                    {
                        // préparation des données
                        var result = await _preparationQueries.LancerPreparationDonnees(itemagence.CodeAgencce, declarationInitiale);
                        // récupération des informations sur le fichier à générer
                        var FileDeclarationInfo = await _declarationQueries.GetFileDeclarationInfoAsync();
                        // récupération des données à déclarer
                        var ListDonneesADeclarer = await _declarationQueries.GetDeclarationsAsync(itemagence.CodeAgencce, nbreCompteFinal.ToString(), declarationInitiale);

                        string fullfilePath = Path.Combine(fullPath, FileDeclarationInfo.NomFicher);

                        using (StreamWriter file = new StreamWriter(fullfilePath, true))
                        {
                            foreach (var line in ListDonneesADeclarer)
                            {
                                file.WriteLine(line.Data);
                            }

                            Guid userGuid;
                            if (String.IsNullOrEmpty(userID))
                                userGuid = Guid.NewGuid();
                            else
                                userGuid = Guid.Parse(userID);

                            //await _declarationQueries.PostHistorisationDeclarationInfoAsync(FileDeclarationInfo.NomFicher, nbrecompte, userGuid, itemagence.CodeAgencce, FolderNameZip);
                            await _declarationQueries.PostHistorisationDeclarationInfoAsync(FileDeclarationInfo.NomFicher, nbreCompteFinal.ToString(), userGuid, itemagence.CodeAgencce, FolderNameZip);
                        }
                    }

                    ZipFile.CreateFromDirectory(fullPath, String.Format("{0}.zip", fullPath));
                    Directory.Delete(fullPath, true);

                    return Ok(new List<DeclarationsViewModel> { new DeclarationsViewModel { NomFichier = FolderNameZip } });
                }
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Route("HistoriqueListDonneesDeclarees")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetHistoriqueListDonneesDeclarees([FromBody] CreateHistoDeclarationCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                var result = await _declarationQueries.PostHistorisationDeclarationInfoAsync(command.NomFichier, command.Nombre_Compte_CIP, command.CreatedBy, command.Agence);

                return result != null ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListHistoriqueListDonneesDeclarees")]
        [ProducesResponseType(typeof(IEnumerable<HistorisationDeclarationsViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListHistoriqueListDonneesDeclarees()
        {
            try
            {
                var result = await _declarationQueries.GetListHistorisationDeclarationInfoSync();
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("DetailListHistoriqueListDonneesDeclarees")]
        [ProducesResponseType(typeof(IEnumerable<Object>), (int)HttpStatusCode.OK)]
        public IActionResult GetDetailListHistoriqueListDonneesDeclarees(string Id)
        {
            try
            {
                var SyntheseHisto = _declarationQueries.GetListHistorisationDeclarationInfo(Id);
                var DetailHisto = _declarationQueries.GetDetailListHistorisationDeclarationInfo(Id);

                return Ok(new { SyntheseHisto, DetailHisto });
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("FileDeclarationInfo")]
        [ProducesResponseType(typeof(FileDeclarationInfoViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFileDeclarationInfoc()
        {
            try
            {
                var FileDeclarationInfo = await _declarationQueries.GetFileDeclarationInfoAsync();

                return Ok(FileDeclarationInfo);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("CreateDeclarationFileHisto")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostCreateDeclarationFileHisto([FromBody] SaveDeclarationFileCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                bool commandResult = false;
                commandResult = await _mediator.Send(command);

                return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("CreateFileDeclarationInfo")]
        [ProducesResponseType(typeof(FileDeclarationInfoViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCreateFileDeclarationInfoc()
        {
            try
            {
                var FileDeclarationInfo = await _declarationQueries.GetFileDeclarationInfoAsync();
                //var ListDonneesADeclarer = await _declarationQueries.GetDeclarationsAsync();

                //var path = Path.Combine("E:\\INFORMATIQUE\\PROJET\\CIP\\CIP.WEB", "Files", FileDeclarationInfo.NomFicher);

                //using (StreamWriter writer = new StreamWriter(path))
                //{
                //    foreach (var item in ListDonneesADeclarer)
                //    {
                //        await writer.WriteLineAsync(item.Data);
                //    }
                //}

                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("DownloadFileDeclaration")]
        public async Task<IActionResult> DownloadFileDeclaration(string filename)
        {
            try
            {
                //if (filename == null)
                //    return Content("filename not present");

                //var path = Path.Combine( Directory.GetCurrentDirectory(), "Files", filename);

                //var memory = new MemoryStream();
                //using (var stream = new FileStream(path, FileMode.Open))
                //{
                //    await stream.CopyToAsync(memory);
                //}
                //memory.Position = 0;
                ////return File(memory, "text/plain", Path.GetFileName(path));

                //return File(path, "text/plain", "foo.txt");

                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }

        }

        [HttpGet]
        [Route("IntegrerFichierRetour")]
        public async Task<IActionResult> IntegrerFichierRetour(string nomFichierRetour, string idFichierAller)
        {
            try
            {
                _retourQueries.IntegrerFichierRetour(nomFichierRetour, idFichierAller);
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListComptesDeclares")]
        [ProducesResponseType(typeof(IEnumerable<CompteErreurRetourViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListComptesDeclares(string idFichierAller, string agence, string etat)
        {
            try
            {
                var result = await _declarationQueries.GetComptesDeclaresAsync(idFichierAller, agence, etat);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListPersPhysiquesDeclares")]
        [ProducesResponseType(typeof(IEnumerable<PersPhysiqueErreurRetourViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListPersPhysiquesDeclares(string idFichierAller, string agence, string etat)
        {
            try
            {
                var result = await _declarationQueries.GetPersPhysiquesDeclaresAsync(idFichierAller, agence, etat);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListPersMoralesDeclares")]
        [ProducesResponseType(typeof(IEnumerable<PersMoraleErreurRetourViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListPersMoralesDeclares(string idFichierAller, string agence, string etat)
        {
            try
            {
                var result = await _declarationQueries.GetPersMoralesDeclaresAsync(idFichierAller, agence, etat);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListIncidentsChequesDeclares")]
        [ProducesResponseType(typeof(IEnumerable<IncidentsChequesErreurRetourViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListIncidentsChequesDeclares(string idFichierAller, string agence, string etat)
        {
            try
            {
                var result = await _declarationQueries.GetIncidentsChequesDeclaresAsync(idFichierAller, agence, etat);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListChequesIrreguliersDeclares")]
        [ProducesResponseType(typeof(IEnumerable<ChequesIrreguliersErreurRetourViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListChequesIrreguliersDeclares(string idFichierAller, string agence, string etat)
        {
            try
            {
                var result = await _declarationQueries.GetChequesIrreguliersDeclaresAsync(idFichierAller, agence, etat);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListIncidentsEffetsDeclares")]
        [ProducesResponseType(typeof(IEnumerable<IncidentsEffetsErreurRetourViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListIncidentsEffetsDeclares(string idFichierAller, string agence, string etat)
        {
            try
            {
                var result = await _declarationQueries.GetIncidentsEffetsDeclaresAsync(idFichierAller, agence, etat);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListErreursEnregistrementsDeclares")]
        [ProducesResponseType(typeof(IEnumerable<ErreurEnregistrementDeclareViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListErreursEnregistrementsDeclares(string enregistrementID, string enregistrementTable)
        {
            try
            {
                var result = await _declarationQueries.GetErreursEnregistrementsDeclaresAsync(enregistrementID, enregistrementTable);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ResumeTraitementAller")]
        [ProducesResponseType(typeof(TraitementAllerViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetResumeTraitementAller(string idFichierAller)
        {
            try
            {
                var result = await _declarationQueries.GetResumeTraitementAllerAsync(idFichierAller);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile(Guid Id)
        {
            try
            {
                IFormFile file = Request.Form.Files[0];

                if (file == null)
                    return (IActionResult)BadRequest();

                var completFileName = string.Format(@"{0}\{1}", _retourQueries.RootRetourFilesDirectory, file.FileName);

                if (_fileReader.ReadFile(file, completFileName))
                {
                    _retourQueries.IntegrerFichierRetour(file.FileName, Id.ToString());
                }


                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }


        [HttpPut]
        [Route("UpdateCompte")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutUpdateCompte([FromBody] UpdateCompteCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                bool commandResult = false;
                commandResult = await _mediator.Send(command);
                return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePersPhysique")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutUpdatePersPhysique([FromBody] UpdatePersPhysiqueCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                bool commandResult = false;
                commandResult = await _mediator.Send(command);
                return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePersMorale")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutUpdatePersMorale([FromBody] UpdatePersMoraleCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                bool commandResult = false;
                commandResult = await _mediator.Send(command);
                return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateIncidentCheque")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutUpdateIncidentCheque([FromBody] UpdateIncidentChequeCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                bool commandResult = false;
                commandResult = await _mediator.Send(command);
                return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateChequeIrregulier")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutUpdateChequeIrregulier([FromBody] UpdateChequeIrregulierCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                bool commandResult = false;
                commandResult = await _mediator.Send(command);
                return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateIncidentEffet")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutUpdateIncidentEffet([FromBody] UpdateIncidentEffetCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                bool commandResult = false;
                commandResult = await _mediator.Send(command);
                return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }
    }

}