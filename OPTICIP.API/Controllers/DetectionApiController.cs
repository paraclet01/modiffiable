﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using OPTICIP.DataAccessLayer.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using OPTICIP.API.Application.Commands.DetectionCommands;

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
            try
            {
                await _detectionQueries.LancerDetectionComptes("");
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("DetectionComptes_New")]
        [ProducesResponseType(typeof(IEnumerable<CIP1ViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DetectionPersonnesComptes_New()
        {
            try
            {
                var result = await _detectionQueries.LancerDetectionComptes_New();
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }
        [HttpGet]
        [Route("DetectionPersonnesPhysiques")]
        public async Task<IActionResult> DetectionPersonnesPhysiques()
        {
            try
            {
                await _detectionQueries.LancerDetectionPersonnesPhysiques("");
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("DetectionPersonnesMorales")]
        public async Task<IActionResult> DetectionPersonnesMorales()
        {
            try
            {
                await _detectionQueries.LancerDetectionPersonnesMorales("");
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("DetectionCartes")]
        public async Task<IActionResult> DetectionCartes()
        {
            try
            {
                await _detectionQueries.LancerDetectionCartes("");
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("DetectionChequesTFJ")]
        public async Task<IActionResult> DetectionChequesTFJ()
        {
            try
            {
                await _detectionQueries.LancerDetectionChequesTFJ();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListChequesDetectesTFJ")]
        [ProducesResponseType(typeof(IEnumerable<ChqRejViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListChequesDetectesTFJ(int Option)
        {
            try
            {
                //var result = await _detectionQueries.ListChequesDetectesTFJ_Old();
                var result = await _detectionQueries.ListChequesDetectesTFJ(Option);
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ListEffetsDetectesTFJ")]
        [ProducesResponseType(typeof(IEnumerable<EffRejViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ListEffetsDetectesTFJ()
        {
            try
            {
                var result = await _detectionQueries.ListEffetsDetectesTFJ();
                return Ok(result);
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("DetectionCheques")]
        public async Task<IActionResult> DetectionCheques()
        {
            try
            {
                await _detectionQueries.LancerDetectionCheques("");
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("DetectionChequesIrreguliers")]
        public async Task<IActionResult> DetectionChequesIrreguliers()
        {
            try
            {
                await _detectionQueries.LancerDetectionChequesIrreguliers("");
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("DetectionEffetsTFJ")]
        public async Task<IActionResult> DetectionEffetsTFJ()
        {
            try
            {
                await _detectionQueries.LancerDetectionEffetsTFJ();
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("DetectionEffets")]
        public async Task<IActionResult> DetectionEffets()
        {
            try
            {
                await _detectionQueries.LancerDetectionEffets("");
                return Ok();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("UpdatIncidentChqBenef")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdatIncidentChqBenef([FromBody] UpdateIncidentChqBenefCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            try
            {
                var result = await _detectionQueries.UpdatIncidentChqBenef(command.idCheque, command.benefNom, command.benefPrenom);
                return result >= 1 ? (IActionResult)Ok() : (IActionResult)BadRequest();
            }
            catch (Exception e)
            {
                Logger.ApplicationLogger.LogError(e);
                return (IActionResult)BadRequest(e.Message);
            }
        }


    }
}