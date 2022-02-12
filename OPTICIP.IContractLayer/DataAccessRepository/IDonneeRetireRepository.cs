using OPTICIP.Entities.Models;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IDonneeRetireRepository : IRepository<TDonneeRetire>
    {
        TDonneeRetire Add(TDonneeRetire DonneeRetire);
        //TUsers Update(TUsers User);
        Task<TDonneeRetire> GetAsync(Guid Id);
        void Delete(TDonneeRetire DonneeRetire);
    }
}
