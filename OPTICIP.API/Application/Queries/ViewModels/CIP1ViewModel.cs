using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class CIP1ViewModel
    {
        /*  numseq  VARCHAR2(21),
            compte  VARCHAR2(11),
            codbnq  VARCHAR2(5),
            codgch  VARCHAR2(5),
            clerib  VARCHAR2(2),
            datouv  DATE,
            datfrm  DATE,
            explmaj VARCHAR2(3),
            datmaj  DATE,
            type    VARCHAR2(1),
            state   VARCHAR2(1),
            valide  VARCHAR2(1),
            numlig  NUMBER(5),
            datenv  DATE
         */

        public string numseq { get; set; }
        public string compte { get; set; }
        public string codbnq { get; set; }
        public string codgch { get; set; }
        public string clerib { get; set; }
        public DateTime datouv { get; set; }
        public DateTime datfrm { get; set; }
        public string explmaj { get; set; }
        public DateTime datmaj { get; set; }
        public string type { get; set; }
        public string state { get; set; }
        public string valide { get; set; }
        public Int32 numlig { get; set; }
        public DateTime datenv { get; set; }
    }
}
