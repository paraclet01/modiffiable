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
    public class Donnees_A_DeclarerRepository : IDonnees_A_DeclarerRepository
    {
        private readonly CIPContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public Donnees_A_DeclarerRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TDonnees_A_Declarer Add(TDonnees_A_Declarer Donnees_A_Declarer)
        {
            return this._context.Add(Donnees_A_Declarer).Entity;
        }

        public TDonnees_A_Declarer Update(TDonnees_A_Declarer Donnees_A_Declarer)
        {
            this._context.Attach(Donnees_A_Declarer);
            this._context.TDonnees_A_Declarer.Update(Donnees_A_Declarer);
            this._context.Entry(Donnees_A_Declarer).State = EntityState.Modified;
            return Donnees_A_Declarer;
        }
    }
}
