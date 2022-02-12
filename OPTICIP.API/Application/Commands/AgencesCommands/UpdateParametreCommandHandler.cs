using MediatR;
using OPTICIP.Entities.DataEntities;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.AgencesCommands
{
    public class UpdateParametreCommandHandler : IRequestHandler<UpdateParametreCommand, bool>
    {
        private readonly IGeneriqueRepository<TParametres> _parametreRepository;
        private readonly IMediator _mediator;
        private readonly IRepositoryFactory _repositoryFactory;

        public UpdateParametreCommandHandler(IMediator mediator, IGeneriqueRepository<TParametres> parametreRepository, IRepositoryFactory repositoryFactory)
        {
            _parametreRepository = parametreRepository ?? throw new ArgumentNullException(nameof(parametreRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        public async Task<bool> Handle(UpdateParametreCommand message, CancellationToken cancellationToken)
        {

            try
            {
                var Parametre = await _parametreRepository.GetAsync(message.Id);
                bool ParametreExisted = (Parametre == null) ? false : true;

                if (ParametreExisted) {
                    Parametre.Update(message.Code,message.Libelle, message.Description);  
                }

                _parametreRepository.Update(Parametre);

                // Logging
                Guid guid = Guid.NewGuid();
                var messageLog = "Code : " + message.Code + ", Libelle : " + message.Libelle + ", Description : " + message.Description;
                TLog log = new TLog(guid, "Mise à jour d'un paramètre", messageLog, guid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                await _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();

                return await _parametreRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex){
                throw ex;
            }
        }
    }
}
