using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class IncidentDePaiementChequeViewModel
    {
        public String numseq { get; set; }
        public String rib { get; set; }
        public String nom { get; set; }
        public String nocheque { get; set; }
        public String montant { get; set; }
        public String beneficiaire { get; set; }
        public DateTime? date_emis { get; set; }
        public DateTime? datinc { get; set; }
        public DateTime? date_presentation { get; set; }
        public String type_incident { get; set; }
        public String type_declaration { get; set; }
        public DateTime? datmaj { get; set; }
        public DateTime? datenv { get; set; }
        public DateTime? datdecl { get; set; }
        public String statut { get; set; }
        public String detail_incident { get; set; }

    }
}
