using OPTICIP.Entities.DataEntities;
using OPTICIP.SeedWork;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IDonnees_A_DeclarerRepository : IRepository<TDonnees_A_Declarer>
    {
        TDonnees_A_Declarer Add(TDonnees_A_Declarer Donnees_A_Declarer);
        TDonnees_A_Declarer Update(TDonnees_A_Declarer Donnees_A_Declarer);
    }
}
