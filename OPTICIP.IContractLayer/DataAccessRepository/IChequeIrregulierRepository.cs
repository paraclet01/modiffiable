using OPTICIP.Entities.Models;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IChequeIrregulierRepository : IRepository<TChequeIrregulier>
    {
        TChequeIrregulier Add(TChequeIrregulier ChequeIrregulier);
        TChequeIrregulier Update(TChequeIrregulier ChequeIrregulier);
        Task<TChequeIrregulier> GetAsync(Guid Id);
        TChequeIrregulier Get(Guid Id);
    }
}
