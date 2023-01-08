using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class CIP3ViewModel
    {
        public string numseq { get; set; }
        public string compte { get; set; }
        public string codbnq { get; set; }
        public string codgch { get; set; }
        public string clerib { get; set; }
        public string iso { get; set; }
        public string juricat { get; set; }
        public string apegr { get; set; }
        public string adr { get; set; }
        public string ville { get; set; }
        public string explmaj { get; set; }
        public DateTime? datmaj { get; set; }
        public string type { get; set; }
        public string state { get; set; }
        public string valide { get; set; }
        public string rcsno { get; set; }
        public string forme { get; set; }
        public string sigle { get; set; }
        public string resp { get; set; }
        public string pays { get; set; }
        public string codape { get; set; }
        public Int32 numlig { get; set; }
        public string mand { get; set; }
        public DateTime? datenv { get; set; }
        //==> CIP V2
        public string Email { get; set; }
    }
}
