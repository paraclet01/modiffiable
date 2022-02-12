using OPTICIP.SeedWork;
using System;

namespace OPTICIP.Entities.Models
{
    public class TPersPhysique : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Num_Enr { get; set; }
        public string Agence { get; set; }
        public string RIB { get; set; }
        public string Cle_RIB { get; set; }
        public string Nom_Naissance { get; set; }
        public string Prenoms { get; set; }
        public string Lieu_Naissance { get; set; }
        public DateTime? Date_Naissance { get; set; }
        public string Nom_Mari { get; set; }
        public string Nom_Naissance_Mere { get; set; }
        public string Nationalite { get; set; }
        public string Sexe { get; set; }
        public string Resident_UEMOA { get; set; }
        public string Num_Carte_Iden { get; set; }
        public string Responsable { get; set; }
        public string Mandataire { get; set; }
        public string Adresse { get; set; }
        public string Code_Pays { get; set; }
        public string Ville { get; set; }
        public string Num_Reg_Com { get; set; }
        public string Num_Ligne_Erreur { get; set; }
        public string Etat { get; set; }
        public DateTime? Date_Detection { get; set; }
        public DateTime? Date_Declaration { get; set; }

//==> CIP V2
        public string EmailTitu { get; set; }
        public string NomContact { get; set; }
        public string PnomContact { get; set; }
        public string AdrContact { get; set; }
        public string EmailContact { get; set; }

        public TPersPhysique(){}

        public TPersPhysique(Guid P_Id, string P_Code, string P_Num_Enr, string P_Agence, string P_RIB, string P_Cle_RIB, string P_Nom_Naissance, string P_Prenoms, string P_Lieu_Naissance, DateTime? P_Date_Naissance,
            string P_Nom_Mari, string P_Nom_Naissance_Mere, string P_Nationalite, string P_Sexe, string P_Resident_UEMOA, string P_Num_Carte_Iden, string P_Responsable, string P_Mandataire,
            string P_Adresse, string P_Code_Pays, string P_Ville, string P_Num_Reg_Com, string P_Num_Ligne_Erreur, string P_Etat, DateTime? P_Date_Detection, DateTime? P_Date_Declaration,
        //==> CIP V2
        string P_EmailTitu,
        string P_NomContact,
        string P_PnomContact,
        string P_AdrContact,
        string P_EmailContact)
        {
            Id = P_Id;
            Code = P_Code;
            Num_Enr = P_Num_Enr;
            Agence = P_Agence;
            RIB = P_RIB;
            Cle_RIB = P_Cle_RIB;
            Nom_Naissance = P_Nom_Naissance;
            Prenoms = P_Prenoms;
            Lieu_Naissance = P_Lieu_Naissance;
            Date_Naissance = P_Date_Naissance;
            Nom_Mari = P_Nom_Mari;
            Nom_Naissance_Mere = P_Nom_Naissance_Mere;
            Nationalite = P_Nationalite;
            Sexe = P_Sexe;
            Resident_UEMOA = P_Resident_UEMOA;
            Num_Carte_Iden = P_Num_Carte_Iden;
            Responsable = P_Responsable;
            Mandataire = P_Mandataire;
            Adresse = P_Adresse;
            Code_Pays = P_Code_Pays;
            Ville = P_Ville;
            Num_Reg_Com = P_Num_Reg_Com;
            Num_Ligne_Erreur = P_Num_Ligne_Erreur;
            Etat = P_Etat;
            Date_Detection = P_Date_Detection;
            Date_Declaration = P_Date_Declaration;

            //==> CIP V2
            EmailTitu = P_EmailTitu??"";
            NomContact = P_NomContact??"";
            PnomContact = P_PnomContact??"";
            AdrContact = P_AdrContact??"";
            EmailContact = P_EmailContact??"";
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
