using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class DonneeRetireRepository : IDonneeRetireRepository
    {
        private readonly CIPContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public DonneeRetireRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TDonneeRetire Add(TDonneeRetire DonneeRetire)
        {
            return this._context.Add(DonneeRetire).Entity;
        }

        //public TUsers Update(TUsers User)
        //{
        //    this._context.Attach(User);
        //    this._context.TUsers.Update(User);
        //    this._context.Entry(User).State = EntityState.Modified;

        //    return User;
        //}

        public void Delete(TDonneeRetire Donnee)
        {
            this._context.TDonneeRetire.Remove(Donnee);
            //this._context.Entry(Donnee).State = EntityState.Modified;
        }

        public async Task<TDonneeRetire> GetAsync(Guid Id)
        {
            try
            {
                var DonneeRetire = await this._context.TDonneeRetire.FindAsync(Id);
                return DonneeRetire;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
