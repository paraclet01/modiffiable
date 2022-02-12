using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.DataEntities;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class ErreurRetourRepository : IErreurRetourRepository
    {
        private readonly CIPContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ErreurRetourRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TErreurRetour Add(TErreurRetour ErreurRetour)
        {
            return this._context.Add(ErreurRetour).Entity;
        }

        public TErreurRetour Update(TErreurRetour ErreurRetour)
        {
            this._context.Attach(ErreurRetour);
            this._context.TErreurRetour.Update(ErreurRetour);
            this._context.Entry(ErreurRetour).State = EntityState.Modified;
            return ErreurRetour;
        }
    }
}
