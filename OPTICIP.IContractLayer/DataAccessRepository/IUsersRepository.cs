using OPTICIP.Entities.Models;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IUsersRepository : IRepository<TUsers>
    {
        TUsers Add(TUsers User);
        TUsers Update(TUsers User);
        Task<TUsers> GetAsync(Guid Id);
    }
}
