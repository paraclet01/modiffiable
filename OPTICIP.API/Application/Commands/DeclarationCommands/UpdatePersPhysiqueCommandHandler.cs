using MediatR;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    public class UpdatePersPhysiqueCommandHandler : IRequestHandler<UpdatePersPhysiqueCommand, bool>
    {
        private readonly IPersPhysiqueRepository _persPhysiqueRepository;
        private readonly IMediator _mediator;
        private readonly IRepositoryFactory _repositoryFactory;

        public UpdatePersPhysiqueCommandHandler(IMediator mediator, IPersPhysiqueRepository persPhysiqueRepository, IRepositoryFactory repositoryFactory)
        {
            _persPhysiqueRepository = persPhysiqueRepository ?? throw new ArgumentNullException(nameof(persPhysiqueRepository));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(UpdatePersPhysiqueCommand message, CancellationToken cancellationToken)
        {

            try
            {
                var persPhysique = await _persPhysiqueRepository.GetAsync(message.Id);

                if (persPhysique != null)
                {
                    persPhysique.Nom_Naissance = message.Nom_Naissance;
                    persPhysique.Prenoms = message.Prenoms;
                    persPhysique.Lieu_Naissance = message.Lieu_Naissance;
                    persPhysique.Date_Naissance = message.Date_Naissance;
                    persPhysique.Nom_Mari = message.Nom_Mari;
                    persPhysique.Nom_Naissance_Mere = message.Nom_Naissance_Mere;
                    persPhysique.Nationalite = message.Nationalite;
                    persPhysique.Sexe = message.Sexe;
                    persPhysique.Resident_UEMOA = message.Resident_UEMOA;
                    persPhysique.Num_Carte_Iden = message.Num_Carte_Iden;
                    persPhysique.Responsable = message.Responsable;
                    persPhysique.Mandataire = message.Mandataire;
                    persPhysique.Adresse = message.Adresse;
                    persPhysique.Code_Pays = message.Code_Pays;
                    persPhysique.Ville = message.Ville;
                    persPhysique.Num_Reg_Com = message.Num_Reg_Com;
                    persPhysique.Etat = "C";


                    //==> CIP V2
                    persPhysique.EmailTitu =    message.EmailTitu;
                    persPhysique.NomContact =   message.NomContact;
                    persPhysique.PnomContact =  message.PnomContact;
                    persPhysique.AdrContact =   message.AdrContact;
                    persPhysique.EmailContact = message.EmailContact;

                    _persPhysiqueRepository.Update(persPhysique);                
                }

                return await _persPhysiqueRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
