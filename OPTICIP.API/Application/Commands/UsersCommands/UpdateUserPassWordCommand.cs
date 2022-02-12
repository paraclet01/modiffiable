using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.UsersCommands
{
    [DataContract]
    public class UpdateUserPassWordCommand : IRequest<bool>
    {

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string MotPasse { get; set; }

        public UpdateUserPassWordCommand()
        {

        }

        public UpdateUserPassWordCommand(Guid Id, String MotPasse)
        {
            this.Id = Id;
            this.MotPasse = MotPasse;
        }
    }
}
