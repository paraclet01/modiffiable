using OPTICIP.SeedWork;
using System;

namespace OPTICIP.Entities.Models
{
    public class TPersMorale : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Num_Enr { get; set; }
        public string Agence { get; set; }
        public string RIB { get; set; }
        public string Cle_RIB { get; set; }
        public string Code_Pays { get; set; }
        public string Cat_Personne { get; set; }
        public string Iden_Personne { get; set; }
        public string Raison_Soc { get; set; }
        public string Sigle { get; set; }
        public string Code_Activite { get; set; }
        public string Responsable { get; set; }
        public string Mandataire { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string Iden_BCEAO { get; set; }
        public string Num_Ligne_Erreur { get; set; }
        public string Etat { get; set; }
        public DateTime? Date_Detection { get; set; }
        public DateTime? Date_Declaration { get; set; }

        //==> CIP V2
        public string Email { get; set; }

        public TPersMorale()
        {

        }

        public TPersMorale(Guid P_Id, string P_Code, string P_Num_Enr, string P_Agence, string P_RIB, string P_Cle_RIB, string P_Code_Pays, string P_Cat_Personne, string P_Iden_Personne,
            string P_Raison_Soc, string P_Sigle, string P_Code_Activite,  string P_Responsable, string P_Mandataire,
            string P_Adresse, string P_Ville, string P_Iden_BCEAO, string P_Num_Ligne_Erreur, string P_Etat, DateTime? P_Date_Detection, DateTime? P_Date_Declaration,
        //==> CIP V2
        string P_Email
)
        {
            Id = P_Id;
            Code = P_Code;
            Num_Enr = P_Num_Enr;
            Agence = P_Agence;
            RIB = P_RIB;
            Cle_RIB = P_Cle_RIB;
            Code_Pays = P_Code_Pays;
            Cat_Personne = P_Cat_Personne;
            Iden_Personne = P_Iden_Personne;
            Raison_Soc = P_Raison_Soc;
            Sigle = P_Sigle;
            Code_Activite = P_Code_Activite;
            Responsable = P_Responsable;
            Mandataire = P_Mandataire;
            Adresse = P_Adresse;
            Ville = P_Ville;
            Iden_BCEAO = P_Iden_BCEAO;
            Num_Ligne_Erreur = P_Num_Ligne_Erreur;
            Etat = P_Etat;
            Date_Detection = P_Date_Detection;
            Date_Declaration = P_Date_Declaration;

            //==> CIP V2
            Email = P_Email??"";
        }

        public void Update(String P_Etat, DateTime P_Date_Declaration)
        {
            this.Etat = P_Etat ?? throw new Exception(nameof(P_Etat));
            this.Date_Declaration = P_Date_Declaration;
        }

        public void DeleteCompte()
        {

        }
    }
}
