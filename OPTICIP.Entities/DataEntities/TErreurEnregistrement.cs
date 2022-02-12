using OPTICIP.SeedWork;
using System;

namespace OPTICIP.Entities.DataEntities
{
    public class TErreurEnregistrement : IAggregateRoot
    {
        public Guid Id { get; set; }
        public Guid EnregistrementID { get; set; }
        public string EnregistrementTable { get; set; }
        public string Texte { get; set; }

        public TErreurEnregistrement()
        {

        }

        public TErreurEnregistrement(Guid P_Id, Guid P_EnregistrementID, string P_EnregistrementTable, string P_Texte)
        {
            Id = P_Id;
            EnregistrementID = P_EnregistrementID;
            EnregistrementTable = P_EnregistrementTable;
            Texte = P_Texte;
        }
    }
}
