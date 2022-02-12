using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.DataEntities;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.DataAccessRepository
{
    public class CarteRepository : ICarteRepository, IDeclarationFichier_DataSaveRepository
    {
        private readonly CIPContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public CarteRepository(CIPContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TCarte Add(TCarte Carte)
        {
            return this._context.Add(Carte).Entity;
        }

        public TCarte Update(TCarte Carte)
        {
            this._context.Attach(Carte);
            this._context.TCarte.Update(Carte);
            this._context.Entry(Carte).State = EntityState.Modified;
            return Carte;
        }

        public TCarte GetAsync(Guid Id)
        {
            try
            {
                var Carte = this._context.TCarte.Find(Id);
                return Carte;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public  async Task<bool> Check_Data_Saved(TDeclarationFichier_EltEnr DeclarationFichier_EltEnr, Guid Id)
        {
            try
            {
                var Item =  GetAsync(Id);
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
