using OPTICIP.SeedWork;
using System;
using System.Threading;

namespace OPTICIP.Entities.Models
{
    public class TIncidentEffet : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Code { get;  set; }
        public string Num_Enr { get;  set; }
        public string Agence { get; set; }
        public string RIB { get;  set; }
        public string Cle_RIB { get;  set; }
        public DateTime? Echeance { get;  set; }
        public Int64 Montant { get;  set; }
        public DateTime? Date_Refus_Paiement { get;  set; }
        public string Type_Effet { get;  set; }
        public string Motif_Non_Paiement { get;  set; }
        public string Avis_Domiciliation { get;  set; }
        public string Ordre_Paiement_Perm { get;  set; }
        public string Num_Ligne_Erreur { get;  set; }
        public string Etat { get;  set; }
        public DateTime? Date_Detection { get; set; }
        public DateTime? Date_Declaration { get; set; }

        //==> CIP V2
        public string MotifDesc { get; set; }

        public TIncidentEffet()
        {

        }

        public TIncidentEffet(Guid P_Id, string P_Code, string P_Num_Enr, string P_Agence, string P_RIB, string P_Cle_RIB, DateTime? P_Echeance,
            Int64 P_Montant, DateTime? P_Date_Refus_Paiement, string P_Type_Effet, string P_Motif_Non_Paiement, string P_Avis_Domiciliation,
            string P_Ordre_Paiement_Perm, string P_Num_Ligne_Erreur, string P_Etat, DateTime? P_Date_Detection, DateTime? P_Date_Declaration,
        //==> CIP V2
        string P_MotifDesc
        )
        {
            Id = P_Id;
            Code = P_Code;
            Num_Enr = P_Num_Enr;
            Agence = P_Agence;
            RIB = P_RIB;
            Cle_RIB = P_Cle_RIB;
            Echeance = P_Echeance;
            Montant = P_Montant;
            Date_Refus_Paiement = P_Date_Refus_Paiement;
            Type_Effet = P_Type_Effet;
            Motif_Non_Paiement = P_Motif_Non_Paiement;
            Avis_Domiciliation = P_Avis_Domiciliation;
            Ordre_Paiement_Perm = P_Ordre_Paiement_Perm;
            Num_Ligne_Erreur = P_Num_Ligne_Erreur;
            Etat = P_Etat;
            Date_Detection = P_Date_Detection;
            Date_Declaration = P_Date_Declaration;

            //==> CIP V2
            MotifDesc = P_MotifDesc??"";
        }

        public void Update(String P_Etat, DateTime P_Date_Declaration)
        {
            this.Etat = P_Etat ?? throw new Exception(nameof(P_Etat));
            this.Date_Declaration = P_Date_Declaration;
        }
    }
}
