using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.DataEntities;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class LettreRepository : ILettreRepository
    {
        private readonly CIPContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public LettreRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TLettre Add(TLettre Lettre)
        {
            return this._context.Add(Lettre).Entity;
        }

        public TLettre Update(TLettre Lettre)
        {
            this._context.Attach(Lettre);
            this._context.TLettre.Update(Lettre);
            this._context.Entry(Lettre).State = EntityState.Modified;
            return Lettre;
        }

        public TLettre GetAsync(Guid Id)
        {
            try
            {
                var Lettre = this._context.TLettre.Find(Id);
                return Lettre;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
