using System;
using System.Collections.Generic;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public partial class AttNonPaiementEffet
    {
        public Guid Id { get; set; }
        public string Nooper { get; set; }
        public string Compte { get; set; }
        public string Nom { get; set; }
        public string Adrpostal { get; set; }
        public string Montant { get; set; }
        public DateTime? Datech { get; set; }
        public DateTime? Datrej { get; set; }
        public string Librej { get; set; }
        public string Beneficiaire { get; set; }
        public DateTime? DateInsertion { get; set; }
    }
}
