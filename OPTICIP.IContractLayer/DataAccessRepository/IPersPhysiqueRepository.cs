using OPTICIP.Entities.Models;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IPersPhysiqueRepository : IRepository<TPersPhysique>
    {
        TPersPhysique Add(TPersPhysique PersPhysique);
        TPersPhysique Update(TPersPhysique PersPhysique);
        Task<TPersPhysique> GetAsync(Guid Id);
        TPersPhysique Get(Guid Id);
    }
}
