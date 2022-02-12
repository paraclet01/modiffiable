using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    [DataContract]
    public class SaveDeclarationFileCommand : IRequest<bool>
    {

        //[DataMember]
        //public Guid Id { get; set; }

        [DataMember]
        public string NomFichier { get; set; }

        [DataMember]
        public String DateDeclaration { get; set; }

        public SaveDeclarationFileCommand()
        {

        }

        public SaveDeclarationFileCommand(Guid Id, string NomFichier, String DateDeclaration)
        {
            //this.Id = Id;
            this.NomFichier = NomFichier;
            this.DateDeclaration = DateDeclaration;
        }
    }
}
