  using MediatR;
using OPTICIP.Entities.DataEntities;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.UsersCommands
{
    public class ChangeStatutUserCommandHandler : IRequestHandler<ChangeStatutUserCommand, bool>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMediator _mediator;
        private readonly IRepositoryFactory _repositoryFactory;

        public ChangeStatutUserCommandHandler(IMediator mediator, IUsersRepository usersRepository, IRepositoryFactory repositoryFactory  /*, IIdentityService identityService*/)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        public async Task<bool> Handle(ChangeStatutUserCommand message, CancellationToken cancellationToken)
        {

            try
            {
                var User = await _usersRepository.GetAsync(message.Id);
                bool UserExisted = (User == null) ? false : true;

                if (UserExisted) {
                    User.ChangStatutUser(message.Statut);  
                }

                _usersRepository.Update(User);

                Guid guid = Guid.NewGuid();
                var messageLog = "Statut : " + message.Statut + ", ID : " + message.Id;
                TLog log = new TLog(guid, "Changement de statut d'un compte utilisateur", messageLog, guid, DateTime.Now);
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
