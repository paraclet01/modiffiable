using OPTICIP.Entities.Models;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IPersMoraleRepository : IRepository<TPersMorale>
    {
        TPersMorale Add(TPersMorale PersMorale);
        TPersMorale Update(TPersMorale PersMorale);
        Task<TPersMorale> GetAsync(Guid Id);
        TPersMorale Get(Guid Id);
    }
}
