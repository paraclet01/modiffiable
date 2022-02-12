using OPTICIP.Entities.Models;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IDeclarationFichierRepository : IRepository<TDeclarationFichier>
    {
        TDeclarationFichier Add(TDeclarationFichier DeclarationFichier);
        Task<TDeclarationFichier> GetAsync(Guid Id);

    }
}
