using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class UsersViewModel
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenoms { get; set; }
        public string Login { get; set; }
        public string MotPasse { get; set; }
        public String IdProfil { get; set; }
        public String Profil { get; set; }
        public int Statut { get; set; }
        public String StatutLibelle { get; set; }
    }
}
