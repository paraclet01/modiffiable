using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.DataEntities;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class LogRepository : ILogRepository
    {
        private readonly CIPContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public LogRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TLog Add(TLog Log)
        {
            return this._context.Add(Log).Entity;
        }

        public TLog Update(TLog Log)
        {
            this._context.Attach(Log);
            this._context.TLog.Update(Log);
            this._context.Entry(Log).State = EntityState.Modified;

            return Log;
        }

        public TLog GetAsync(Guid Id)
        {
            try
            {
                var Log = this._context.TLog.Find(Id);
                return Log;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
