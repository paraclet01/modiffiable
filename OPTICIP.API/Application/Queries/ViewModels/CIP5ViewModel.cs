using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class CIP5ViewModel
    {
        public string numseq { get; set; }
        public string compte { get; set; }
        public string codbnq { get; set; }
        public string codgch { get; set; }
        public string clerib { get; set; }
        public string typinc { get; set; }
        public DateTime datchq { get; set; }
        public DateTime darreg { get; set; }
        public DateTime datpre { get; set; }
        public DateTime datinc { get; set; }
        public Int64 mntfrf { get; set; }
        public Int64 mntrej { get; set; }
        public string chqref { get; set; }
        public DateTime datreg { get; set; }
        public string numpen { get; set; }
        public string explmaj { get; set; }
        public DateTime datmaj { get; set; }
        public string type { get; set; }
        public string state { get; set; }
        public string valide { get; set; }
        public string numlig { get; set; }
        public DateTime datenv { get; set; }

        //==> CIP V2
        public Int64? MontPen { get; set; }
        public string BenefNom { get; set; }
        public string BenefPrenom { get; set; }
        public int? MotifCode { get; set; }
        public string MotifDesc { get; set; }
    }
}
