using MediatR;
using OPTICIP.Entities.DataEntities;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    public class CreateDonneeRetireCommandHandler : IRequestHandler<CreateDonneeRetireCommand, bool>
    {
        private readonly IDonneeRetireRepository _donneeRetireRepository;
        private readonly IMediator _mediator;
        private readonly IRepositoryFactory _repositoryFactory;

        public CreateDonneeRetireCommandHandler(IMediator mediator, IDonneeRetireRepository donneeRetireRepository, IRepositoryFactory repositoryFactory)
        {
            _donneeRetireRepository = donneeRetireRepository ?? throw new ArgumentNullException(nameof(donneeRetireRepository));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(CreateDonneeRetireCommand message, CancellationToken cancellationToken)
        {

            try
            {
                var DonneeRetire = new TDonneeRetire(message.IdItem, message.Table_Item, message.CreatedBy);
                _donneeRetireRepository.Add(DonneeRetire);

                // Logging
                Guid guid = Guid.NewGuid();
                var messageLog = "Table_Item : " + message.Table_Item + ", IdItem : " + message.IdItem;
                TLog log = new TLog(guid, "Retrait d'une donnée", messageLog, guid, DateTime.Now);
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
