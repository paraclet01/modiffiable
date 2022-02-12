using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    [DataContract]
    public class DeleteDonneeErreurCommand : IRequest<bool>
    {
        [DataMember]
        public Guid EnregistrementID { get; set; }
        [DataMember]
        public string EnregistrementTable { get; set; }

        [DataMember]
        public string Table_Item { get; set; }

        public DeleteDonneeErreurCommand()
        {

        }

        public DeleteDonneeErreurCommand(Guid _enregistrementID, string _enregistrementTable, string _Table_Item)
        {
            this.EnregistrementID = _enregistrementID;
            this.EnregistrementTable = _enregistrementTable;
            this.Table_Item = Table_Item;
        }
    }
}
