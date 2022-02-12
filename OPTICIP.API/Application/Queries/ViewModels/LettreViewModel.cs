using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class LettreViewModel
    {
        public Guid Id { get; set; }
        public string Nooper { get; set; }
        public string Client { get; set; }
        public string Numero_Compte { get; set; }
        public string Numero_Cheque { get; set; }
        public string Montant_Incident { get; set; }
        public DateTime Date_Incident { get; set; }
        public DateTime Date_Generation { get; set; }
        public string Chemin { get; set; }
        public string Type_Lettre { get; set; }
        public string Nom_Lettre { get; set; }
        public string IDP { get; set; }

    }
}
