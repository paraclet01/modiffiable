using Microsoft.AspNetCore.Http;
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
    public class DetectionApiController : ControllerBase
    {
        private readonly IDetectionQueries _detectionQueries;

        public DetectionApiController(IDetectionQueries detectionQueries)
        {
            _detectionQueries = detectionQueries ?? throw new ArgumentNullException(nameof(detectionQueries));
        }

        [HttpGet]
        [Route("DetectionComptes")]
        public async Task<IActionResult> DetectionPersonnesComptes()
        {
            await _detectionQueries.LancerDetectionComptes("");
            return Ok();
        }

        [HttpGet]
        [Route("DetectionPersonnesPhysiques")]
        public async Task<IActionResult> DetectionPersonnesPhysiques()
        {
            await _detectionQueries.LancerDetectionPersonnesPhysiques("");
            return Ok();
        }

        [HttpGet]
        [Route("DetectionPersonnesMorales")]
        public async Task<IActionResult> DetectionPersonnesMorales()
        {
            await _detectionQueries.LancerDetectionPersonnesMorales("");
            return Ok();
        }

        [HttpGet]
        [Route("DetectionCartes")]
        public async Task<IActionResult> DetectionCartes()
        {
            await _detectionQueries.LancerDetectionCartes("");
            return Ok();
        }

        [HttpGet]
        [Route("DetectionChequesTFJ")]
        public async Task<IActionResult> DetectionChequesTFJ()
        {
            await _detectionQueries.LancerDetectionChequesTFJ();
            return Ok();
        }

        [HttpGet]
        [Route("ListChequesDetectesTFJ")]
        [ProducesResponseType(typeof(IEnumerable<ChqRejViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListChequesDetectesTFJ()
        {
            var result = await _detectionQueries.ListChequesDetectesTFJ();
            return Ok(result);
        }

        [HttpGet]
        [Route("ListEffetsDetectesTFJ")]
        [ProducesResponseType(typeof(IEnumerable<ChqRejViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListEffetsDetectesTFJ()
        {
            var result = await _detectionQueries.ListEffetsDetectesTFJ();
            return Ok(result);
        }

        [HttpGet]
        [Route("DetectionCheques")]
        public async Task<IActionResult> DetectionCheques()
        {
            await _detectionQueries.LancerDetectionCheques("");
            return Ok();
        }

        [HttpGet]
        [Route("DetectionChequesIrreguliers")]
        public async Task<IActionResult> DetectionChequesIrreguliers()
        {
            await _detectionQueries.LancerDetectionChequesIrreguliers("");
            return Ok();
        }

        [HttpGet]
        [Route("DetectionEffetsTFJ")]
        public async Task<IActionResult> DetectionEffetsTFJ()
        {
            await _detectionQueries.LancerDetectionEffetsTFJ();
            return Ok();
        }

        [HttpGet]
        [Route("DetectionEffets")]
        public async Task<IActionResult> DetectionEffets()
        {
            await _detectionQueries.LancerDetectionEffets("");
            return Ok();
        }

    }
}