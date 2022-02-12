using OPTICIP.SeedWork;
using System;

namespace OPTICIP.Entities.Models
{
    public class TIncidentCheque : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Code { get;  set; }
        public string Num_Enr { get;  set; }
        public string Agence { get; set; }
        public string RIB { get;  set; }
        public string Cle_RIB { get;  set; }
        public string Type_Incident { get;  set; }
        public DateTime? Date_Emission { get;  set; }
        public DateTime? Date_Refus_Paiement { get;  set; }
        public DateTime? Date_Presentation { get;  set; }
        public DateTime? Point_Depart { get;  set; }
        public Int64 Montant_Nominal { get;  set; }
        public Int64 Montant_Insuffisance { get;  set; }
        public string Numero_Cheque { get;  set; }
        public DateTime? Date_Regularisation { get;  set; }
        public string Identifiant { get;  set; }
        public string Num_Ligne_Erreur { get;  set; }
        public string Etat { get;  set; }
        public DateTime? Date_Detection { get; set; }
        public DateTime? Date_Declaration { get; set; }

        //==> CIP V2
        public Int64? MontPen { get; set; }
        public string BenefNom { get; set; }
        public string BenefPrenom { get; set; }
        public int? MotifCode { get; set; }
        public string MotifDesc { get; set; }

        public TIncidentCheque()
        {

        }
        public TIncidentCheque(Guid P_Id, string P_Code, string P_Num_Enr, string P_Agence, string P_RIB, string P_Cle_RIB, string P_Type_Incident,
            DateTime? P_Date_Emission, DateTime? P_Date_Refus_Paiement, DateTime? P_Date_Presentation, DateTime?  P_Point_Depart, Int64 P_Montant_Nominal,
            Int64 P_Montant_Insuffisance, string P_Numero_Cheque, DateTime? P_Date_Regularisation, string P_Identifiant, string P_Num_Ligne_Erreur,
            string P_Etat, DateTime? P_Date_Detection, DateTime? P_Date_Declaration,
        //==> CIP V2
        Int64?  P_MontPen        ,
        string P_BenefNom       ,
        string P_BenefPrenom    ,
        int?    P_MotifCode      ,
        string P_MotifDesc      
            )  
        {
            Id = P_Id;
            Code = P_Code;
            Num_Enr = P_Num_Enr;
            Agence = P_Agence;
            RIB = P_RIB;
            Cle_RIB = P_Cle_RIB;
            Type_Incident = P_Type_Incident;
            Date_Emission = P_Date_Emission;
            Date_Refus_Paiement = P_Date_Refus_Paiement;
            Date_Presentation = P_Date_Presentation;
            Point_Depart = P_Point_Depart;
            Montant_Nominal = P_Montant_Nominal;
            Numero_Cheque = P_Numero_Cheque;
            Date_Regularisation = P_Date_Regularisation;
            Identifiant = P_Identifiant;
            Num_Ligne_Erreur = P_Num_Ligne_Erreur;
            Montant_Insuffisance = P_Montant_Insuffisance;
            Etat = P_Etat;
            Date_Detection = P_Date_Detection;
            Date_Declaration = P_Date_Declaration;

            //==> CIP V2
            MontPen = P_MontPen;
            BenefNom = P_BenefNom??"";
            BenefPrenom = P_BenefPrenom??"";
            MotifCode = P_MotifCode;
            MotifDesc = P_MotifDesc??"";
        }

        public void Update(String P_Etat, DateTime P_Date_Declaration)
        {
            this.Etat = P_Etat ?? throw new Exception(nameof(P_Etat));
            this.Date_Declaration = P_Date_Declaration;
        }
    }
}
