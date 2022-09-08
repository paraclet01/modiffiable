using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DetectionCommands
{
    [DataContract]
    public class UpdateIncidentChqBenefCommand : IRequest<bool>
    {

        [DataMember]
        public Guid idCheque { get; set; }

        [DataMember]
        public string benefNom { get; set; }

        [DataMember]
        public string benefPrenom { get; set; }
        public UpdateIncidentChqBenefCommand()
        {

        }

        public UpdateIncidentChqBenefCommand(Guid _idCheque, String _benefNom, String _benefPrenom)
        {
            this.idCheque = _idCheque;
            this.benefNom = _benefNom;
            this.benefPrenom = benefPrenom;
        }
    }
}
