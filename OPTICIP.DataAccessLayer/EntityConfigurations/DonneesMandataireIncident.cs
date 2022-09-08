using System;
using System.Collections.Generic;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public partial class DonneesMandataireIncident
    {
        public Guid Id { get; set; }
        public string Nompre { get; set; }
        public string Civilite { get; set; }
        public string NomClient { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adrpostal { get; set; }
        public string Adrgeo { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        public DateTime? Datinc { get; set; }
        public string Numcheq { get; set; }
        public string Montchq { get; set; }
        public string Datemi { get; set; }
        public string Compte { get; set; }
        public string Benef { get; set; }
        public string Datpre { get; set; }
        public string Motif { get; set; }
        public string Idp { get; set; }
        public string Nooper { get; set; }
        public string TypeIncident { get; set; }
        public DateTime? DateInsertion { get; set; }
    }
}
