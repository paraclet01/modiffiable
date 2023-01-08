using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class CompteRepository : ICompteRepository, IDeclarationFichier_DataSaveRepository
    {
        private readonly CIPContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public CompteRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TCompte Add(TCompte Compte)
        {
            return this._context.Add(Compte).Entity;
        }

        public void Add(List<TCompte> Comptes)
        {
            this._context.AddRange(Comptes);
        }

        public TCompte Update(TCompte Compte)
        {
            this._context.Attach(Compte);
            this._context.TCompte.Update(Compte);
            this._context.Entry(Compte).State = EntityState.Modified;

            return Compte;
        }
 
        public async Task<TCompte> GetAsync(Guid Id)
        {
            try
            {
                var Compte = await this._context.TCompte.FindAsync(Id);
                return Compte;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TCompte Get(Guid Id)
        {
            try
            {
                var Compte =  this._context.TCompte.Find(Id);
                return Compte;
            }
            catch (Exception ex)
            {
                throw;
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

               return  await this._context.SaveEntitiesAsync();
            }
            catch (Exception ex)
            {
                //return false;
                throw ex;
            }
        }
    }
}
