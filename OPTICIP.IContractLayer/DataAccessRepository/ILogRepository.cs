using OPTICIP.Entities.DataEntities;
using OPTICIP.SeedWork;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface ILogRepository : IRepository<TLog>
    {
        TLog Add(TLog Log);
        TLog Update(TLog TLog);
    }
}
