using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class ErreurEnregistrementDeclareViewModel
    {
        public Guid Id { get; set; }
        public Guid EnregistrementID { get; set; }
        public string EnregistrementTable { get; set; }
        public string Numero_Ligne_Erreur { get; set; }
        public string Code_Erreur { get; set; }
        public string Champ_Erreur { get; set; }
        public string Libelle_Erreur { get; set; }
    }
}
