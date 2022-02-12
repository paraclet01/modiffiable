using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class ParametresViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Libelle { get; set; }
        public string Description { get; set; }
        public int CodeModifiable { get; set; }
    }
}
