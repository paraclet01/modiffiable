using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;

namespace OPTICIP.Entities.Models
{
    public class TDeclarationFichier_EltEnr : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string NomFichier { get; set; }
        public string Num_Enr { get; set; }
        public string Code { get; set; }
        public Decimal NumeroOrdre { get; set; }
        public Decimal NombreCompte { get; set; }
        public DateTime DateDeclaration { get; set; }
        
        public TDeclarationFichier_EltEnr(){}

        public TDeclarationFichier_EltEnr(String P_NomFichier, String P_Num_Enr, String P_Code, DateTime P_DateDeclaration)
        {
            this.Id = Guid.NewGuid();
            this.NomFichier = P_NomFichier ?? throw new Exception(nameof(P_NomFichier));
            this.Num_Enr = P_Num_Enr ?? throw new Exception(nameof(P_Num_Enr));
            this.Code = P_Code ?? throw new Exception(nameof(P_Code));
            this.DateDeclaration = P_DateDeclaration ;
        }

    }
}
