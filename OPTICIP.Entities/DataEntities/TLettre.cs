using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OPTICIP.Entities.DataEntities
{
    public class TLettre : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Nooper { get; set; }
        public string Client { get; set; }
        public string Numero_Compte { get; set; }
        public string Numero_Cheque { get; set; }
        public string Montant_Incident { get; set; }
        public DateTime Date_Incident { get; set; }
        public DateTime Date_Generation { get; set; }
        public string Chemin { get; set; }
        public string Type_Lettre { get; set; }
        public string Nom_Lettre { get; set; }
        public string IDP { get; set; }

        public TLettre()
        {

        }

        public TLettre(Guid P_Id, string P_Nooper, string P_Client, string P_Numero_Compte, string P_Numero_Cheque, string P_Montant_Incident, DateTime P_Date_Incident, DateTime P_Date_Generation, string P_Chemin, string P_Type_Lettre, string P_Nom_Lettre, string P_IDP)
        {
            Id = P_Id;
            Nooper = P_Nooper;
            Client = P_Client;
            Numero_Compte = P_Numero_Compte;
            Numero_Cheque = P_Numero_Cheque;
            Montant_Incident = P_Montant_Incident;
            Date_Incident = P_Date_Incident;
            Date_Generation = P_Date_Generation;
            Chemin = P_Chemin;
            Type_Lettre = P_Type_Lettre;
            Nom_Lettre = P_Nom_Lettre;
            IDP = P_IDP;
        }
    }
}
