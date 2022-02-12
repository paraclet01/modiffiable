using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.UsersCommands
{
    [DataContract]
    public class UpdateUserCommand : IRequest<bool>
    {

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Nom { get; set; }

        [DataMember]
        public string Prenoms { get; set; }
        
        [DataMember]
        public String Profil { get; set; }

        public UpdateUserCommand()
        {

        }

        public UpdateUserCommand(Guid Id, String Nom, String Prenoms, String Profil)
        {
            this.Id = Id;
            this.Nom = Nom;
            this.Prenoms = Prenoms;
            this.Profil = Profil;
        }
    }
}
