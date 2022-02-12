using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class CartesViewModel
    {

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Num_Carte { get; set; }
        public string Agence { get; set; }
        public string RIB { get; set; }
        public string Cle_RIB { get; set; }
        public DateTime Date_Fin_Validite { get; set; }
        public string Titulaire { get; set; }
        public string Type_Carte { get; set; }
        public DateTime Date_Opposition { get; set; }
        public string Num_Ligne_Erreur { get; set; }
        public string Etat { get; set; }
        public Guid IdItem_Retire { get; set; }

        //==> CIP V2
        public string Porteur { get; set; }
    }
}
