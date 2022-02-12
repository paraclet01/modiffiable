using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    [DataContract]
    public class CreateDonneeRetireCommand : IRequest<bool>
    {
        
        [DataMember]
        public Guid IdItem { get; set; }

        [DataMember]
        public string Table_Item { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public string Agence { get; set; }

        public CreateDonneeRetireCommand()
        {

        }

        public CreateDonneeRetireCommand(Guid _IdItem, String _Table_Item, String _CreatedBy, String _Agence)
        {
            this.IdItem = _IdItem;
            this.Table_Item = _Table_Item;
            this.CreatedBy = _CreatedBy;
            this.Agence = _Agence;
        }
    }
}
