using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class IncidentEffetViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; private set; }
        public string Num_Enr { get; private set; }
        public string RIB { get; private set; }
        public string Cle_RIB { get; private set; }
        public DateTime Echeance { get; private set; }
        public Int32 Montant { get; private set; }
        public DateTime Date_Refus_Paiement { get; private set; }
        public string Type_Effet { get; private set; }
        public string Motif_Non_Paiement { get; private set; }
        public string Avis_Domiciliation { get; private set; }
        public string Ordre_Paiement_Perm { get; private set; }
        public string Num_Ligne_Erreur { get; private set; }
        public string Etat { get; private set; }

    }
}
