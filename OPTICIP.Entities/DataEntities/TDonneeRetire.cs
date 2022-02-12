using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;

namespace OPTICIP.Entities.Models
{
    public class TDonneeRetire : IAggregateRoot
    {
        public Guid Id { get; set; }
        public Guid IdItem { get; set; }
        public string Table_Item { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        
        public TDonneeRetire(){}

        public TDonneeRetire(Guid P_IdItem, String P_Table_Item, String P_CreatedBy)
        {
            this.Id = Guid.NewGuid();
            this.IdItem = P_IdItem;
            this.Table_Item = P_Table_Item ?? throw new Exception(nameof(P_Table_Item));
            this.CreatedBy = P_CreatedBy ?? throw new Exception(nameof(P_CreatedBy));
            this.CreatedOn =DateTime.UtcNow.Date;
        }

        public TDonneeRetire(Guid P_IdItem)
        {
            this.IdItem = P_IdItem;
        }

        //public void UpdateUser( String P_Nom, String P_Prenoms, String P_Profil)
        //{
        //    this.Nom = P_Nom ?? throw new Exception(nameof(P_Nom));
        //    this.Prenoms = P_Prenoms ?? throw new Exception(nameof(P_Prenoms));
        //    this.Profil = P_Profil;
        //}

        //public void UpdateMotPasse(String P_MotPasse)
        //{
        //    this.MotPasse = P_MotPasse ?? throw new Exception(nameof(P_MotPasse));
        //}

        //public void ChangStatutUser(int Statut)
        //{
        //    this.Statut = Statut;
        //}

    }
}
