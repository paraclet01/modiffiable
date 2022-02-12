using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.AgencesCommands
{
    [DataContract]
    public class DeleteAgenceCommand : IRequest<bool>
    {

        [DataMember]
        public Guid Id { get; set; }

        public DeleteAgenceCommand()
        {

        }

        public DeleteAgenceCommand(Guid Id)
        {
            this.Id = Id;
        }
    }
}
