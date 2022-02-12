using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class IncidentDePaiementEffetViewModel
    {
        public String numseq { get; set; }
        public String rib { get; set; }
        public String nom { get; set; }
        public String mnt { get; set; }
        public String motif_rejet { get; set; }
        public DateTime? datech { get; set; }
        public String type_declaration { get; set; }
        public DateTime? datmaj { get; set; }
        public DateTime? datenv { get; set; }
        public DateTime? datdecl { get; set; }
        public String statut { get; set; }
    }
}
