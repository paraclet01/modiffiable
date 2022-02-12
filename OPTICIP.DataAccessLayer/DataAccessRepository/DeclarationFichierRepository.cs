using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class DeclarationFichierRepository : IDeclarationFichierRepository
    {
        private readonly CIPContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public DeclarationFichierRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TDeclarationFichier Add(TDeclarationFichier DeclarationFichier)
        {
            return this._context.Add(DeclarationFichier).Entity;
        }

        public async Task<TDeclarationFichier> GetAsync(Guid Id)
        {
            try
            {
                var DeclarationFichier = await this._context.TDeclarationFichier.FindAsync(Id);
                return DeclarationFichier;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
