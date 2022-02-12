using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class CompteMandataireViewModel
    {
        public String numseq { get; set; }
        public String rib { get; set; }
        public String nom_compte { get; set; }
        public DateTime? datouv { get; set; }
        public DateTime? datfrm { get; set; }
        public String type_declaration { get; set; }
        public DateTime? datmaj { get; set; }
        public DateTime? datenv { get; set; }
        public DateTime? datdecl { get; set; }
        public String statut { get; set; }
    }
}
