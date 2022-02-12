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
    public class UpdateIncidentEffetCommandHandler : IRequestHandler<UpdateIncidentEffetCommand, bool>
    {
        private readonly IIncidentEffetRepository _incidentEffetRepository;
        private readonly IMediator _mediator;
        private readonly IRepositoryFactory _repositoryFactory;

        public UpdateIncidentEffetCommandHandler(IMediator mediator, IIncidentEffetRepository incidentEffetRepository, IRepositoryFactory repositoryFactory)
        {
            _incidentEffetRepository = incidentEffetRepository ?? throw new ArgumentNullException(nameof(incidentEffetRepository));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(UpdateIncidentEffetCommand message, CancellationToken cancellationToken)
        {
            try
            {
                var incidentEffet = await _incidentEffetRepository.GetAsync(message.Id);

                if (incidentEffet != null)
                {
                    incidentEffet.Echeance = message.Echeance;
                    incidentEffet.Montant = message.Montant;
                    incidentEffet.Date_Refus_Paiement = message.Date_Refus_Paiement;
                    incidentEffet.Type_Effet = message.Type_Effet;
                    incidentEffet.Motif_Non_Paiement = message.Motif_Non_Paiement;
                    incidentEffet.Avis_Domiciliation = message.Avis_Domiciliation;
                    incidentEffet.Ordre_Paiement_Perm = message.Ordre_Paiement_Perm;
                    incidentEffet.Etat = "C";

                    //==> CIP V2
                    incidentEffet.MotifDesc = message.MotifDesc;

                    _incidentEffetRepository.Update(incidentEffet);
                }
               
                return await _incidentEffetRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
