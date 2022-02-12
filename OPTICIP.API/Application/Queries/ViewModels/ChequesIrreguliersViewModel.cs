using System;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class ChequesIrreguliersViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Num_Enr { get; set; }
        public string Agence { get; set; }
        public string RIB { get; set; }
        public string Cle_RIB { get; set; }
        public string Type_Irregularite { get; set; }
        public string Debut_Lot { get; set; }
        public string Fin_Lot { get; set; }
        public DateTime Date_Opposition { get; set; }
        public string Num_Ligne_Erreur { get; set; }
        public string Etat { get; set; }
        public Guid IdItem_Retire { get; set; }
    }
}
