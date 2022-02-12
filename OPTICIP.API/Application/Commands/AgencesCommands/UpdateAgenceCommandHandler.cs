using MediatR;
using OPTICIP.Entities.DataEntities;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.AgencesCommands
{
    public class UpdateAgenceCommandHandler : IRequestHandler<UpdateAgenceCommand, bool>
    {
        private readonly IGeneriqueRepository<TAgences> _agencesRepository;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMediator _mediator;

        public UpdateAgenceCommandHandler(IMediator mediator, IGeneriqueRepository<TAgences> agencesRepository, IRepositoryFactory repositoryFactory)
        {
            _agencesRepository = agencesRepository ?? throw new ArgumentNullException(nameof(agencesRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        public async Task<bool> Handle(UpdateAgenceCommand message, CancellationToken cancellationToken)
        {

            try
            {
                var Agence = await _agencesRepository.GetAsync(message.Id);
                bool AgenceExisted = (Agence == null) ? false : true;

                if (AgenceExisted) {
                    Agence.UpdateAgence(message.Libelle, message.Description);  
                }

                _agencesRepository.Update(Agence);

                // Logging
                Guid guid = Guid.NewGuid();
                var messageLog = "Code : " + message.Code + ", Libelle : " + message.Libelle + ", Description : " + message.Description;
                TLog log = new TLog(guid, "Mise à jour d'une agence", messageLog, guid, DateTime.Now);
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
