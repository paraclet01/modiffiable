using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.Entities.DataEntities
{
    public class THistorique_Lanc_Proc : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int Resultat { get; set; }

        public THistorique_Lanc_Proc()
        {
        }

        public THistorique_Lanc_Proc(Guid P_Id, string P_Nom, DateTime P_DateDebut, DateTime P_DateFin, int P_Resultat)
        {
            Id = P_Id;
            Nom = P_Nom;
            DateDebut = P_DateDebut;
            DateFin = P_DateFin;
            Resultat = P_Resultat;
        }
    }
}
