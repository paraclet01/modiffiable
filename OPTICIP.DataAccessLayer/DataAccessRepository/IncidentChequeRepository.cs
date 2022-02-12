using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class IncidentChequeRepository : IIncidentChequeRepository, IDeclarationFichier_DataSaveRepository
    {
        private readonly CIPContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public IncidentChequeRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public TIncidentCheque Add(TIncidentCheque IncidentCheque)
        {
            return this._context.Add(IncidentCheque).Entity;
        }

        public TIncidentCheque Update(TIncidentCheque IncidentCheque)
        {
            this._context.Attach(IncidentCheque);
            this._context.TIncidentCheque.Update(IncidentCheque);
            this._context.Entry(IncidentCheque).State = EntityState.Modified;
            return IncidentCheque;
        }

        public async Task<TIncidentCheque> GetAsync(Guid Id)
        {
            try
            {
                return await this._context.TIncidentCheque.FindAsync(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TIncidentCheque Get(Guid Id)
        {
            try
            {
                var IncidentCheque = this._context.TIncidentCheque.Find(Id);
                return IncidentCheque;
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
