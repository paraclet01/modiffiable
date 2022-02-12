using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.PreparationCommands
{
    [DataContract]
    public class UpdateIncidentChqEbCommand : IRequest<bool>
    {

        [DataMember]
        public string Compte { get; set; }

        [DataMember]
        public string Cheque { get; set; }
       
        [DataMember]
        public DateTime Datoper { get; set; }

        [DataMember]
        public string Beneficiaire { get; set; }
        [DataMember]
        public DateTime Dateemi { get; set; }

        public UpdateIncidentChqEbCommand()
        {

        }

        public UpdateIncidentChqEbCommand(string _Compte,  string _Cheque, DateTime _Datoper, String _Beneficiaire, DateTime _Dateemi)
        {
            this.Dateemi = _Dateemi;
            this.Beneficiaire = _Beneficiaire;
            this.Compte = _Compte;
            this.Cheque = _Cheque;
            this.Datoper = _Datoper;
        }
    }
}
