using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    [DataContract]
    public class UpdateChequeIrregulierCommand : IRequest<bool>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public String Type_Irregularite { get; set; }

        [DataMember]
        public String Debut_Lot { get; set; }

        [DataMember]
        public String Fin_Lot { get; set; }

        [DataMember]
        public DateTime? Date_Opposition { get; set; }

        public UpdateChequeIrregulierCommand()
        {

        }

        public UpdateChequeIrregulierCommand(Guid P_Id,
            String P_Type_Irregularite, String P_Debut_Lot, String P_Fin_Lot
           , DateTime? P_Date_Opposition)
        {
            this.Id = P_Id;
            this.Type_Irregularite = P_Type_Irregularite;
            this.Debut_Lot = P_Debut_Lot;
            this.Fin_Lot = P_Fin_Lot;
            this.Date_Opposition = P_Date_Opposition;
        }
    }
}
