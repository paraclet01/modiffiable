using MediatR;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    public class UpdateCompteCommandHandler : IRequestHandler<UpdateCompteCommand, bool>
    {
        private readonly ICompteRepository _compteRepository;
        private readonly IMediator _mediator;
        private readonly IRepositoryFactory _repositoryFactory;

        public UpdateCompteCommandHandler(IMediator mediator, ICompteRepository compteRepository, IRepositoryFactory repositoryFactory)
        {
            _compteRepository = compteRepository ?? throw new ArgumentNullException(nameof(compteRepository));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(UpdateCompteCommand message, CancellationToken cancellationToken)
        {

            try
            {
                var compte = await _compteRepository.GetAsync(message.Id);

                if (compte != null)
                {
                    compte.Date_Ouverture = message.Date_Ouverture;
                    compte.Date_Fermerture = message.Date_Fermerture;
                    compte.Etat = "C";
                    _compteRepository.Update(compte);
                }

                return await _compteRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
