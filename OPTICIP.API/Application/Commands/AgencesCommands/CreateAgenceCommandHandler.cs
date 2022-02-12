using MediatR;
using OPTICIP.Entities.DataEntities;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.AgencesCommands
{
    public class CreateAgenceCommandHandler : IRequestHandler<CreateAgenceCommand, bool>
    {
        private readonly IGeneriqueRepository<TAgences> _agencesRepository;
        private readonly IMediator _mediator;
        private readonly IRepositoryFactory _repositoryFactory;

        public CreateAgenceCommandHandler(IMediator mediator, IGeneriqueRepository<TAgences> agencesRepository, IRepositoryFactory repositoryFactory)
        {
            _agencesRepository = agencesRepository ?? throw new ArgumentNullException(nameof(agencesRepository));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(CreateAgenceCommand message, CancellationToken cancellationToken)
        {
            try
            {
                var agence = new TAgences(Guid.NewGuid(), message.Code, message.Libelle, message.Description);
                _agencesRepository.Add(agence);

                // Logging
                Guid guid = Guid.NewGuid();
                var messageLog = "Code : " + message.Code + ", Libelle : " + message.Libelle + ", Description : " + message.Description;
                TLog log = new TLog(guid, "Création d'une agence", messageLog, guid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                await _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();

                return await _agencesRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex){
                throw ex;
            }
        }
    }
}
