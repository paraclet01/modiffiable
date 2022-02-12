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
    public class ErreurEnregistrementRepository : IErreurEnregistrementRepository
    {
        private readonly CIPContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ErreurEnregistrementRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TErreurEnregistrement Add(TErreurEnregistrement ErreurEnregistrement)
        {
            return this._context.Add(ErreurEnregistrement).Entity;
        }

        public TErreurEnregistrement Update(TErreurEnregistrement ErreurEnregistrement)
        {
            this._context.Attach(ErreurEnregistrement);
            this._context.TErreurEnregistrement.Update(ErreurEnregistrement);
            this._context.Entry(ErreurEnregistrement).State = EntityState.Modified;
            return ErreurEnregistrement;
        }
    }
}
