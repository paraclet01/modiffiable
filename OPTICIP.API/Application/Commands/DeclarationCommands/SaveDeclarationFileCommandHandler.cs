using MediatR;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.DataAccessLayer.DataAccessRepository;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    public class SaveDeclarationFileCommandHandler : IRequestHandler<SaveDeclarationFileCommand, bool>
    {
        private readonly IDeclarationFichierRepository _declarationFichierRepository;
        private readonly IDeclarationQueries _declarationQueries;
        private readonly IMediator _mediator;

        public SaveDeclarationFileCommandHandler(IMediator mediator, IDeclarationFichierRepository declarationFichierRepository, IDeclarationQueries declarationQueries /*, IIdentityService identityService*/)
        {
            _declarationFichierRepository = declarationFichierRepository ?? throw new ArgumentNullException(nameof(declarationFichierRepository));
            _declarationQueries = declarationQueries ?? throw new ArgumentNullException(nameof(declarationQueries));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(SaveDeclarationFileCommand message, CancellationToken cancellationToken)
        {
            try
            {

                //var ListDonneesADeclarer = _declarationQueries.GetDeclarationsSync();
                //var Count = ListDonneesADeclarer.ToList().Count;

                //if (Count <= 0)
                //    return false;

                //var declarationFichier = new TDeclarationFichier(Guid.NewGuid(), message.NomFichier, DateTime.Parse(message.DateDeclaration));
                //_declarationFichierRepository.Add(declarationFichier);

                //foreach (var item in ListDonneesADeclarer)
                //{
                //    if (item.Type_Data.Equals("0000") || item.Type_Data.Equals("1000"))
                //        continue;

                //    //var ClassFromType = GetClassFromType(item.Type_Data);
                //    var RepositoryFromType = GetRepositoryFromType(item.Type_Data) ;

                //    var Num_Enr = (item.Data.Split(";"))[1];
                //    var Code = (item.Data.Split(";"))[0];

                //    TDeclarationFichier_EltEnr DeclarationFichier_EltEnr = 
                //        new TDeclarationFichier_EltEnr(message.NomFichier, Num_Enr,Code, DateTime.Parse(message.DateDeclaration));
                //  if( await RepositoryFromType.Check_Data_Saved(DeclarationFichier_EltEnr, item.Id) )
                //    {
                //        Count--;
                //    }
                //}

                //if (Count > 0)
                //    return false;

                return await _declarationFichierRepository.UnitOfWork.SaveEntitiesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IDeclarationFichier_DataSaveRepository GetRepositoryFromType(string type)
        {

            Dictionary<String, IDeclarationFichier_DataSaveRepository> ListRepositoryFromType = new Dictionary<string, IDeclarationFichier_DataSaveRepository>();

            ListRepositoryFromType.Add("01", new CompteRepository(new CIPContext()));
            ListRepositoryFromType.Add("02", new PersPhysiqueRepository(new CIPContext()));
            ListRepositoryFromType.Add("03", new PersMoraleRepository(new CIPContext()));
            ListRepositoryFromType.Add("04", new CarteRepository(new CIPContext()));
            ListRepositoryFromType.Add("05", new IncidentChequeRepository(new CIPContext()));
            ListRepositoryFromType.Add("06", new ChequeIrregulierRepository(new CIPContext()));
            ListRepositoryFromType.Add("07", new IncidentEffetRepository(new CIPContext()));

            return ListRepositoryFromType[type];
        }

        public IAggregateRoot GetClassFromType(string type)
        {

            Dictionary<String, IAggregateRoot> ListClassFromType = new Dictionary<string, IAggregateRoot>();

            ListClassFromType.Add("01", new TCompte());
            ListClassFromType.Add("02", new TPersPhysique());
            ListClassFromType.Add("03", new TPersMorale());
            ListClassFromType.Add("05", new TIncidentCheque());
            ListClassFromType.Add("06", new TChequeIrregulier());
            ListClassFromType.Add("07", new TIncidentEffet());

            return ListClassFromType[type];
        }
    }
}
