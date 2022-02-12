using MediatR;
using OPTICIP.Entities.DataEntities;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    public class UpdateChequeIrregulierCommandHandler : IRequestHandler<UpdateChequeIrregulierCommand, bool>
    {
        private readonly IChequeIrregulierRepository _chequeIrregulierRepository;
        private readonly IMediator _mediator;
        private readonly IRepositoryFactory _repositoryFactory;

        public UpdateChequeIrregulierCommandHandler(IMediator mediator, IChequeIrregulierRepository chequeIrregulierRepository, IRepositoryFactory repositoryFactory)
        {
            _chequeIrregulierRepository = chequeIrregulierRepository ?? throw new ArgumentNullException(nameof(chequeIrregulierRepository));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(UpdateChequeIrregulierCommand message, CancellationToken cancellationToken)
        {

            try
            {
                var chequeIrregulier = await _chequeIrregulierRepository.GetAsync(message.Id);

                if (chequeIrregulier != null)
                {
                    chequeIrregulier.Type_Irregularite = message.Type_Irregularite;
                    chequeIrregulier.Debut_Lot = message.Debut_Lot;
                    chequeIrregulier.Fin_Lot = message.Fin_Lot;
                    chequeIrregulier.Date_Opposition = message.Date_Opposition;
                    chequeIrregulier.Etat = "C";

                    _chequeIrregulierRepository.Update(chequeIrregulier);
                }

                return await _chequeIrregulierRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
