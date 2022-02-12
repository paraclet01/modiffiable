using OPTICIP.API.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Interfaces
{
    public interface IDeclarationsQuerie
    {
        //Task<UsersViewModel> GetUserAsync(Guid id);
        Task<IEnumerable<ChequesIrreguliersViewModel>> GetChequesIrregulierAsync();
        Task<IEnumerable<CompteViewModel>> GetComptesAsync();
        Task<IEnumerable<IncidentChequeViewModel>> GetIncidentsChequesAsync();
        Task<IEnumerable<IncidentEffetViewModel>> GetIncidentsEffetsAsync();
        Task<IEnumerable<Pers_MoraleViewModel>> GetPersonnesMoralesAsync();
        Task<IEnumerable<Pers_PhysiqueViewModel>> GetPersonnesPhysiquesAsync();
        Task<IEnumerable<CartesViewModel>> GetCartesAsync();
        IEnumerable<DeclarationsViewModel> GetDeclarationsSync();
        Task<IEnumerable<DeclarationsViewModel>> GetDeclarationsAsync();
        Task<FileDeclarationInfoViewModel> GetFileDeclarationInfoAsync();
    }
}
