using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class IncidChqEbViewModel
    {
        public string nooper { get; set; }
        public DateTime datoper { get; set; }
        public DateTime datrej { get; set; }
        public string compte { get; set; }
        public string nom { get; set; }
        public string chqref { get; set; }
        public Int64 mntrej { get; set; }
        public DateTime datemi { get; set; }
        public string benef { get; set; }
        public string expl { get; set; }
        public string explmaj { get; set; }
        public DateTime datmaj { get; set; }
    }

}
