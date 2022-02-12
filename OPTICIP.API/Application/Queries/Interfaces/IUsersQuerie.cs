using OPTICIP.API.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.Interfaces
{
    public interface IUsersQuerie
    {
        Task<UsersViewModel> GetUserAsync(Guid id);
        //Task<UsersViewModel> GetUserAsync(String Login, String MotPasse);
        UsersViewModel GetUser(String Login, String MotPasse);
        Task<IEnumerable<UsersViewModel>> GetUsersAsync(int Statut);
    }
}
