using OPTICIP.Entities.Models;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IGeneriqueRepository<T> : IRepository<T> where T : IAggregateRoot
    {
        T Add(T User);
        T Update(T User);
        Task<T> GetAsync(Guid Id);
        Task<T> GetAsync(int Id);
    }
}
