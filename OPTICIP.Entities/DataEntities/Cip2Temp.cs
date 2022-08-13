using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.Entities.DataEntities
{
    public partial class Cip2Temp : CIPEntity
    {
        public string Numseq { get; set; }
        public string Compte { get; set; }
        public string Codbnq { get; set; }
        public string Codgch { get; set; }
        public string Clerib { get; set; }
        public string Nomnais { get; set; }
        public string Prenom { get; set; }
        public string Commnais { get; set; }
        public DateTime? Datnais { get; set; }
        public string Nommari { get; set; }
        public string Nommere { get; set; }
        public string Iso { get; set; }
        public string Sexe { get; set; }
        public string Numid { get; set; }
        public string Resp { get; set; }
        public string Adr { get; set; }
        public string Ville { get; set; }
        public string Payadr { get; set; }
        public string Explmaj { get; set; }
        public DateTime? Datmaj { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public string Valide { get; set; }
        public string Residumoa { get; set; }
        public decimal? Numlig { get; set; }
        public string Mand { get; set; }
        public DateTime? Datenv { get; set; }
        public string Emailtitu { get; set; }
        public string Nomcontact { get; set; }
        public string Pnomcontact { get; set; }
        public string Adrcontact { get; set; }
        public string Emailcontact { get; set; }
        //public Guid XcipRowid { get; set; }
        public DateTime? DateImportation { get; set; }
        public Guid? ImportePar { get; set; }
        public DateTime? DateModification { get; set; }
        public Guid? ModifiePar { get; set; }
        public int? StatutXcip { get; set; }
    }
}
