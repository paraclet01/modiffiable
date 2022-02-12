using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.PreparationCommands
{
    [DataContract]
    public class UpdateIncidentChqCommand : IRequest<bool>
    {

        [DataMember]
        public string Compte { get; set; }

        [DataMember]
        public string Cheque { get; set; }

        [DataMember]
        public DateTime DateRegulatisation { get; set; }

        [DataMember]
        public DateTime DatJustification { get; set; }

        [DataMember]
        public String MotifRegularisation { get; set; }

        [DataMember]
        public String NumeroPenalite { get; set; }

        [DataMember]
        public decimal MontantPenalite { get; set; }


        public UpdateIncidentChqCommand()
        {

        }

        public UpdateIncidentChqCommand(string _Compte, string _Cheque, DateTime _DateRegulatisation, DateTime _DatJustification, String _MotifRegularisation, String _NumeroPenalite, decimal _MontantPenalite)
        {
            this.Compte = _Compte;
            this.Cheque = _Cheque;
            this.DateRegulatisation = _DateRegulatisation;
            this.DatJustification = _DatJustification;
            this.MotifRegularisation = _MotifRegularisation;
            this.NumeroPenalite = _NumeroPenalite;
            this.MontantPenalite = _MontantPenalite;
        }
    }
}
