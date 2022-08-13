using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.Entities.DataEntities
{
    public class CIPEntity: IAggregateRoot
    {
        public Guid XcipRowid { get; set; }

        public CIPEntity()
        {
            if (XcipRowid == null || XcipRowid == Guid.Empty)
                XcipRowid = Guid.NewGuid();
        }
    }
}
