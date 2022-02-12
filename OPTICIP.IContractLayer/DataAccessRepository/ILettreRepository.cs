using OPTICIP.Entities.DataEntities;
using OPTICIP.SeedWork;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface ILettreRepository : IRepository<TLettre>
    {
        TLettre Add(TLettre Lettre);
        TLettre Update(TLettre Lettre);
    }
}
