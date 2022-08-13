using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.Entities.DataEntities
{
    public partial class SuiviImportation : IAggregateRoot
    {
        public Guid XcipRowid { get; set; }
        public int TypeImportation { get; set; }
        public int NombreLignesIns { get; set; }
        public int NombreLignesMaj { get; set; }
        public DateTime? DateImportation { get; set; }
        public Guid? ImportePar { get; set; }
        public DateTime? DateModification { get; set; }
        public Guid? ModifiePar { get; set; }

        public SuiviImportation()
        {
            if (XcipRowid == null || XcipRowid == Guid.Empty)
                XcipRowid = Guid.NewGuid();
        }
    }
}
