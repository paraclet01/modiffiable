using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class IncidentEffetRepository : IIncidentEffetRepository, IDeclarationFichier_DataSaveRepository
    {
        private readonly CIPContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public IncidentEffetRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TIncidentEffet Add(TIncidentEffet IncidentEffet)
        {
            return this._context.Add(IncidentEffet).Entity;
        }

        public TIncidentEffet Update(TIncidentEffet IncidentEffet)
        {
            this._context.Attach(IncidentEffet);
            this._context.TIncidentEffet.Update(IncidentEffet);
            this._context.Entry(IncidentEffet).State = EntityState.Modified;
            return IncidentEffet;
        }

        public async Task<TIncidentEffet> GetAsync(Guid Id)
        {
            try
            {
                return await this._context.TIncidentEffet.FindAsync(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TIncidentEffet Get(Guid Id)
        {
            try
            {
                var IncidentEffet = this._context.TIncidentEffet.Find(Id);
                return IncidentEffet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Check_Data_Saved(TDeclarationFichier_EltEnr DeclarationFichier_EltEnr,Guid Id)
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
