using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class ParametresRepository : IGeneriqueRepository<TParametres>
    {
        private readonly CIPContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ParametresRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TParametres Add(TParametres Parametre)
        {
            return this._context.Add(Parametre).Entity;
        }

        public TParametres Update(TParametres Parametre)
        {
            this._context.Attach(Parametre);
            this._context.TParametres.Update(Parametre);
            this._context.Entry(Parametre).State = EntityState.Modified;

            return Parametre;
        }

        public async Task<TParametres> GetAsync(Guid Id)
        {
            try
            {
                var Parametre = await this._context.TParametres.FindAsync(Id);
                return Parametre;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TParametres> GetAsync(int Id)
        {
            try
            {
                var Parametre = await this._context.TParametres.FindAsync(Id);
                return Parametre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
