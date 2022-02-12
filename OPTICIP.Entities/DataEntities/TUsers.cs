using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;


namespace OPTICIP.Entities.Models
{
    public class TUsers : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenoms { get; set; }
        public string Login { get; set; }
        public string MotPasse { get; set; }
        public String Profil { get; set; }
        public int Statut { get; set; }
        
        public TUsers(){}

        public TUsers(Guid P_Id, String P_Nom, String P_Prenoms, String P_Login, String P_MotPasse, String P_Profil)
        {

            this.Id = P_Id;
            this.Nom = P_Nom ?? throw new Exception(nameof(P_Nom));
            this.Prenoms = P_Prenoms ?? throw new Exception(nameof(P_Prenoms));
            this.Login = P_Login ?? throw new Exception(nameof(P_Login));
            this.MotPasse = P_MotPasse ?? throw new Exception(nameof(P_MotPasse));
            this.Profil = P_Profil;
            this.Statut = 0; // active par defaut
        }

        public void UpdateUser( String P_Nom, String P_Prenoms, String P_Profil)
        {
            this.Nom = P_Nom ?? throw new Exception(nameof(P_Nom));
            this.Prenoms = P_Prenoms ?? throw new Exception(nameof(P_Prenoms));
            this.Profil = P_Profil;
        }

        public void UpdateMotPasse(String P_MotPasse)
        {
            this.MotPasse = P_MotPasse ?? throw new Exception(nameof(P_MotPasse));
        }

        public void ChangStatutUser(int Statut)
        {
            this.Statut = Statut;
        }

    }
}
