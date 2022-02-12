using MediatR;
using System;
using System.Runtime.Serialization;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    [DataContract]
    public class UpdatePersMoraleCommand : IRequest<bool>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Code_Pays { get; set; }

        [DataMember]
        public String Cat_Personne { get; set; }

        [DataMember]
        public String Iden_Personne { get; set; }

        [DataMember]
        public String Raison_Soc { get; set; }

        [DataMember]
        public String Sigle { get; set; }

        [DataMember]
        public String Code_Activite { get; set; }

        [DataMember]
        public String Responsable { get; set; }

        [DataMember]
        public String Mandataire { get; set; }

        [DataMember]
        public String Adresse { get; set; }

        [DataMember]
        public String Ville { get; set; }

        [DataMember]
        public String Iden_BCEAO { get; set; }

        //==> CIP V2
        [DataMember]
        public string Email { get; set; }

        public UpdatePersMoraleCommand()
        {

        }

        public UpdatePersMoraleCommand(Guid P_Id, String P_Code_Pays, String P_Cat_Personne
         , String P_Iden_Personne, String P_Raison_Soc, String P_Sigle, String P_Code_Activite, String P_Responsable, String P_Mandataire, String P_Adresse
         , String P_Ville, String P_Iden_BCEAO,
        //==> CIP V2
        string P_Email
        )
        {
            this.Id = P_Id;
            this.Code_Pays = P_Code_Pays;
            this.Cat_Personne = P_Cat_Personne;
            this.Iden_Personne = P_Iden_Personne;
            this.Raison_Soc = P_Raison_Soc;
            this.Sigle = P_Sigle;
            this.Code_Activite = P_Code_Activite;
            this.Responsable = P_Responsable;
            this.Mandataire = P_Mandataire;
            this.Adresse = P_Adresse;
            this.Ville = P_Ville;
            this.Iden_BCEAO = P_Iden_BCEAO;

            //==> CIP V2
            this.Email = P_Email;
        }
    }
}
