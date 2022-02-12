using OPTICIP.Entities.DataEntities;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface ICarteRepository : IRepository<TCarte>
    {
        TCarte Add(TCarte Carte);
        //TCarte Update(TCarte Carte);
        //TCarte GetAsync(int Id);
    }
}
