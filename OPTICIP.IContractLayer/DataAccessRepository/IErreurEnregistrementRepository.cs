using OPTICIP.Entities.DataEntities;
using OPTICIP.SeedWork;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IErreurEnregistrementRepository : IRepository<TErreurEnregistrement>
    {
        TErreurEnregistrement Add(TErreurEnregistrement ErreurEnregistrement);
        TErreurEnregistrement Update(TErreurEnregistrement ErreurEnregistrement);
    }
}
