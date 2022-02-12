using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;


namespace OPTICIP.Entities.Models
{
    public class TParametres : IAggregateRoot
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Libelle { get; set; }
        public string Description { get; set; }

        public TParametres(){}

        public TParametres(int P_Id, String P_Code, String P_Libelle, String P_Description)
        {

            this.Id = P_Id;
            this.Code = P_Code ?? throw new Exception(nameof(P_Code));
            this.Libelle = P_Libelle ?? throw new Exception(nameof(P_Libelle));
            this.Description = P_Description ;
        }

        public void Update(String P_Code, String P_Libelle, String P_Description)
        {
            if (!String.IsNullOrEmpty(P_Code))
                this.Code = P_Code ?? throw new Exception(nameof(P_Code));

            this.Libelle = P_Libelle ?? throw new Exception(nameof(P_Libelle));
            this.Description = P_Description;
        }

    }
}
