using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OPTICIP.Entities.DataEntities
{
    public class TLog : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string Details_Action { get; set; }
        public Guid ID_Utilisateur { get; set; }
        public DateTime Date { get; set; }

        public TLog()
        {

        }

        public TLog(Guid P_Id, string P_Action, string P_Details_Action, Guid P_ID_Utilisateur, DateTime P_Date)
        {
            Id = P_Id;
            Action = P_Action;
            Details_Action = P_Details_Action;
            ID_Utilisateur = P_ID_Utilisateur;
            Date = P_Date;
        }
    }
}
