using MediatR;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OPTICIP.BusinessLogicLayer.Utilitaire;
using OPTICIP.Entities.DataEntities;

namespace OPTICIP.API.Application.Commands.UsersCommands
{
    public class UpdateUserPassWordCommandHandler : IRequestHandler<UpdateUserPassWordCommand, bool>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMediator _mediator;
        private readonly IRepositoryFactory _repositoryFactory;

        public UpdateUserPassWordCommandHandler(IMediator mediator, IUsersRepository usersRepository, IRepositoryFactory repositoryFactory /*, IIdentityService identityService*/)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        public async Task<bool> Handle(UpdateUserPassWordCommand message, CancellationToken cancellationToken)
        {

            try
            {
                var User = await _usersRepository.GetAsync(message.Id);
                bool UserExisted = (User == null) ? false : true;

                if (UserExisted) {
                    User.UpdateMotPasse(message.MotPasse.Crypter());  
                }

                _usersRepository.Update(User);

                Guid guid = Guid.NewGuid();
                var messageLog = "Id : " + message.Id;
                TLog log = new TLog(guid, "Changement de mot de passe d'un compte utilisateur", messageLog, guid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                await _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();

                return await _usersRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex){
                throw ex;
            }
        }
    }
}
