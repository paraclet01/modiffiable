using OPTICIP.Entities.Models;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IDeclarationFichier_EltEnrRepository : IRepository<TDeclarationFichier_EltEnr>
    {
        TDeclarationFichier_EltEnr Add(TDeclarationFichier_EltEnr DeclarationFichier_EltEnr);
        Task<TDeclarationFichier_EltEnr> GetAsync(Guid Id);
    }
}
