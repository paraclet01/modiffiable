using Microsoft.AspNetCore.Mvc;
using OPTICIP.API.Application.Queries.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPTICIP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetourApiController : ControllerBase
    {
        private readonly IRetourQueries _retourQueries;
        public RetourApiController(IRetourQueries retourQueries)
        {
            _retourQueries = retourQueries ?? throw new ArgumentNullException(nameof(retourQueries));
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
    }
}
