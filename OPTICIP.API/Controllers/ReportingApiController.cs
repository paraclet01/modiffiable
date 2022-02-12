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
    public class ReportingApiController : ControllerBase
    {
        private readonly IReportingQueries _reportingQueries;

        public ReportingApiController(IReportingQueries reportingQueries)
        {
            _reportingQueries = reportingQueries ?? throw new ArgumentNullException(nameof(reportingQueries));
        }

        [HttpGet]
        [Route("GenererLettreAvertissement")]
        public async Task<IActionResult> GenererLettreAvertissement()
        {
            try
            {
                await _reportingQueries.GenererLettreAvertissement();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererLotLettreAvertissement")]
        public async Task<IActionResult> GenererLotLettreAvertissement()
        {
            try
            {
                await _reportingQueries.GenererLotLettreAvertissement();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetChequeEnAvertissement")]
        [ProducesResponseType(typeof(IEnumerable<AvertViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetChequeEnAvertissement()
        {
            try
            {
                var result = await _reportingQueries.GetChequesEnAvertissement();
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererLettreInjonction")]
        public async Task<IActionResult> GenererLettreInjonction()
        {
            try
            {
                await _reportingQueries.GenererLettreInjonction();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererLotLettreInjonction")]
        public async Task<IActionResult> GenererLotLettreInjonction()
        {
            try
            {
                await _reportingQueries.GenererLotLettreInjonction();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetChequeEnInjonction")]
        [ProducesResponseType(typeof(IEnumerable<InjViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetChequeEnInjonction()
        {
            try
            {
                var result = await _reportingQueries.GetChequesEnInjonction();
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererLettreInfMandatairesInj")]
        public async Task<IActionResult> GenererLettreInfMandatairesInj()
        {
            try
            {
                await _reportingQueries.GenererLettreInfMandatairesInj();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererLettreInfMandatairesInf")]
        public async Task<IActionResult> GenererLettreInfMandatairesInf()
        {
            try
            {
                await _reportingQueries.GenererLettreInfMandatairesInf();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        //==> Générer Infractions et Infonctions
        [HttpGet]
        [Route("GenererLettreInfMandataires")]
        public async Task<IActionResult> GenererLettreInfMandataires()
        {
            try
            {
                await _reportingQueries.GenererLettreInfMandatairesInj();
                await _reportingQueries.GenererLettreInfMandatairesInf();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererLotLettreInfMandatairesInj")]
        public async Task<IActionResult> GenererLotLettreInfMandatairesInj()
        {
            try
            {
                await _reportingQueries.GenererLotLettreInfMandatairesInj();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererLotLettreInfMandatairesInf")]
        public async Task<IActionResult> GenererLotLettreInfMandatairesInf()
        {
            try
            {
                await _reportingQueries.GenererLotLettreInfMandatairesInf();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }


        //==> Générer Lot Infractions et Infonctions
        [HttpGet]
        [Route("GenererLotLettreInfMandataires")]
        public async Task<IActionResult> GenererLotLettreInfMandataires()
        {
            try
            {
                await _reportingQueries.GenererLotLettreInfMandatairesInf();
                await _reportingQueries.GenererLotLettreInfMandatairesInj();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetLettreInfMandatairesInj")]
        [ProducesResponseType(typeof(IEnumerable<InfMandViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLettreInfMandatairesInj()
        {
            try
            {
                var result = await _reportingQueries.GetMandatairesDesChequesEnInjonction();
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetLettreInfMandatairesInf")]
        [ProducesResponseType(typeof(IEnumerable<InfMandViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLettreInfMandatairesInf()
        {
            try
            {
                var result = await _reportingQueries.GetMandatairesDesChequesEnInfraction();
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetDonneesCertNonPaiement")]
        [ProducesResponseType(typeof(IEnumerable<CertNonPaiementViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDonneesCertNonPaiement()
        {
            try
            {
                var result = await _reportingQueries.GetDonneesCertNonPaiement();
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererCertNonPaiement")]
        public async Task<IActionResult> GenererCertNonPaiement()
        {
            try
            {
                await _reportingQueries.GenererCertNonPaiements();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererLotCertNonPaiement")]
        public async Task<IActionResult> GenererLotCertNonPaiement()
        {
            try
            {
                await _reportingQueries.GenererLotCertNonPaiements();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListLettres")]
        [ProducesResponseType(typeof(IEnumerable<LettreViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLettres(string typeLettre)
        {
            try
            {
                var result = await _reportingQueries.GetLettres(typeLettre);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListLettresLot")]
        [ProducesResponseType(typeof(IEnumerable<LettreViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLettresLot(string typeLettre)
        {
            try
            {
                var result = await _reportingQueries.GetLettresLot(typeLettre);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetChequesEnInfraction")]
        [ProducesResponseType(typeof(IEnumerable<InfraViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetChequesEnInfraction()
        {
            try
            {
                var result = await _reportingQueries.GetChequesEnInfraction();
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererLettresEnInfraction")]
        public async Task<IActionResult> GenererLettresEnInfraction()
        {
            try
            {
                await _reportingQueries.GenererLettresEnInfraction();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererLotLettresEnInfraction")]
        public async Task<IActionResult> GenererLotLettresEnInfraction()
        {
            try
            {
                await _reportingQueries.GenererLotLettresEnInfraction();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetAttNonPaiementEffet")]
        [ProducesResponseType(typeof(IEnumerable<AttNonPaiementEffetViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAttNonPaiementEffet()
        {
            try
            {
                var result = await _reportingQueries.GetAttNonPaiementEffet();
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererAttNonPaiementEffet")]
        public async Task<IActionResult> GenererAttNonPaiementEffet()
        {
            try
            {
                await _reportingQueries.GenererAttNonPaiementEffet();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererLotAttNonPaiementEffet")]
        public async Task<IActionResult> GenererLotAttNonPaiementEffet()
        {
            try
            {
                await _reportingQueries.GenererLotAttNonPaiementEffet();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GetAttPaiementCheques")]
        [ProducesResponseType(typeof(IEnumerable<AttPaiementChequesViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAttPaiementCheques()
        {
            try
            {
                var result = await _reportingQueries.GetAttPaiementCheques();
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererAttPaiementCheques")]
        public async Task<IActionResult> GenererAttPaiementCheques()
        {
            try
            {
                await _reportingQueries.GenererAttPaiementCheques();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("GenererLotAttPaiementCheques")]
        public async Task<IActionResult> GenererLotAttPaiementCheques()
        {
            try
            {
                await _reportingQueries.GenererLotAttPaiementCheques();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }
    }
}
