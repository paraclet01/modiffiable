using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class DeclarationsViewModel
    {
        public string Numero { get; set; }
        public string Data { get; set; }
        public string NomFichier { get;  set; }
        public string Type_Data { get; set; }
        public string Agence { get; set; }
        public Guid Id { get; set; }
    }
}
