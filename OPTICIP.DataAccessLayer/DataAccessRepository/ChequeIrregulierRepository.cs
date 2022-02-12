using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class ChequeIrregulierRepository : IChequeIrregulierRepository, IDeclarationFichier_DataSaveRepository
    {
        private readonly CIPContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ChequeIrregulierRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TChequeIrregulier Add(TChequeIrregulier ChequeIrregulier)
        {
            return this._context.Add(ChequeIrregulier).Entity;
        }

        public TChequeIrregulier Update(TChequeIrregulier ChequeIrregulier)
        {
            this._context.Attach(ChequeIrregulier);
            this._context.TChequeIrregulier.Update(ChequeIrregulier);
            this._context.Entry(ChequeIrregulier).State = EntityState.Modified;
            return ChequeIrregulier;
        }

        public TChequeIrregulier Get(Guid Id)
        {
            try
            {
                var ChequeIrregulier = this._context.TChequeIrregulier.Find(Id);
                return ChequeIrregulier;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TChequeIrregulier> GetAsync(Guid Id)
        {
            try
            {
                 return await this._context.TChequeIrregulier.FindAsync(Id);
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
                    Item.Update("T", DeclarationFichier_EltEnr.DateDeclaration);
                }
                Update(Item);
                this._context.Add(DeclarationFichier_EltEnr);
              return await  this._context.SaveEntitiesAsync();
            }
            catch (Exception ex)
            {
                //return false;
                throw ex;
            }
        }
    }
}
