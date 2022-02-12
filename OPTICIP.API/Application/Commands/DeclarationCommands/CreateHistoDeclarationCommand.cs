using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    [DataContract]
    public class CreateHistoDeclarationCommand : IRequest<bool>
    {
        
       
        [DataMember]
        public string NomFichier { get; set; }

        [DataMember]
        public string Nombre_Compte_CIP { get; set; }

        [DataMember]
        public Guid CreatedBy { get; set; }

        [DataMember]
        public string Agence { get; set; }


        public CreateHistoDeclarationCommand()
        {

        }

        public CreateHistoDeclarationCommand( String _NomFichier, String _Nombre_Compte_CIP, Guid _CreatedBy, String _Agence)
        {
            this.NomFichier = _NomFichier;
            this.CreatedBy = _CreatedBy;
            this.Nombre_Compte_CIP = _Nombre_Compte_CIP;
            this.Agence = _Nombre_Compte_CIP;
        }
    }
}
