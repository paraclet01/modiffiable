using MediatR;
using System;
using System.Runtime.Serialization;

namespace OPTICIP.API.Application.Commands.DeclarationCommands
{
    [DataContract]
    public class UpdateCompteCommand : IRequest<bool>
    {

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public DateTime Date_Ouverture { get; set; }

        [DataMember]
        public DateTime? Date_Fermerture { get; set; }


        public UpdateCompteCommand()
        {

        }

        public UpdateCompteCommand(Guid P_Id, DateTime P_Date_Ouverture, DateTime? P_Date_Fermerture)
        {
            this.Id = P_Id;
            this.Date_Ouverture = P_Date_Ouverture;
            this.Date_Fermerture = P_Date_Fermerture;
        }
    }
}
