using MediatR;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    public class UpdatePersMoraleCommandHandler : IRequestHandler<UpdatePersMoraleCommand, bool>
    {
        private readonly IPersMoraleRepository _persMoraleRepository;
        private readonly IMediator _mediator;
        private readonly IRepositoryFactory _repositoryFactory;

        public UpdatePersMoraleCommandHandler(IMediator mediator, IPersMoraleRepository persMoraleRepository, IRepositoryFactory repositoryFactory)
        {
            _persMoraleRepository = persMoraleRepository ?? throw new ArgumentNullException(nameof(persMoraleRepository));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(UpdatePersMoraleCommand message, CancellationToken cancellationToken)
        {

            try
            {
                var persMorale = await _persMoraleRepository.GetAsync(message.Id);

                if (persMorale != null)
                {
                    persMorale.Code_Pays = message.Code_Pays;
                    persMorale.Cat_Personne = message.Cat_Personne;
                    persMorale.Iden_Personne = message.Iden_Personne;
                    persMorale.Raison_Soc = message.Raison_Soc;
                    persMorale.Sigle = message.Sigle;
                    persMorale.Code_Activite = message.Code_Activite;
                    persMorale.Responsable = message.Responsable;
                    persMorale.Mandataire = message.Mandataire;
                    persMorale.Adresse = message.Adresse;
                    persMorale.Ville = message.Ville;
                    persMorale.Iden_BCEAO = message.Iden_BCEAO;
                    persMorale.Etat = "C";

                    //==> CIP V2
                    persMorale.Email = message.Email;

                    _persMoraleRepository.Update(persMorale);                    
                }

                return await _persMoraleRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
