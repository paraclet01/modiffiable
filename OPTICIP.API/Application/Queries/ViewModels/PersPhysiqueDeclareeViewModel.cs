using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class PersPhysiqueDeclareeViewModel
    {
        public String numseq { get; set; }
        public String rib { get; set; }
        public String nom_compte { get; set; }
        public String nomnais { get; set; }
        public String prenom { get; set; }
        public String nommari { get; set; }
        public DateTime? datnais { get; set; }
        public String type_declaration { get; set; }
        public DateTime? datmaj { get; set; }
        public DateTime? datenv { get; set; }
        public DateTime? datdecl { get; set; }
        public String statut { get; set; }
    }
}
