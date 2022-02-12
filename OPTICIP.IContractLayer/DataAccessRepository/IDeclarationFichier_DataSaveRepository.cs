using OPTICIP.Entities.Models;
using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OPTICIP.IContractLayer.DataAccessRepository
{
    public interface IDeclarationFichier_DataSaveRepository
    {
        Task<Boolean> Check_Data_Saved(TDeclarationFichier_EltEnr DeclarationFichier_EltEnr, Guid Id );
        //Object GetAsync(Guid Id);
        //Object Update(Object Carte);
    }
}
