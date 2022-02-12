using OPTICIP.SeedWork;
using System;

namespace OPTICIP.Entities.DataEntities
{
    public class THistorique_Declarations : IAggregateRoot
    {
        public Guid Id { get; set; }
        public Int32 Ordre { get; set; }
        public String Text { get; set; }
        public Guid Id_Declaration { get; set; }
        public String Agence { get; set; }
        public Guid RecordID { get; set; }
        public String TableSource { get; set; }

        public THistorique_Declarations()
        {

        }

        public THistorique_Declarations(Guid P_Id, Int32 P_Ordre, String P_Text, Guid P_Id_Declaration, String P_Agence, Guid P_RecordID, String P_TableSource)
        {
            Id = P_Id;
            Ordre = P_Ordre;
            Text = P_Text;
            Id_Declaration = P_Id_Declaration;
            Agence = P_Agence;
            RecordID = P_RecordID;
            TableSource = P_TableSource;
        }
    }
}
