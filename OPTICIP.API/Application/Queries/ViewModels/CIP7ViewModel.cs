using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class CIP7ViewModel
    {
        public string numseq { get; set; }
        public string compte { get; set; }
        public string codbnq { get; set; }
        public string codgch { get; set; }
        public string clerib { get; set; }
        public DateTime datech { get; set; }
        public Int64 mnt { get; set; }
        public DateTime datref { get; set; }
        public string typeff { get; set; }
        public string motif { get; set; }
        public string avidom { get; set; }
        public string ordpai { get; set; }
        public string explmaj { get; set; }
        public DateTime datmaj { get; set; }
        public string type { get; set; }
        public string state { get; set; }
        public string valide { get; set; }
        public string numlig { get; set; }
        public DateTime datenv { get; set; }

        //==> CIP V2
        public string MotifDesc { get; set; }
    }
}
