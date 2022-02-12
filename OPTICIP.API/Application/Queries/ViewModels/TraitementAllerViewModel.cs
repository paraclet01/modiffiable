using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class TraitementAllerViewModel
    {
        // Enregistrements déclarés
        public int ComptesDeclaresAcceptes { get; set; }
        public int ComptesDeclaresRejetes{ get; set; }

        public int PersPhysiquesDeclaresAcceptes { get; set; }
        public int PersPhysiquesDeclaresRejetes { get; set; }

        public int PersMoralesDeclaresAcceptes { get; set; }
        public int PersMoralesDeclaresRejetes { get; set; }

        public int IncidentsChequesDeclaresAcceptes { get; set; }
        public int IncidentsChequesDeclaresRejetes { get; set; }

        public int ChequesIrreguliersDeclaresAcceptes { get; set; }
        public int ChequesIrreguliersDeclaresRejetes { get; set; }

        public int IncidentsEffetsDeclaresAcceptes { get; set; }
        public int IncidentsEffetsDeclaresRejetes { get; set; }

        // Enregistrements modifiés
        public int ComptesModifiesAcceptes { get; set; }
        public int ComptesModifiesRejetes { get; set; }

        public int PersPhysiquesModifiesAcceptes { get; set; }
        public int PersPhysiquesModifiesRejetes { get; set; }

        public int PersMoralesModifiesAcceptes { get; set; }
        public int PersMoralesModifiesRejetes { get; set; }

        public int IncidentsChequesModifiesAcceptes { get; set; }
        public int IncidentsChequesModifiesRejetes { get; set; }

        public int ChequesIrreguliersModifiesAcceptes { get; set; }
        public int ChequesIrreguliersModifiesRejetes { get; set; }

        public int IncidentsEffetsModifiesAcceptes { get; set; }
        public int IncidentsEffetsModifiesRejetes { get; set; }
    }
}
