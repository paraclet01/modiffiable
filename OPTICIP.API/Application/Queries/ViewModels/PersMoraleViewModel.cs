using System;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class PersMoraleViewModel
    {
        public Guid Id { get; set; }
        public string Code { get;  set; }
        public string Num_Enr { get;  set; }
        public string Agence { get; set; }
        public string RIB { get;  set; }
        public string Cle_RIB { get;  set; }
        public string Code_Pays { get;  set; }
        public string Cat_Personne { get;  set; }
        public string Iden_Personne { get;  set; }
        public string Raison_Soc { get;  set; }
        public string Sigle { get;  set; }
        public string Code_Activite { get;  set; }
        public string Responsable { get;  set; }
        public string Mandataire { get;  set; }
        public string Adresse { get;  set; }
        public string Ville { get;  set; }
        public string Iden_BCEAO { get;  set; }
        public string Num_Ligne_Erreur { get;  set; }
        public string Etat { get;  set; }
        public Guid IdItem_Retire { get; set; }
        public DateTime? Date_Detection { get; set; }
        public DateTime? Date_Declaration { get; set; }        

        //==> CIP V2
        public string Email { get; set; }
    }
}
