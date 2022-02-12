using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPTICIP.API.Application.Commands.UsersCommands;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.API.Application.Queries.ViewModels;
using OPTICIP.BusinessLogicLayer.Utilitaire;

namespace OPTICIP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class UsersApiController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IUsersQuerie _userQueries;
        public UsersApiController(IMediator mediator, IUsersQuerie userQueries /*, IIdentityService identityService*/)
        {

            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userQueries = userQueries ?? throw new ArgumentNullException(nameof(userQueries));
        }

        [HttpPost]
        [Route("CreateUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostCreateUser([FromBody]CreateUserCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            //if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            //{
            //    commandResult = await _mediator.Send(command);
            //}

            commandResult = await _mediator.Send(command);
            return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
        }


        [HttpPut]
        [Route("UpdateUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutUpdateUser([FromBody]UpdateUserCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            //if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            //{
            //    commandResult = await _mediator.Send(command);
            //}
            commandResult = await _mediator.Send(command);
            return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
           
        }

        [HttpPut]
        [Route("UpdateUserPassWord")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutUpdateUserPassWord([FromBody]UpdateUserPassWordCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            //if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            //{
            //    commandResult = await _mediator.Send(command);
            //}
            commandResult = await _mediator.Send(command);
            return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
        }

        [HttpPut]
        [Route("UpdateStatutUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PutUpdateStatutUser([FromBody]ChangeStatutUserCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            commandResult = await _mediator.Send(command);
            return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
        }

        [HttpGet]
        [Route("ListUsers")]
        [ProducesResponseType(typeof(IEnumerable<UsersViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListUsers(string Statut)
        {
            var Users = await _userQueries.GetUsersAsync(int.Parse(Statut));
            return Ok(Users);
        }

        [HttpGet("User", Name = "Get")]
        [ProducesResponseType(typeof(UsersViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUser(string Id)
        {
            var user = await _userQueries.GetUserAsync(Guid.Parse(Id));

            return Ok(user);
        }

        [HttpGet("LoginUser")]
        [ProducesResponseType(typeof(UsersViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLoginUser(string Login, string MotPasse)
        {
            try
            {
                var user = _userQueries.GetUser(Login, MotPasse);
                return user != null ? Ok(user) : (IActionResult)BadRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }        
        }

    }
}