using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class SyntheseViewModel
    {
        public String Type { get; set; }
        public int Declares { get; set; }
        public int DeclaresAcceptes { get; set; }
        public int DeclaresRejetes { get; set; }
        public int Modifies { get; set; }
        public int ModifiesAcceptes { get; set; }
        public int ModifiesRejetes { get; set; }
    }
}
