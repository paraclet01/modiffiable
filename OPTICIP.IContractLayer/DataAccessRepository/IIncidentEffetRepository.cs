using OPTICIP.Entities.Models;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IIncidentEffetRepository : IRepository<TIncidentEffet>
    {
        TIncidentEffet Add(TIncidentEffet IncidentEffet);
        TIncidentEffet Update(TIncidentEffet IncidentEffet);
        Task<TIncidentEffet> GetAsync(Guid Id);
        TIncidentEffet Get(Guid Id);
    }
}
