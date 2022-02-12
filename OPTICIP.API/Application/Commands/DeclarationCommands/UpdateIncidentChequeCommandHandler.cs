using MediatR;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    public class UpdateIncidentChequeCommandHandler : IRequestHandler<UpdateIncidentChequeCommand, bool>
    {
        private readonly IIncidentChequeRepository _incidentChequeRepository;
        private readonly IMediator _mediator;
        private readonly IRepositoryFactory _repositoryFactory;

        public UpdateIncidentChequeCommandHandler(IMediator mediator, IIncidentChequeRepository incidentChequeRepository, IRepositoryFactory repositoryFactory)
        {
            _incidentChequeRepository = incidentChequeRepository ?? throw new ArgumentNullException(nameof(incidentChequeRepository));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(UpdateIncidentChequeCommand message, CancellationToken cancellationToken)
        {

            try
            {
                var incidentCheque = await _incidentChequeRepository.GetAsync(message.Id);

                if (incidentCheque != null)
                {
                    incidentCheque.Type_Incident = message.Type_Incident;
                    incidentCheque.Date_Emission = message.Date_Emission;
                    incidentCheque.Date_Refus_Paiement = message.Date_Refus_Paiement;
                    incidentCheque.Date_Presentation = message.Date_Presentation;
                    incidentCheque.Point_Depart = message.Point_Depart;
                    incidentCheque.Montant_Nominal = message.Montant_Nominal;
                    incidentCheque.Montant_Insuffisance = message.Montant_Insuffisance;
                    incidentCheque.Numero_Cheque = message.Numero_Cheque;
                    incidentCheque.Date_Regularisation = message.Date_Regularisation;
                    incidentCheque.Identifiant = (message.Identifiant == null ? "" :  message.Identifiant);
                    incidentCheque.Etat = "C";

                    //==> CIP V2
                    incidentCheque.MontPen =        message.MontPen;
                    incidentCheque.BenefNom =       message.BenefNom;
                    incidentCheque.BenefPrenom =    message.BenefPrenom;
                    incidentCheque.MotifCode =      message.MotifCode;
                    incidentCheque.MotifDesc =      message.MotifDesc;

                    _incidentChequeRepository.Update(incidentCheque);
                }

                return await _incidentChequeRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
