using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class IncidChqViewModel
    {
        public string nooper { get; set; }
        public string expl { get; set; }
        public string explmaj { get; set; }
        public string chqref { get; set; }
        public string client { get; set; }
        public string extint { get; set; }
        public string compte { get; set; }
        public DateTime datinc { get; set; }
        public DateTime datchq { get; set; }
        public DateTime datreg { get; set; }
        public DateTime datjour { get; set; }
        public DateTime datmaj { get; set; }
        public string devise { get; set; }
        public Int64 mntdev { get; set; }
        public Int64 mntfrf { get; set; }
        public Int64 mntrej { get; set; }
        public Int64 mtpen { get; set; }
        public Int64 posfrf { get; set; }
        public string typinc { get; set; }
        public string numlig { get; set; }
        public string reg { get; set; }
        public string mreg { get; set; }
        public string etatreg { get; set; }
        public string benef { get; set; }
        public string typinf { get; set; }
        public string numseq { get; set; }
        public DateTime datpre { get; set; }
        public DateTime datdep { get; set; }
        public Int64 mntins { get; set; }
        public string numpen { get; set; }
        public string valide { get; set; }
        public DateTime datave { get; set; }
        public DateTime djustif { get; set; }
        public DateTime datinj { get; set; }
        public DateTime datinf { get; set; }
    }
}
