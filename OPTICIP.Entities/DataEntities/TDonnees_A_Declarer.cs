using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.Entities.DataEntities
{
    public class TDonnees_A_Declarer : IAggregateRoot
    {
        public Guid Id { get; set; }
        public Int32 Ordre { get; set; }
        public string Texte { get; set; }
        public string Agence { get; set; }
        public Guid RecordID { get; set; }
        public string TableSource { get; set; }

        public TDonnees_A_Declarer()
        {
        }

        public TDonnees_A_Declarer(Guid P_Id, string P_Texte, Int32 P_Ordre, String P_Agence, Guid P_RecordID, string P_TableSource)
        {
            Id = P_Id;
            Texte = P_Texte;
            Ordre = P_Ordre;
            Agence = P_Agence;
            RecordID = P_RecordID;
            TableSource = P_TableSource;
        }
    }
}
