using System;

namespace OPTICIP.API.Application.Queries.ViewModels
{
    public class PersPhysiqueViewModel
    {
        public Guid Id { get; set; }
        public string Code { get;  set; }
        public string Num_Enr { get;  set; }
        public string Agence { get; set; }
        public string RIB { get;  set; }
        public string Cle_RIB { get;  set; }
        public string Nom_Naissance { get;  set; }
        public string Prenoms { get;  set; }
        public string Lieu_Naissance { get;  set; }
        public DateTime? Date_Naissance { get;  set; }
        public string Nom_Mari { get;  set; }
        public string Nom_Naissance_Mere { get;  set; }
        public string Nationalite { get;  set; }
        public string Sexe { get;  set; }
        public string Resident_UEMOA { get;  set; }
        public string Num_Carte_Iden { get;  set; }
        public string Responsable { get;  set; }
        public string Mandataire { get;  set; }
        public string Adresse { get;  set; }
        public string Code_Pays { get;  set; }
        public string Ville { get;  set; }
        public string Num_Reg_Com { get;  set; }
        public string Num_Ligne_Erreur { get;  set; }
        public string Etat { get;  set; }
        public Guid IdItem_Retire { get; set; }
        public DateTime Date_Declaration { get; set; }

        //==> CIP V2
        public string EmailTitu { get; set; }
        public string NomContact { get; set; }
        public string PnomContact { get; set; }
        public string AdrContact { get; set; }
        public string EmailContact { get; set; }
    }
}
