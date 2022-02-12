using OPTICIP.Entities.DataEntities;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IHistorique_Lanc_ProcRepository : IRepository<THistorique_Lanc_Proc>
    {
        THistorique_Lanc_Proc Add(THistorique_Lanc_Proc historique_Lanc_Proc);
        THistorique_Lanc_Proc Update(THistorique_Lanc_Proc historique_Lanc_Proc);
    }
}
