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
    public class Historique_DeclarationsRepository : IHistorique_DeclarationsRepository
    {
        private readonly CIPContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public Historique_DeclarationsRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public THistorique_Declarations Add(THistorique_Declarations Historique_Declarations)
        {
            return this._context.Add(Historique_Declarations).Entity;
        }

        public THistorique_Declarations Update(THistorique_Declarations Historique_Declarations)
        {
            this._context.Attach(Historique_Declarations);
            this._context.THistorique_Declarations.Update(Historique_Declarations);
            this._context.Entry(Historique_Declarations).State = EntityState.Modified;
            return Historique_Declarations;
        }
    }
}
