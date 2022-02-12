using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IRepositoryFactory
    {
        ICompteRepository CompteRepository { get; }
        IPersPhysiqueRepository PersPhysiqueRepository { get; }
        IPersMoraleRepository PersMoraleRepository { get; }
        ILettreRepository LettreRepository { get; }
        ILettreLotRepository LettreLotRepository { get; }
        ICarteRepository CarteRepository { get; }
        IIncidentChequeRepository IncidentChequeRepository { get; }
        IChequeIrregulierRepository ChequeIrregulierRepository { get; }
        IIncidentEffetRepository IncidentEffetRepository { get; }
        IDonnees_A_DeclarerRepository Donnees_A_DeclarerRepository { get; }
        IHistorique_Lanc_ProcRepository Historique_Lanc_ProcRepository { get; }
        IErreurEnregistrementRepository ErreurEnregistrementRepository { get; }
        ILogRepository LogRepository { get; }
        IErreurRetourRepository ErreurRetourRepository { get; }
        IHistorique_DeclarationsRepository Historique_DeclarationsRepository { get; }
    }
}
