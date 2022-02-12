using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class DeclarationFichier_EltEnrRepository : IDeclarationFichier_EltEnrRepository
    {
        private readonly CIPContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public DeclarationFichier_EltEnrRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TDeclarationFichier_EltEnr Add(TDeclarationFichier_EltEnr DeclarationFichier)
        {
            return this._context.Add(DeclarationFichier).Entity;
        }

        public async Task<TDeclarationFichier_EltEnr> GetAsync(Guid Id)
        {
            try
            {
                var DeclarationFichier = await this._context.TDeclarationFichier_EltEnr.FindAsync(Id);
                return DeclarationFichier;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
