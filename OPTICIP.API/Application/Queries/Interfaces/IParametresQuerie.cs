using OPTICIP.API.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Interfaces
{
    public interface IParametresQuerie
    {
        Task<IEnumerable<ParametresViewModel>> GetAllParametresAsync();
        Task<ParametresViewModel> GetParametresByIdAsync(int id);
        Task<IEnumerable<AgencesViewModel>> GetAgencesAsync();
        ParametresViewModel GetParametreByCodeAsync(String pCode);
        Task<AgencesViewModel> GetAgenceAsync(Guid id);
    }
}
