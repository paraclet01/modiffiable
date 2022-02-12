using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;


namespace OPTICIP.Entities.Models
{
    public class TAgences : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string CodeAgencce { get; set; }
        public string Libelle { get; set; }
        public string Description { get; set; }
        public int Statut { get; set; }

        public TAgences(){}

        public TAgences(Guid P_Id, String P_Code, String P_Libelle, String P_Description)
        {

            this.Id = P_Id;
            this.CodeAgencce = P_Code ?? throw new Exception(nameof(P_Code));
            this.Libelle = P_Libelle ?? throw new Exception(nameof(P_Libelle));
            this.Description = P_Description ;
            this.Statut = 0;
        }

        public void UpdateAgence(String P_Libelle, String P_Description)
        {
            //this.CodeAgencce = P_Code ?? throw new Exception(nameof(P_Code));
            this.Libelle = P_Libelle ?? throw new Exception(nameof(P_Libelle));
            this.Description = P_Description;
        }

        public void DeleteAgence()
        {
            this.Statut = 1;

        }

    }
}
