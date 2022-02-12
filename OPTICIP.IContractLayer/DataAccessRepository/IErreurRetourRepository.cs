using OPTICIP.Entities.DataEntities;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IErreurRetourRepository : IRepository<TErreurRetour>
    {
        TErreurRetour Add(TErreurRetour ErreurRetour);
        TErreurRetour Update(TErreurRetour ErreurRetour);
    }
}
