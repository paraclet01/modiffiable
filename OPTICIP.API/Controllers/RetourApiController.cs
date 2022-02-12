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
            _retourQueries.IntegrerFichierRetour(nomFichierRetour, idFichierAller);
            return Ok();
        }
    }
}
