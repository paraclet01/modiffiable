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
    public class SyntheseApiController : ControllerBase
    {
        private readonly ISyntheseQueries _syntheseQueries;

        public SyntheseApiController(ISyntheseQueries syntheseQueries)
        {
            _syntheseQueries = syntheseQueries ?? throw new ArgumentNullException(nameof(syntheseQueries));
        }

        [HttpGet]
        [Route("GetSyntheseDeclaration")]
        [ProducesResponseType(typeof(IEnumerable<SyntheseViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSyntheseDeclaration(String dateDebut, String dateFin)
        {
            try
            { 
            var result = await _syntheseQueries.GetSyntheseDeclarationAsync(dateDebut, dateFin);
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
