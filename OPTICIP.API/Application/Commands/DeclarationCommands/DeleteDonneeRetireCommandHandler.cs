using MediatR;
using OPTICIP.Entities.DataEntities;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    public class DeleteDonneeRetireCommandHandler : IRequestHandler<DeleteDonneeRetireCommand, bool>
    {
        private readonly IDonneeRetireRepository _donneeRetireRepository;
        private readonly IMediator _mediator;
        private readonly IRepositoryFactory _repositoryFactory;

        public DeleteDonneeRetireCommandHandler(IMediator mediator, IDonneeRetireRepository donneeRetireRepository, IRepositoryFactory repositoryFactory)
        {
            _donneeRetireRepository = donneeRetireRepository ?? throw new ArgumentNullException(nameof(donneeRetireRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        public async Task<bool> Handle(DeleteDonneeRetireCommand message, CancellationToken cancellationToken)
        {

            try
            {
                var DonneeRetire = await _donneeRetireRepository.GetAsync(message.IdItem);
                bool DonneeRetireOriginallyExisted = (DonneeRetire == null) ? false : true;


                if (DonneeRetireOriginallyExisted)
                {
                    _donneeRetireRepository.Delete(DonneeRetire);
                }

                // Logging
                Guid guid = Guid.NewGuid();
                var messageLog = "IdItem : " + message.IdItem;
                TLog log = new TLog(guid, "Ajout d'une donnée", messageLog, guid, DateTime.Now);
                _repositoryFactory.LogRepository.Add(log);
                await _repositoryFactory.LogRepository.UnitOfWork.SaveEntitiesAsync();

                return await _donneeRetireRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex){
                throw ex;
            }
        }
    }
}
