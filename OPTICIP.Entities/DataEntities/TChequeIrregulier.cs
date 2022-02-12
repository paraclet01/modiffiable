using OPTICIP.SeedWork;
using System;

namespace OPTICIP.Entities.Models
{
    public  class TChequeIrregulier : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Code { get;  set; }
        public string Num_Enr { get;  set; }
        public string Agence { get; set; }
        public string RIB { get;  set; }
        public string Cle_RIB { get;  set; }
        public string Type_Irregularite { get;  set; }
        public string Debut_Lot { get;  set; }
        public string Fin_Lot { get;  set; }
        public DateTime? Date_Opposition { get;  set; }
        public string Num_Ligne_Erreur { get;  set; }
        public string Etat { get;  set; }
        public DateTime? Date_Detection { get; set; }
        public DateTime? Date_Declaration { get; set; }

        public TChequeIrregulier()
        {

        }

        public TChequeIrregulier(Guid P_Id, string P_Code, string P_Num_Enr, string P_Agence, string P_RIB, string P_Cle_RIB, string P_Type_Irregularite,
            string P_Debut_Lot, string P_Fin_Lot, DateTime? P_Date_Opposition, string P_Num_Ligne_Erreur,
            string P_Etat, DateTime? P_Date_Detection, DateTime? P_Date_Declaration)
        {
            Id = P_Id;
            Code = P_Code;
            Num_Enr = P_Num_Enr;
            Agence = P_Agence;
            RIB = P_RIB;
            Cle_RIB = P_Cle_RIB;
            Type_Irregularite = P_Type_Irregularite;
            Debut_Lot = P_Debut_Lot;
            Fin_Lot = P_Fin_Lot;
            Date_Opposition = P_Date_Opposition;
            Num_Ligne_Erreur = P_Num_Ligne_Erreur;
            Etat = P_Etat;
            Date_Detection = P_Date_Detection;
            Date_Declaration = P_Date_Declaration;
        }

        public void Update(String P_Etat, DateTime P_Date_Declaration)
        {
            this.Etat = P_Etat ?? throw new Exception(nameof(P_Etat));
            this.Date_Declaration = P_Date_Declaration;
        }
    }
}
