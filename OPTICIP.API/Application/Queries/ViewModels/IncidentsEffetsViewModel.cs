using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class IncidentsEffetsViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Num_Enr { get; set; }
        public string Agence { get; set; }
        public string RIB { get; set; }
        public string Cle_RIB { get; set; }
        public DateTime Echeance { get; set; }
        public Int32 Montant { get; set; }
        public DateTime Date_Refus_Paiement { get; set; }
        public string Type_Effet { get; set; }
        public string Motif_Non_Paiement { get; set; }
        public string Avis_Domiciliation { get; set; }
        public string Ordre_Paiement_Perm { get; set; }
        public string Num_Ligne_Erreur { get; set; }
        public string Etat { get; set; }
        public Guid IdItem_Retire { get; set; }
        public DateTime Date_Declaration { get; set; }

        //==> CIP V2
        public string MotifDesc { get; set; }
    }
}
