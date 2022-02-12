using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class IncidentChequeViewModel
    {

        public Guid Id { get; set; }
        public string Code { get; private set; }
        public string Num_Enr { get; private set; }
        public string RIB { get; private set; }
        public string Cle_RIB { get; private set; }
        public string Type_Incident { get; private set; }
        public DateTime Date_Emission { get; private set; }
        public DateTime Date_Refus_Paiement { get; private set; }
        public DateTime Date_Presentation { get; private set; }
        public DateTime Point_Depart { get; private set; }
        public Int64 Montant_Nominal { get; private set; }
        public Int64 Montant_Insuffisance { get; private set; }
        public string Numero_Cheque { get; private set; }
        public DateTime Date_Regularisation { get; private set; }
        public string Identifiant { get; private set; }
        public string Num_Ligne_Erreur { get; private set; }
        public string Etat { get; private set; }
    }
}
