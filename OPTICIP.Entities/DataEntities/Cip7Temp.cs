using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.Entities.DataEntities
{
    public partial class Cip7Temp : CIPEntity
    {
        public string Numseq { get; set; }
        public string Compte { get; set; }
        public string Codbnq { get; set; }
        public string Codgch { get; set; }
        public string Clerib { get; set; }
        public DateTime? Datech { get; set; }
        public int? Mnt { get; set; }
        public DateTime? Datref { get; set; }
        public string Typeff { get; set; }
        public string Motif { get; set; }
        public string Avidom { get; set; }
        public string Ordpai { get; set; }
        public string Explmaj { get; set; }
        public DateTime? Datmaj { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public string Valide { get; set; }
        public decimal? Numlig { get; set; }
        public DateTime? Datenv { get; set; }
        public string Motifdesc { get; set; }
        //public Guid XcipRowid { get; set; }
        public DateTime? DateImportation { get; set; }
        public Guid? ImportePar { get; set; }
        public DateTime? DateModification { get; set; }
        public Guid? ModifiePar { get; set; }
        public int? StatutXcip { get; set; }
    }
}
