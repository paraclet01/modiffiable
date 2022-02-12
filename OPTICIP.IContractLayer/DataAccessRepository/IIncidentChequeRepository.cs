using OPTICIP.Entities.Models;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IIncidentChequeRepository : IRepository<TIncidentCheque>
    {
        TIncidentCheque Add(TIncidentCheque IncidentCheque);
        TIncidentCheque Update(TIncidentCheque IncidentCheque);
        Task<TIncidentCheque> GetAsync(Guid Id);
        TIncidentCheque Get(Guid Id);
    }
}
