using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.UsersCommands
{
    [DataContract]
    public class CreateUserCommand : IRequest<bool>
    {
        
        [DataMember]
        public string Nom { get; set; }

        [DataMember]
        public string Prenoms { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string MotPasse { get; set; }

        [DataMember]
        public String Profil { get; set; }


        public CreateUserCommand()
        {

        }

        public CreateUserCommand(String Nom, String Prenoms, String Login, String MotPasse, String Profil)
        {
            this.Nom = Nom;
            this.Prenoms = Prenoms;
            this.Login = Login;
            this.MotPasse = "";
            this.Profil = Profil;
        }
    }
}
