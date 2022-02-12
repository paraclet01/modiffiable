using MediatR;
using System;
using System.Runtime.Serialization;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    [DataContract]
    public class UpdateIncidentChequeCommand : IRequest<bool>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Type_Incident { get; set; }

        [DataMember]
        public DateTime? Date_Emission { get; set; }

        [DataMember]
        public DateTime? Date_Refus_Paiement { get; set; }

        [DataMember]
        public DateTime? Date_Presentation { get; set; }

        [DataMember]
        public DateTime? Point_Depart { get; set; }

        [DataMember]
        public Int64 Montant_Nominal { get; set; }

        [DataMember]
        public Int64 Montant_Insuffisance { get; set; }

        [DataMember]
        public String Numero_Cheque { get; set; }

        [DataMember]
        public DateTime? Date_Regularisation { get; set; }

        [DataMember]
        public String Identifiant { get; set; }

        //==> CIP V2
        [DataMember]
        public Int64? MontPen { get; set; }
        [DataMember]
        public string BenefNom { get; set; }
        [DataMember]
        public string BenefPrenom { get; set; }
        [DataMember]
        public int? MotifCode { get; set; }
        [DataMember]
        public string MotifDesc { get; set; }

        public UpdateIncidentChequeCommand()
        {

        }

        public UpdateIncidentChequeCommand(Guid P_Id,String P_Type_Incident, DateTime? P_Date_Emission
        , DateTime? P_Date_Refus_Paiement, DateTime? P_Date_Presentation, DateTime? P_Point_Depart, Int64 P_Montant_Nominal, Int64 P_Montant_Insuffisance, String P_Numero_Cheque, DateTime? P_Date_Regularisation
        , String P_Identifiant,
        //==> CIP V2
        Int64? P_MontPen,
        string P_BenefNom,
        string P_BenefPrenom,
        int? P_MotifCode,
        string P_MotifDesc
        )
        {
            this.Id = P_Id;
            this.Type_Incident = P_Type_Incident;
            this.Date_Emission = P_Date_Emission;
            this.Date_Refus_Paiement = P_Date_Refus_Paiement;
            this.Date_Presentation = P_Date_Presentation;
            this.Point_Depart = P_Point_Depart;
            this.Montant_Nominal = P_Montant_Nominal;
            this.Montant_Insuffisance = P_Montant_Insuffisance;
            this.Numero_Cheque = P_Numero_Cheque;
            this.Date_Regularisation = P_Date_Regularisation;
            this.Identifiant = P_Identifiant;
            
            //==> CIP V2
            this.MontPen = P_MontPen;
            this.BenefNom = P_BenefNom;
            this.BenefPrenom = P_BenefPrenom;
            this.MotifCode = P_MotifCode;
            this.MotifDesc = P_MotifDesc;
        }
    }
}
