using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.Entities.DataEntities
{
    public partial class Cip3Temp : CIPEntity
    {
        public string Numseq { get; set; }
        public string Compte { get; set; }
        public string Codbnq { get; set; }
        public string Codgch { get; set; }
        public string Clerib { get; set; }
        public string Iso { get; set; }
        public string Juricat { get; set; }
        public string Apegr { get; set; }
        public string Adr { get; set; }
        public string Ville { get; set; }
        public string Explmaj { get; set; }
        public DateTime? Datmaj { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public string Valide { get; set; }
        public string Rcsno { get; set; }
        public string Forme { get; set; }
        public string Sigle { get; set; }
        public string Resp { get; set; }
        public string Pays { get; set; }
        public string Codape { get; set; }
        public decimal? Numlig { get; set; }
        public string Mand { get; set; }
        public DateTime? Datenv { get; set; }
        public string Email { get; set; }
        //public Guid XcipRowid { get; set; }
        public DateTime? DateImportation { get; set; }
        public Guid? ImportePar { get; set; }
        public DateTime? DateModification { get; set; }
        public Guid? ModifiePar { get; set; }
        public int? StatutXcip { get; set; }
    }
}
