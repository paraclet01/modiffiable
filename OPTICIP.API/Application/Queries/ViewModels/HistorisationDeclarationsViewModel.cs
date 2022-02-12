using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class HistorisationDeclarationsViewModel
    {
        public Guid Id { get; set; }
        public string NomFichier { get; set; }
        public string Nombre_Compte_CIP { get; set; }
        public string DateDeclaration { get; set; }
        public string DeclarePar { get; set; }
        public int Nombre_ElementDeclares { get; set; }
        public string Agence { get; set; }
        public string Libelleagence { get; set; }
        public string Traitement_Retour { get; set; }
    }
}
