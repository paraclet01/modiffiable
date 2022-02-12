using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.UsersCommands
{
    [DataContract]
    public class ChangeStatutUserCommand : IRequest<bool>
    {

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public int Statut { get; set; }

        public ChangeStatutUserCommand()
        {

        }

        public ChangeStatutUserCommand(Guid Id, int Statut)
        {
            this.Id = Id;
            this.Statut = Statut;
        }
    }
}
