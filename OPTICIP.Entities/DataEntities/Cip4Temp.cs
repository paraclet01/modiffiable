using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.Entities.DataEntities
{
    public partial class Cip4Temp : CIPEntity
    {
        public string Idcarte { get; set; }
        public string Compte { get; set; }
        public string Codbnq { get; set; }
        public string Codgch { get; set; }
        public string Clerib { get; set; }
        public DateTime? Datetimevali { get; set; }
        public DateTime? Datetimeoppo { get; set; }
        public string Explmaj { get; set; }
        public DateTime? Datmaj { get; set; }
        public string Ctype { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public string Valide { get; set; }
        public decimal? Numlig { get; set; }
        public string NumseqIdp { get; set; }
        public DateTime? Datenv { get; set; }
        public string Porteur { get; set; }
        //public Guid XcipRowid { get; set; }
        public DateTime? DateImportation { get; set; }
        public Guid? ImportePar { get; set; }
        public DateTime? DateModification { get; set; }
        public Guid? ModifiePar { get; set; }
        public int? StatutXcip { get; set; }
    }
}
