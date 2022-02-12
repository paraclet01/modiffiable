using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class PersMoraleRepository : IPersMoraleRepository, IDeclarationFichier_DataSaveRepository
    {
        private readonly CIPContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public PersMoraleRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TPersMorale Add(TPersMorale PersMorale)
        {
            return this._context.Add(PersMorale).Entity;
        }


        public TPersMorale Update(TPersMorale PersMorale)
        {
            this._context.Attach(PersMorale);
            this._context.TPersMorale.Update(PersMorale);
            this._context.Entry(PersMorale).State = EntityState.Modified;
            return PersMorale;
        }

        public TPersMorale Get(Guid Id)
        {
            try
            {
                var PersMorale = this._context.TPersMorale.Find(Id);
                return PersMorale;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TPersMorale> GetAsync(Guid Id)
        {
            try
            {
                return await this._context.TPersMorale.FindAsync(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Check_Data_Saved(TDeclarationFichier_EltEnr DeclarationFichier_EltEnr, Guid Id)
        {
            try
            {
                var Item = Get(Id);
                bool ItemExisted = (Item == null) ? false : true;
                if (ItemExisted)
                {
                    Item.Update("D", DeclarationFichier_EltEnr.DateDeclaration);
                }
                Update(Item);
                this._context.Add(DeclarationFichier_EltEnr);
                 return await this._context.SaveEntitiesAsync();
            }
            catch (Exception ex)
            {
                //return false;
                throw ex;
            }
        }
    }
}
