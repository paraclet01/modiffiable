using Microsoft.AspNetCore.Mvc;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OPTICIP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StatistiquesApiController : ControllerBase
    {
        private readonly IStatistiquesQueries _statistiquesQueries;

        public StatistiquesApiController(IStatistiquesQueries statistiquesQueries)
        {
            _statistiquesQueries = statistiquesQueries ?? throw new ArgumentNullException(nameof(statistiquesQueries));
        }

        [HttpGet]
        [Route("GetComptesBancairesDeclares")]
        [ProducesResponseType(typeof(IEnumerable<CompteBancaireViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetComptesBancairesDeclares(String dateDebut, String dateFin)
        {
            try
            {
                var result = await _statistiquesQueries.GetComptesBancairesDeclares(dateDebut, dateFin);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetComptesMandataires")]
        [ProducesResponseType(typeof(IEnumerable<CompteMandataireViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetComptesMandataires(String dateDebut, String dateFin)
        {
            try
            {
                var result = await _statistiquesQueries.GetComptesMandataires(dateDebut, dateFin);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetIncidentsDePaiementCheques")]
        [ProducesResponseType(typeof(IEnumerable<IncidentDePaiementChequeViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetIncidentsDePaiementCheques(String dateDebut, String dateFin)
        {
            try
            {
                var result = await _statistiquesQueries.GetIncidentsDePaiementCheques(dateDebut, dateFin);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetIncidentsDePaiementEffets")]
        [ProducesResponseType(typeof(IEnumerable<IncidentDePaiementEffetViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetIncidentsDePaiementEffets(String dateDebut, String dateFin)
        {
            try
            {
                var result = await _statistiquesQueries.GetIncidentsDePaiementEffets(dateDebut, dateFin);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        /*
        [HttpGet]
        [Route("GetDeclarationsSusmentionnees")]
        [ProducesResponseType(typeof(IEnumerable<DeclarationSusmentionneeViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDeclarationsSusmentionnees(String dateDebut, String dateFin)
        {
            var result = await _statistiquesQueries.GetDeclarationsSusmentionnees(dateDebut, dateFin);
            return Ok(result);
        }
        */

        [HttpGet]
        [Route("GetIncidentsDePaiementRegularises")]
        [ProducesResponseType(typeof(IEnumerable<IncidentDePaiementRegulariseViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetIncidentsDePaiementRegularises(String dateDebut, String dateFin)
        {
            try
            {
                var result = await _statistiquesQueries.GetIncidentsDePaiementRegularises(dateDebut, dateFin);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetComptesEnInterditBancaire")]
        [ProducesResponseType(typeof(IEnumerable<CompteEnInterditBancaireViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetComptesEnInterditBancaire(String dateDebut, String dateFin)
        {
            try
            {
                var result = await _statistiquesQueries.GetComptesEnInterditBancaire(dateDebut, dateFin);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetComptesEnInterditionLevee")]
        [ProducesResponseType(typeof(IEnumerable<CompteEnInterditionLeveeViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetComptesEnInterditionLevee(String dateDebut, String dateFin)
        {
            try
            {
                var result = await _statistiquesQueries.GetComptesEnInterditionLevee(dateDebut, dateFin);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetPersPhysiquesDeclarees")]
        [ProducesResponseType(typeof(IEnumerable<PersPhysiqueViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersPhysiquesDeclarees(String dateDebut, String dateFin)
        {
            try
            {
                var result = await _statistiquesQueries.GetPersPhysiquesDeclarees(dateDebut, dateFin);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetPersMoralesDeclarees")]
        [ProducesResponseType(typeof(IEnumerable<PersMoraleDeclareeViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersMoralesDeclarees(String dateDebut, String dateFin)
        {
            try
            {
                var result = await _statistiquesQueries.GetPersMoralesDeclarees(dateDebut, dateFin);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }
    }
}
