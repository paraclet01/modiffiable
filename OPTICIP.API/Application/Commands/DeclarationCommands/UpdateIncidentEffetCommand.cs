using MediatR;
using System;
using System.Runtime.Serialization;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    [DataContract]
    public class UpdateIncidentEffetCommand : IRequest<bool>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public DateTime Echeance { get; set; }

        [DataMember]
        public Int64 Montant { get; set; }

        [DataMember]
        public DateTime? Date_Refus_Paiement { get; set; }

        [DataMember]
        public String Type_Effet { get; set; }

        [DataMember]
        public String Motif_Non_Paiement { get; set; }

        [DataMember]
        public String Avis_Domiciliation { get; set; }

        [DataMember]
        public String Ordre_Paiement_Perm { get; set; }

        //==> CIP V2
        [DataMember]
        public string MotifDesc { get; set; }

        public UpdateIncidentEffetCommand()
        {

        }

        public UpdateIncidentEffetCommand(Guid P_Id, DateTime P_Echeance
           , Int64 P_Montant, DateTime? P_Date_Refus_Paiement, String P_Type_Effet, String P_Motif_Non_Paiement, String P_Avis_Domiciliation
            , String P_Ordre_Paiement_Perm,
        //==> CIP V2
        string P_MotifDesc
        )
        {
            this.Id = P_Id;
            this.Echeance = P_Echeance;
            this.Montant = P_Montant;
            this.Date_Refus_Paiement = P_Date_Refus_Paiement;
            this.Type_Effet = P_Type_Effet;
            this.Motif_Non_Paiement = P_Motif_Non_Paiement;
            this.Avis_Domiciliation = P_Avis_Domiciliation;
            this.Ordre_Paiement_Perm = P_Ordre_Paiement_Perm;
            //==> CIP V2
            this.MotifDesc = P_MotifDesc;
        }
    }
}
