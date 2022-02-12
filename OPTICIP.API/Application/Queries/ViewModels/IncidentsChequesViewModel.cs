using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class IncidentsChequesViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Num_Enr { get; set; }
        public string Agence { get; set; }
        public string RIB { get; set; }
        public string Cle_RIB { get; set; }
        public string Type_Incident { get; set; }
        public DateTime Date_Emission { get; set; }
        public DateTime Date_Refus_Paiement { get; set; }
        public DateTime Date_Presentation { get; set; }
        public DateTime Point_Depart { get; set; }
        public Int64 Montant_Nominal { get; set; }
        public Int64 Montant_Insuffisance { get; set; }
        public string Numero_Cheque { get; set; }
        public DateTime Date_Regularisation { get; set; }
        public string Identifiant { get; set; }
        public string Num_Ligne_Erreur { get; set; }
        public string Etat { get; set; }
        public Guid IdItem_Retire { get; set; }
        public DateTime Date_Declaration { get; set; }

        //==> CIP V2
        public Int64? MontPen { get; set; }
        public string BenefNom { get; set; }
        public string BenefPrenom { get; set; }
        public int? MotifCode { get; set; }
        public string MotifDesc { get; set; }
    }
}
