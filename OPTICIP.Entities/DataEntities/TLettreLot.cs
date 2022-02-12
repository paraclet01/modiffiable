using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.Entities.DataEntities
{
    public class TLettreLot : IAggregateRoot
    {
        public Guid Id { get; set; }
        public DateTime Date_Generation { get; set; }
        public string Type_Lettre { get; set; }
        public string Chemin { get; set; }
        public string Nom_Lettre { get; set; }

        public TLettreLot()
        {

        }

        public TLettreLot(Guid P_Id, DateTime P_Date_Generation, string P_Chemin, string P_Type_Lettre, string P_Nom_Lettre)
        {
            Id = P_Id;
            Date_Generation = P_Date_Generation;
            Type_Lettre = P_Type_Lettre;
            Chemin = P_Chemin;
            Nom_Lettre = P_Nom_Lettre;
        }
    }
}
