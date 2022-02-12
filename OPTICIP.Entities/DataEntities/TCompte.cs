using OPTICIP.SeedWork;
using System;

namespace OPTICIP.Entities.Models
{
    public class TCompte : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Num_Enr { get; set; }
        public string Agence { get; set; }
        public string Rib { get; set; }
        public string Cle_RIB { get; set; }
        public DateTime? Date_Ouverture { get; set; }
        public DateTime? Date_Fermerture { get; set; }
        public string Num_Ligne_Erreur { get; set; }
        public string Etat { get; set; }
        public DateTime? Date_Detection { get; set; }
        public DateTime? Date_Declaration { get; set; }

        public TCompte(){}

        public TCompte(Guid P_Id, String P_Code, String P_NumEnr, string P_Agence, String P_Rib, String P_CleRib, 
            DateTime? P_DateOuverture, DateTime? P_DateFermeture, String P_NumLigneErreur, String P_Etat,
             DateTime? P_DateDetection, DateTime? P_DateDeclaration)
        {
            this.Id = P_Id;
            this.Code = P_Code ?? throw new Exception(nameof(P_Code));
            this.Num_Enr = P_NumEnr ?? throw new Exception(nameof(P_NumEnr));
            this.Rib = P_Rib ?? throw new Exception(nameof(P_Rib));
            Agence = P_Agence;
            this.Cle_RIB = P_CleRib ?? throw new Exception(nameof(P_CleRib));
            this.Date_Ouverture = P_DateOuverture;
            this.Date_Fermerture = P_DateFermeture;
            this.Date_Detection = P_DateDetection;
            this.Date_Declaration = P_DateDeclaration;
            this.Num_Ligne_Erreur = P_NumLigneErreur;
            this.Etat = P_Etat;
        }

        public void Update(String P_Etat, DateTime P_Date_Declaration)
        {
            this.Etat = P_Etat ?? throw new Exception(nameof(P_Etat));
            this.Date_Declaration = P_Date_Declaration;
        }

        public void UpdateDate(DateTime P_Date_Ouverture, DateTime? P_Date_Fermerture)
        {
            this.Date_Ouverture = P_Date_Ouverture;
            this.Date_Fermerture = P_Date_Fermerture;
        }

        public void DeleteCompte()
        {

        }

    }
}
