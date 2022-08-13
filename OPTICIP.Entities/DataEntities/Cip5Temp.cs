using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.Entities.DataEntities
{
    public partial class Cip5Temp : CIPEntity
    {
        public string Numseq { get; set; }
        public string Compte { get; set; }
        public string Codbnq { get; set; }
        public string Codgch { get; set; }
        public string Clerib { get; set; }
        public string Typinc { get; set; }
        public DateTime? Datchq { get; set; }
        public DateTime? Darreg { get; set; }
        public DateTime? Datpre { get; set; }
        public DateTime? Datinc { get; set; }
        public int? Mntfrf { get; set; }
        public int? Mntrej { get; set; }
        public string Chqref { get; set; }
        public DateTime? Datreg { get; set; }
        public string Numpen { get; set; }
        public string Explmaj { get; set; }
        public DateTime? Datmaj { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public string Valide { get; set; }
        public decimal? Numlig { get; set; }
        public DateTime? Datenv { get; set; }
        public int? Montpen { get; set; }
        public string Benefnom { get; set; }
        public string Benefprenom { get; set; }
        public decimal? Motifcode { get; set; }
        public string Motifdesc { get; set; }
        //public Guid XcipRowid { get; set; }
        public DateTime? DateImportation { get; set; }
        public Guid? ImportePar { get; set; }
        public DateTime? DateModification { get; set; }
        public Guid? ModifiePar { get; set; }
        public int? StatutXcip { get; set; }
    }
}
