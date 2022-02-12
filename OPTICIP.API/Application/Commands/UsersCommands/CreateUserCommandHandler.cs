using MediatR;
using OPTICIP.BusinessLogicLayer.Utilitaire;
using OPTICIP.Entities.DataEntities;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.UsersCommands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMediator _mediator;
        private readonly IRepositoryFactory _repositoryFactory;

        public CreateUserCommandHandler(IMediator mediator, IUsersRepository usersRepository, IRepositoryFactory repositoryFactory)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(CreateUserCommand message, CancellationToken cancellationToken)
        {

            try
            {
                var user = new TUsers(Guid.NewGuid(), message.Nom, message.Prenoms, message.Login, message.MotPasse.Crypter(), message.Profil);
                _usersRepository.Add(user);

                // Logging
                Guid guid = Guid.NewGuid();
                var messageLog = "Nom : " + message.Nom +  ", Prenoms : " +  message.Prenoms + ", Login : " + message.Login; 
                TLog log = new TLog(guid, "Création d'un compte utilisateur", messageLog, guid, DateTime.Now);
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
