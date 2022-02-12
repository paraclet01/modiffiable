using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class CompteViewModel
    {
        public Guid Id { get; set; }
        public string Code { get;  set; }
        public string Num_Enr { get;  set; }
        public string Agence { get; set; }
        public string RIB { get;  set; }
        public string Cle_RIB { get;  set; }
        public DateTime Date_Ouverture { get;  set; }
        public DateTime Date_Fermerture { get;  set; }
        public DateTime Date_Declaration { get; set; }
        public string Num_Ligne_Erreur { get;  set; }
        public string Etat { get;  set; }
        public Guid IdItem_Retire { get; set; }

    }
}
