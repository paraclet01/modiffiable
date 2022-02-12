using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class LettreLotViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date_Generation { get; set; }
        public string Chemin { get; set; }
        public string Type_Lettre { get; set; }
        public string Nom_Lettre { get; set; }
    }
}
