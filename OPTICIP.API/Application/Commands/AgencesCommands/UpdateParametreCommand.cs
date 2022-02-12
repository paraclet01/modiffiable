using MediatR;
using System;
using System.Runtime.Serialization;

namespace OPTICIP.API.Application.Commands.AgencesCommands
{
    [DataContract]
    public class UpdateParametreCommand : IRequest<bool>
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Libelle { get; set; }

        [DataMember]
        public string Description { get; set; }

        public UpdateParametreCommand()
        {

        }

        public UpdateParametreCommand(int Id, String Code, String Libelle, String Description)
        {
            this.Id = Id;
            this.Code = Code;
            this.Libelle = Libelle;
            this.Description = Description;
        }
    }
}
