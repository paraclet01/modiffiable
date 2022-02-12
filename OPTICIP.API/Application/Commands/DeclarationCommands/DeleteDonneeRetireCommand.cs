using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    [DataContract]
    public class DeleteDonneeRetireCommand : IRequest<bool>
    {
        
        [DataMember]
        public Guid IdItem { get; set; }

        [DataMember]
        public string Agence { get; set; }

        [DataMember]
        public string Table_Item { get; set; }

        public DeleteDonneeRetireCommand()
        {

        }

        public DeleteDonneeRetireCommand(Guid _IdItem, String _Agence, string _Table_Item)
        {
            this.IdItem = _IdItem;
            this.Agence = _Agence;
            this.Table_Item = _Table_Item;
        }
    }
}
