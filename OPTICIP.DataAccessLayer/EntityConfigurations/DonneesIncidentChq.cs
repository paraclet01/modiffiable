using System;
using System.Collections.Generic;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public partial class DonneesIncidentChq
    {
        public Guid id { get; set; }
        public int? typeIncident { get; set; }
        public string nompre { get; set; }
        public string agencelib { get; set; }
        public string adrpostal { get; set; }
        public string adrgeo { get; set; }
        public string ville { get; set; }
        public string pays { get; set; }
        public DateTime? datinc { get; set; }
        public string numcheq { get; set; }
        public string montchq { get; set; }
        public string datemi { get; set; }
        public string compte { get; set; }
        public string benef { get; set; }
        public string datpre { get; set; }
        public string motif { get; set; }
        public string nooper { get; set; }
        public string solde_incident { get; set; }
        public string libinf { get; set; }
        public DateTime? dateInsertion { get; set; }
    }
}
