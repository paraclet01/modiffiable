using System;
using System.Collections.Generic;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public partial class CertificatNonPaiement
    {
        public Guid Id { get; set; }
        public string Nooper { get; set; }
        public string Chqref { get; set; }
        public string Client { get; set; }
        public string Compte { get; set; }
        public string NomClient { get; set; }
        public string Agencelib { get; set; }
        public DateTime? Datinc { get; set; }
        public string Mntrej { get; set; }
        public string MotifLibelle { get; set; }
        public DateTime? Datpre { get; set; }
        public DateTime? Datdep { get; set; }
        public string BankDest { get; set; }
        public DateTime? DateInsertion { get; set; }
    }
}
