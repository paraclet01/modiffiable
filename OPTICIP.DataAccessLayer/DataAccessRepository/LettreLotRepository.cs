using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.DataEntities;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class LettreLotRepository : ILettreLotRepository
    {
        private readonly CIPContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public LettreLotRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TLettreLot Add(TLettreLot Lettre)
        {
            return this._context.Add(Lettre).Entity;
        }

        public TLettreLot Update(TLettreLot Lettre)
        {
            this._context.Attach(Lettre);
            this._context.TLettreLot.Update(Lettre);
            this._context.Entry(Lettre).State = EntityState.Modified;
            return Lettre;
        }

        public TLettreLot GetAsync(Guid Id)
        {
            try
            {
                var Lettre = this._context.TLettreLot.Find(Id);
                return Lettre;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
