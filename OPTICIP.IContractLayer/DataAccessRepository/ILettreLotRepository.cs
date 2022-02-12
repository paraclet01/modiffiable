using OPTICIP.Entities.DataEntities;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface ILettreLotRepository : IRepository<TLettreLot>
    {
        TLettreLot Add(TLettreLot Lettre);
        TLettreLot Update(TLettreLot Lettre);
    }
}
