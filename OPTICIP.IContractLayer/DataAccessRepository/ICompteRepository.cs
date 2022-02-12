using OPTICIP.Entities.Models;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface ICompteRepository : IRepository<TCompte>
    {
        TCompte Add(TCompte Compte);
        TCompte Update(TCompte Compte);
        Task<TCompte> GetAsync(Guid Id);
        TCompte Get(Guid Id);
    }
}
