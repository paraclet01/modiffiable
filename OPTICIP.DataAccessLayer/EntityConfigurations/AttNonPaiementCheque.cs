using System;
using System.Collections.Generic;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public partial class AttNonPaiementCheque
    {
        public Guid Id { get; set; }
        public string Nompre { get; set; }
        public string Agencelib { get; set; }
        public string Numcheq { get; set; }
        public string Montchq { get; set; }
        public string Compte { get; set; }
        public DateTime? DateRegularisation { get; set; }
        public string PenaliteLiberatoire { get; set; }
        public string Banque { get; set; }
        public string Nooper { get; set; }
        public DateTime? DateInsertion { get; set; }
    }
}
