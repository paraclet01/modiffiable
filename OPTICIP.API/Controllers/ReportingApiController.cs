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
            await _reportingQueries.GenererLettreAvertissement();
            return Ok();
        }

        [HttpGet]
        [Route("GenererLotLettreAvertissement")]
        public async Task<IActionResult> GenererLotLettreAvertissement()
        {
            await _reportingQueries.GenererLotLettreAvertissement();
            return Ok();
        }

        [HttpGet]
        [Route("GetChequeEnAvertissement")]
        [ProducesResponseType(typeof(IEnumerable<AvertViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetChequeEnAvertissement()
        {
            var result = await _reportingQueries.GetChequesEnAvertissement();
            return Ok(result);
        }   

        [HttpGet]
        [Route("GenererLettreInjonction")]
        public async Task<IActionResult> GenererLettreInjonction()
        {
            await _reportingQueries.GenererLettreInjonction();
            return Ok();
        }

        [HttpGet]
        [Route("GenererLotLettreInjonction")]
        public async Task<IActionResult> GenererLotLettreInjonction()
        {
            await _reportingQueries.GenererLotLettreInjonction();
            return Ok();
        }

        [HttpGet]
        [Route("GetChequeEnInjonction")]
        [ProducesResponseType(typeof(IEnumerable<InjViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetChequeEnInjonction()
        {
            var result = await _reportingQueries.GetChequesEnInjonction();
            return Ok(result);
        }

        [HttpGet]
        [Route("GenererLettreInfMandatairesInj")]
        public async Task<IActionResult> GenererLettreInfMandatairesInj()
        {
            await _reportingQueries.GenererLettreInfMandatairesInj();
            return Ok();
        }

        [HttpGet]
        [Route("GenererLettreInfMandatairesInf")]
        public async Task<IActionResult> GenererLettreInfMandatairesInf()
        {
            await _reportingQueries.GenererLettreInfMandatairesInf();
            return Ok();
        }

        //==> Générer Infractions et Infonctions
        [HttpGet]
        [Route("GenererLettreInfMandataires")]
        public async Task<IActionResult> GenererLettreInfMandataires()
        {
            await _reportingQueries.GenererLettreInfMandatairesInj();
            await _reportingQueries.GenererLettreInfMandatairesInf();
            return Ok();
        }

        [HttpGet]
        [Route("GenererLotLettreInfMandatairesInj")]
        public async Task<IActionResult> GenererLotLettreInfMandatairesInj()
        {
            await _reportingQueries.GenererLotLettreInfMandatairesInj();
            return Ok();
        }

        [HttpGet]
        [Route("GenererLotLettreInfMandatairesInf")]
        public async Task<IActionResult> GenererLotLettreInfMandatairesInf()
        {
            await _reportingQueries.GenererLotLettreInfMandatairesInf();
            return Ok();
        }


        //==> Générer Lot Infractions et Infonctions
        [HttpGet]
        [Route("GenererLotLettreInfMandataires")]
        public async Task<IActionResult> GenererLotLettreInfMandataires()
        {
            await _reportingQueries.GenererLotLettreInfMandatairesInf();
            await _reportingQueries.GenererLotLettreInfMandatairesInj();
            return Ok();
        }

        [HttpGet]
        [Route("GetLettreInfMandatairesInj")]
        [ProducesResponseType(typeof(IEnumerable<InfMandViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLettreInfMandatairesInj()
        {
            var result = await _reportingQueries.GetMandatairesDesChequesEnInjonction();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetLettreInfMandatairesInf")]
        [ProducesResponseType(typeof(IEnumerable<InfMandViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLettreInfMandatairesInf()
        {
            var result = await _reportingQueries.GetMandatairesDesChequesEnInfraction();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDonneesCertNonPaiement")]
        [ProducesResponseType(typeof(IEnumerable<CertNonPaiementViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDonneesCertNonPaiement()
        {
            var result = await _reportingQueries.GetDonneesCertNonPaiement();
            return Ok(result);
        }

        [HttpGet]
        [Route("GenererCertNonPaiement")]
        public async Task<IActionResult> GenererCertNonPaiement()
        {
            await _reportingQueries.GenererCertNonPaiements();
            return Ok();
        }

        [HttpGet]
        [Route("GenererLotCertNonPaiement")]
        public async Task<IActionResult> GenererLotCertNonPaiement()
        {
            await _reportingQueries.GenererLotCertNonPaiements();
            return Ok();
        }

        [HttpGet]
        [Route("ListLettres")]
        [ProducesResponseType(typeof(IEnumerable<LettreViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLettres(string typeLettre)
        {
            var result = await _reportingQueries.GetLettres(typeLettre);
            return Ok(result);
        }

        [HttpGet]
        [Route("ListLettresLot")]
        [ProducesResponseType(typeof(IEnumerable<LettreViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLettresLot(string typeLettre)
        {
            var result = await _reportingQueries.GetLettresLot(typeLettre);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetChequesEnInfraction")]
        [ProducesResponseType(typeof(IEnumerable<InfraViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetChequesEnInfraction()
        {
            var result = await _reportingQueries.GetChequesEnInfraction();
            return Ok(result);
        }

        [HttpGet]
        [Route("GenererLettresEnInfraction")]
        public async Task<IActionResult> GenererLettresEnInfraction()
        {
            await _reportingQueries.GenererLettresEnInfraction();
            return Ok();
        }

        [HttpGet]
        [Route("GenererLotLettresEnInfraction")]
        public async Task<IActionResult> GenererLotLettresEnInfraction()
        {
            await _reportingQueries.GenererLotLettresEnInfraction();
            return Ok();
        }

        [HttpGet]
        [Route("GetAttNonPaiementEffet")]
        [ProducesResponseType(typeof(IEnumerable<AttNonPaiementEffetViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAttNonPaiementEffet()
        {
            var result = await _reportingQueries.GetAttNonPaiementEffet();
            return Ok(result);
        }

        [HttpGet]
        [Route("GenererAttNonPaiementEffet")]
        public async Task<IActionResult> GenererAttNonPaiementEffet()
        {
            await _reportingQueries.GenererAttNonPaiementEffet();
            return Ok();
        }

        [HttpGet]
        [Route("GenererLotAttNonPaiementEffet")]
        public async Task<IActionResult> GenererLotAttNonPaiementEffet()
        {
            await _reportingQueries.GenererLotAttNonPaiementEffet();
            return Ok();
        }

        [HttpGet]
        [Route("GetAttPaiementCheques")]
        [ProducesResponseType(typeof(IEnumerable<AttPaiementChequesViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAttPaiementCheques()
        {
            var result = await _reportingQueries.GetAttPaiementCheques();
            return Ok(result);
        }

        [HttpGet]
        [Route("GenererAttPaiementCheques")]
        public async Task<IActionResult> GenererAttPaiementCheques()
        {
            await _reportingQueries.GenererAttPaiementCheques();
            return Ok();
        }

        [HttpGet]
        [Route("GenererLotAttPaiementCheques")]
        public async Task<IActionResult> GenererLotAttPaiementCheques()
        {
            await _reportingQueries.GenererLotAttPaiementCheques();
            return Ok();
        }
    }
}
