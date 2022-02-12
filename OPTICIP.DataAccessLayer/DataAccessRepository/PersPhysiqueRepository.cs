using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class PersPhysiqueRepository : IPersPhysiqueRepository, IDeclarationFichier_DataSaveRepository
    {
        private readonly CIPContext _context;
        
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public PersPhysiqueRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TPersPhysique Add(TPersPhysique PersPhysique)
        {
            return this._context.Add(PersPhysique).Entity;
        }

        public async Task<TPersPhysique> GetAsync(Guid Id)
        {
            try
            {
                var persPhysique = await this._context.TPersPhysique.FindAsync(Id);
                return persPhysique;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPersPhysique Update(TPersPhysique PersPhysique)
        {
            this._context.Attach(PersPhysique);
            this._context.TPersPhysique.Update(PersPhysique);
            this._context.Entry(PersPhysique).State = EntityState.Modified;

            return PersPhysique;
        }

        public TPersPhysique Get(Guid Id)
        {
            try
            {
                var PersPhysique = this._context.TPersPhysique.Find(Id);
                return PersPhysique;
            }
            catch (Exception ex)
            {
                //return false;
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
