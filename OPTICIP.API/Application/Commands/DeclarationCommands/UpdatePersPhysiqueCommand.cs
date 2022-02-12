using MediatR;
using System;
using System.Runtime.Serialization;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    [DataContract]
    public class UpdatePersPhysiqueCommand : IRequest<bool>
    {
        [DataMember]
        public Guid Id { get; set; }  

        [DataMember]
        public String Nom_Naissance { get; set; }

        [DataMember]
        public String Prenoms { get; set; }

        [DataMember]
        public String Lieu_Naissance { get; set; }

        [DataMember]
        public DateTime? Date_Naissance { get; set; }

        [DataMember]
        public String Nom_Mari { get; set; }

        [DataMember]
        public String Nom_Naissance_Mere { get; set; }

        [DataMember]
        public String Nationalite { get; set; }

        [DataMember]
        public String Sexe { get; set; }

        [DataMember]
        public String Resident_UEMOA { get; set; }

        [DataMember]
        public String Num_Carte_Iden { get; set; }

        [DataMember]
        public String Responsable { get; set; }

        [DataMember]
        public String Mandataire { get; set; }

        [DataMember]
        public String Adresse { get; set; }

        [DataMember]
        public String Code_Pays { get; set; }

        [DataMember]
        public String Ville { get; set; }

        [DataMember]
        public String Num_Reg_Com { get; set; }

        //==> CIP V2
        [DataMember]
        public string EmailTitu { get; set; }

        [DataMember]
        public string NomContact { get; set; }

        [DataMember]
        public string PnomContact { get; set; }

        [DataMember]
        public string AdrContact { get; set; }

        [DataMember]
        public string EmailContact { get; set; }


        public UpdatePersPhysiqueCommand()
        {

        }

        public UpdatePersPhysiqueCommand(Guid P_Id, String P_Nom_Naissance, String P_Prenoms
            , String P_Lieu_Naissance, DateTime? P_Date_Naissance, String P_Nom_Mari, String P_Nom_Naissance_Mere, String P_Nationalite, String P_Sexe, String P_Resident_UEMOA
            , String P_Num_Carte_Iden, String P_Responsable, String P_Mandataire, String P_Adresse, String P_Code_Pays, String P_Ville, String P_Num_Reg_Com,
        //==> CIP V2
        string P_EmailTitu,
        string P_NomContact,
        string P_PnomContact,
        string P_AdrContact,
        string P_EmailContact)

        {
            this.Id = P_Id;
            this.Nom_Naissance = P_Nom_Naissance;
            this.Prenoms = P_Prenoms;
            this.Lieu_Naissance = P_Lieu_Naissance;
            this.Date_Naissance = P_Date_Naissance;
            this.Nom_Mari = P_Nom_Mari;
            this.Nom_Naissance_Mere = P_Nom_Naissance_Mere;
            this.Nationalite = P_Nationalite;
            this.Sexe = P_Sexe;
            this.Resident_UEMOA = P_Resident_UEMOA;
            this.Num_Carte_Iden = P_Num_Carte_Iden;
            this.Responsable = P_Responsable;
            this.Mandataire = P_Mandataire;
            this.Adresse = P_Adresse;
            this.Code_Pays = P_Code_Pays;
            this.Ville = P_Ville;
            this.Num_Reg_Com = P_Num_Reg_Com;

            //==> CIP V2
            this.EmailTitu = P_EmailTitu;
            this.NomContact = P_NomContact;
            this.PnomContact = P_PnomContact;
            this.AdrContact = P_AdrContact;
            this.EmailContact = P_EmailContact;
        }
    }
}
