using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class ChequeIrregulierViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; private set; }
        public string Num_Enr { get; private set; }
        public string RIB { get; private set; }
        public string Cle_RIB { get; private set; }
        public string Type_Irregularite { get; private set; }
        public string Debut_Lot { get; private set; }
        public string Fin_Lot { get; private set; }
        public DateTime Date_Opposition { get; private set; }
        public DateTime Num_Ligne_Erreur { get; private set; }
        public string Etat { get; private set; }
    }
}
