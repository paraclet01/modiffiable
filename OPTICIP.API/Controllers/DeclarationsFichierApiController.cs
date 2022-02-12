using MediatR;
using Microsoft.AspNetCore.Mvc;
using OPTICIP.API.Application.Commands.UsersCommands;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace OPTICIP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DeclarationsFichierApiController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDeclarationsQuerie _declarationQueries;
        public DeclarationsFichierApiController(IMediator mediator, IDeclarationsQuerie declarationQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _declarationQueries = declarationQueries ?? throw new ArgumentNullException(nameof(declarationQueries));

        }



        // GET: api/ListChequeIrregulier
        [HttpGet]
        [Route("ListChequeIrregulier")]
        [ProducesResponseType(typeof(IEnumerable<ChequeIrregulierViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListChequeIrregulier()
        {
            var ChequeIrregulier = await _declarationQueries.GetChequesIrregulierAsync();
            return Ok(ChequeIrregulier);
        }

        // GET: api/ListCompte
        [HttpGet]
        [Route("ListCompte")]
        [ProducesResponseType(typeof(IEnumerable<CompteViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListCompte()
        {
            var Comptes = await _declarationQueries.GetComptesAsync();
            return Ok(Comptes);
        }


        // GET: api/ListIncidentCheque
        [HttpGet]
        [Route("ListIncidentCheque")]
        [ProducesResponseType(typeof(IEnumerable<IncidentChequeViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListIncidentCheque()
        {
            var IncidentCheques = await _declarationQueries.GetIncidentsChequesAsync();
            return Ok(IncidentCheques);
        }


        // GET: api/ListIncidentEffet
        [HttpGet]
        [Route("ListIncidentEffet")]
        [ProducesResponseType(typeof(IEnumerable<IncidentEffetViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListIncidentEffet()
        {
            var IncidentEffets = await _declarationQueries.GetIncidentsEffetsAsync();
            return Ok(IncidentEffets);
        }


        // GET: api/ListPersonnesMorales
        [HttpGet]
        [Route("ListPersonnesMorales")]
        [ProducesResponseType(typeof(IEnumerable<Pers_MoraleViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListPersonnesMorales()
        {
            var ListPersonnesMorales = await _declarationQueries.GetPersonnesMoralesAsync();
            return Ok(ListPersonnesMorales);
        }

        // GET: api/ListPersonnesPhysiques
        [HttpGet]
        [Route("ListPersonnesPhysiques")]
        [ProducesResponseType(typeof(IEnumerable<Pers_PhysiqueViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListPersonnesPhysiques()
        {
            var ListPersonnesPhysiques = await _declarationQueries.GetPersonnesPhysiquesAsync();
            return Ok(ListPersonnesPhysiques);
        }

        // GET: api/ListCartes
        [HttpGet]
        [Route("ListCartes")]
        [ProducesResponseType(typeof(IEnumerable<CartesViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListCartes()
        {
            var ListCartes = await _declarationQueries.GetCartesAsync();
            return Ok(ListCartes);
        }

        // GET: api/ListDonneesADeclarer
        [HttpGet]
        [Route("ListDonneesADeclarer")]
        [ProducesResponseType(typeof(IEnumerable<DeclarationsViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListDonneesADeclarer()
        {
            var ListDonneesADeclarer = await _declarationQueries.GetDeclarationsAsync();

            return Ok(ListDonneesADeclarer);
        }

        // GET: api/ListDonneesADeclarer
        [HttpGet]
        [Route("FileDeclarationInfo")]
        [ProducesResponseType(typeof(FileDeclarationInfoViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFileDeclarationInfoc()
        {
            var FileDeclarationInfo = await _declarationQueries.GetFileDeclarationInfoAsync();
            
            return Ok(FileDeclarationInfo);
        }


        [HttpPost]
        [Route("CreateDeclarationFileHisto")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostCreateDeclarationFileHisto([FromBody]SaveDeclarationFileCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            //if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            //{
            //    commandResult = await _mediator.Send(command);
            //}
            commandResult = await _mediator.Send(command);

            return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
        }


        // GET: api/CreateFileDeclarationInfo
        [HttpGet]
        [Route("CreateFileDeclarationInfo")]
        [ProducesResponseType(typeof(FileDeclarationInfoViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCreateFileDeclarationInfoc()
        {
            var FileDeclarationInfo = await _declarationQueries.GetFileDeclarationInfoAsync();
            var ListDonneesADeclarer = await _declarationQueries.GetDeclarationsAsync();

            var path = Path.Combine("E:\\INFORMATIQUE\\PROJET\\CIP\\CIP.WEB", "Files", FileDeclarationInfo.NomFicher);

            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var item in ListDonneesADeclarer)
                {
                    await writer.WriteLineAsync(item.Data);
                }
            }

            return Ok();
        }

        [HttpGet]
        [Route("DownloadFileDeclaration")]
        public async Task<IActionResult> DownloadFileDeclaration(string filename)
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
    }
}