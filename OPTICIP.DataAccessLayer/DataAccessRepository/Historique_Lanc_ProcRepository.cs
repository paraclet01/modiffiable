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
    public class Historique_Lanc_ProcRepository : IHistorique_Lanc_ProcRepository
    {
        private readonly CIPContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public Historique_Lanc_ProcRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public THistorique_Lanc_Proc Add(THistorique_Lanc_Proc historique_Lanc_Proc)
        {
            return this._context.Add(historique_Lanc_Proc).Entity;
        }

        public THistorique_Lanc_Proc Update(THistorique_Lanc_Proc historique_Lanc_Proc)
        {
            this._context.Attach(historique_Lanc_Proc);
            this._context.THistorique_Lanc_Proc.Update(historique_Lanc_Proc);
            this._context.Entry(historique_Lanc_Proc).State = EntityState.Modified;

            return historique_Lanc_Proc;
        }
    }
}
