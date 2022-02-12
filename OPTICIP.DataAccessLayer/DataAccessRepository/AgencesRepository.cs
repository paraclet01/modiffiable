using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class AgencesRepository : IGeneriqueRepository<TAgences>
    {
        private readonly CIPContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public AgencesRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TAgences Add(TAgences Agence)
        {
            return this._context.Add(Agence).Entity;
        }

        public TAgences Update(TAgences Agence)
        {
            this._context.Attach(Agence);
            this._context.TAgences.Update(Agence);
            this._context.Entry(Agence).State = EntityState.Modified;

            return Agence;
        }

        public async Task<TAgences> GetAsync(Guid Id)
        {
            try
            {
                var Agence = await this._context.TAgences.FindAsync(Id);
                return Agence;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TAgences> GetAsync(int Id)
        {
            try
            {
                var Agence = await this._context.TAgences.FindAsync(Id);
                return Agence;
            }
            catch (Exception ex)
            {
                throw;
            }
            //return new TAgences();
        }
    }
}
