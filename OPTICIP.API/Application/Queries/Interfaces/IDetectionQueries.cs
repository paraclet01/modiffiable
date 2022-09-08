using OPTICIP.API.Application.Queries.ViewModels;
using OPTICIP.DataAccessLayer.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Interfaces
{
    public interface IDetectionQueries
    {
        Task<IEnumerable<CIP1ViewModel>> LancerDetectionComptes_New();
        Task LancerDetectionComptes(string userID);
        Task LancerDetectionPersonnesPhysiques(string userID);
        Task LancerDetectionPersonnesMorales(string userID);
        Task LancerDetectionCartes(string userID);
        Task LancerDetectionChequesTFJ();
        Task LancerDetectionCheques(string userID);
        Task LancerTraitementCheques();
        Task LancerDetectionChequesIrreguliers(string userID);
        Task LancerDetectionEffetsTFJ();
        Task<IEnumerable<ChqRejViewModel>> ListChequesDetectesTFJ_Old();
        Task<IEnumerable<EffRejViewModel>> ListEffetsDetectesTFJ();
        Task LancerDetectionEffets(string userID);
        Task<IEnumerable<IncidChqEbViewModel>> GetIncidChqEbAsync();
        Task<IEnumerable<IncidChqEbViewModel>> GetIncidChqEbAsync(string compte, string cheque, DateTime dateOperation);
        Task<IEnumerable<IncidChqViewModel>> GetIncidChqAsync();
        Task<IEnumerable<IncidChqViewModel>> GetIncidChqAsync(string compte, string cheque);
        Task<int> UpdateIncidChqEbAsync(string compte, string cheque, DateTime dateOperation, string benef, DateTime datemi);
        Task<int> UpdateIncidChqAsync(string compte, string cheque, DateTime datreg, DateTime djustif, string mreg, string numpen, decimal mtpen);

        Task<IEnumerable<ChqRejViewModel>> ListChequesDetectesTFJ(int iOption);
        Task<int> UpdatIncidentChqBenef(Guid pIdCheque, string benefNom, string benefPrenom);
    }
}
