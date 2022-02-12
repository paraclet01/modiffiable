using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.AgencesCommands
{
    [DataContract]
    public class UpdateAgenceCommand : IRequest<bool>
    {

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Libelle { get; set; }

        [DataMember]
        public string Description { get; set; }

        public UpdateAgenceCommand()
        {

        }

        public UpdateAgenceCommand(Guid Id, String Code, String Libelle, String Description)
        {
            this.Id = Id;
            this.Code = Code;
            this.Libelle = Libelle;
            this.Description = Description;
        }
    }
}
