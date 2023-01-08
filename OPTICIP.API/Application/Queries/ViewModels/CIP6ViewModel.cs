using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class CIP6ViewModel
    {
        public string numseq { get; set; }
        public string compte { get; set; }
        public string codbnq { get; set; }
        public string codgch { get; set; }
        public string clerib { get; set; }
        public string motif { get; set; }
        public string chqref1 { get; set; }
        public string chqref2 { get; set; }
        public DateTime? datoppo { get; set; }
        public string explmaj { get; set; }
        public DateTime? datmaj { get; set; }
        public string type { get; set; }
        public string state { get; set; }
        public string valide { get; set; }
        public string numlig { get; set; }
        public DateTime? datenv { get; set; }
    }
}
