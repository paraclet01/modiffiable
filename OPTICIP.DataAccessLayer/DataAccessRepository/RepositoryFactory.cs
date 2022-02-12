using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.IContractLayer.DataAccessRepository;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{

    public class RepositoryFactory : IRepositoryFactory
    {
        private ICompteRepository compteRepository;
        private IPersPhysiqueRepository persPhysiqueRepository;
        private IPersMoraleRepository persMoraleRepository;
        private ICarteRepository  carteRepository;
        private IIncidentChequeRepository incidentChequeRepository;
        private IChequeIrregulierRepository chequeIrregulierRepository;
        private IIncidentEffetRepository  incidentEffetRepository;
        private ILettreRepository lettreRepository;
        private ILettreLotRepository lettrelotRepository;
        private IDonnees_A_DeclarerRepository donnees_A_DeclarerRepository;
        private IHistorique_Lanc_ProcRepository historique_Lanc_ProcRepository;
        private IErreurEnregistrementRepository erreurEnregistrementRepository;
        private IErreurRetourRepository erreurRetourRepository;
        private ILogRepository logRepository;
        private IHistorique_DeclarationsRepository historique_DeclarationsRepository;

        public ICompteRepository CompteRepository 
        {
            get { return compteRepository; }
        }

        public IPersPhysiqueRepository PersPhysiqueRepository
        {
            get { return persPhysiqueRepository; }
        }

        public IPersMoraleRepository PersMoraleRepository
        {
            get { return persMoraleRepository; }
        }

        public ICarteRepository CarteRepository
        {
            get { return carteRepository; }
        }

        public IIncidentChequeRepository IncidentChequeRepository
        {
            get { return incidentChequeRepository; }
        }

        public IChequeIrregulierRepository ChequeIrregulierRepository
        {
            get { return chequeIrregulierRepository; }
        }

        public IIncidentEffetRepository IncidentEffetRepository
        {
            get { return incidentEffetRepository; }
        }

        public ILettreRepository LettreRepository
        {
            get { return lettreRepository; }
        }

        public ILettreLotRepository LettreLotRepository
        {
            get { return lettrelotRepository; }
        }

        public IDonnees_A_DeclarerRepository Donnees_A_DeclarerRepository
        {
            get { return donnees_A_DeclarerRepository; }
        }

        public IHistorique_Lanc_ProcRepository Historique_Lanc_ProcRepository
        {
            get { return historique_Lanc_ProcRepository; }
        }

        public IErreurEnregistrementRepository ErreurEnregistrementRepository
        {
            get { return erreurEnregistrementRepository; }
        }

        public IErreurRetourRepository ErreurRetourRepository
        {
            get { return erreurRetourRepository; }
        }

        public ILogRepository LogRepository
        {
            get { return logRepository; }
        }
        public IHistorique_DeclarationsRepository Historique_DeclarationsRepository
        {
            get { return historique_DeclarationsRepository; }
        }

        public RepositoryFactory(CIPContext context)
        {
            compteRepository = new CompteRepository(new CIPContext());
            persPhysiqueRepository = new PersPhysiqueRepository(new CIPContext());
            persMoraleRepository = new PersMoraleRepository(new CIPContext());
            carteRepository = new CarteRepository(new CIPContext());
            incidentChequeRepository = new IncidentChequeRepository(new CIPContext());
            chequeIrregulierRepository = new ChequeIrregulierRepository(new CIPContext());
            incidentEffetRepository = new IncidentEffetRepository(new CIPContext());
            lettreRepository = new LettreRepository(new CIPContext());
            lettrelotRepository = new LettreLotRepository(new CIPContext());
            donnees_A_DeclarerRepository = new Donnees_A_DeclarerRepository(new CIPContext());
            historique_Lanc_ProcRepository = new Historique_Lanc_ProcRepository(new CIPContext());
            erreurEnregistrementRepository = new ErreurEnregistrementRepository(new CIPContext());
            erreurRetourRepository = new ErreurRetourRepository(new CIPContext());
            logRepository = new LogRepository(new CIPContext());
            historique_DeclarationsRepository = new Historique_DeclarationsRepository(new CIPContext());
        }
    }
}
