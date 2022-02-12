using OPTICIP.SeedWork;
using System;

namespace OPTICIP.Entities.DataEntities
{
    public class TErreurRetour : IAggregateRoot
    {
        public Guid Id { get; set; }
        public Guid EnregistrementID { get; set; }
        public string EnregistrementTable { get; set; }
        public string Numero_Ligne_Erreur { get; set; }
        public string Champ_Erreur { get; set; }
        public string Code_Erreur { get; set; }

        public TErreurRetour()
        {

        }

        public TErreurRetour(Guid P_Id, Guid P_EnregistrementID, string P_EnregistrementTable, string P_Numero_Ligne_Erreur, string P_Champ_Erreur, string P_Code_Erreur)
        {
            Id = P_Id;
            EnregistrementID = P_EnregistrementID;
            EnregistrementTable = P_EnregistrementTable;
            Numero_Ligne_Erreur = P_Numero_Ligne_Erreur;
            Champ_Erreur = P_Champ_Erreur;
            Code_Erreur = P_Code_Erreur;
        }
    }
}
