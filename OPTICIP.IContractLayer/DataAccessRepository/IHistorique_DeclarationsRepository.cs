using OPTICIP.Entities.DataEntities;
using OPTICIP.SeedWork;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IHistorique_DeclarationsRepository : IRepository<THistorique_Declarations>
    {
        THistorique_Declarations Add(THistorique_Declarations Historique_Declarations);
        THistorique_Declarations Update(THistorique_Declarations Historique_Declarations);
    }
}