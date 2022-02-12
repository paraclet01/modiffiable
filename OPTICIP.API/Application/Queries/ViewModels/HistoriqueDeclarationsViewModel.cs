using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class HistoriqueDeclarationsViewModel
    {
        public Guid Id { get; set; }
        public Int32 Ordre { get; set; }
        public String Text { get; set; }
        public Guid Id_Declaration { get; set; }
        public String Agence { get; set; }
        public Guid RecordID { get; set; }
        public String TableSource { get; set; }

    }
}
