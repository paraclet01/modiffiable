using OPTICIP.SeedWork;
using System;
using System.Collections.Generic;

namespace OPTICIP.Entities.Models
{
    public class TDeclarationFichier : IAggregateRoot
    {
        public Guid Id { get; set; }
        public string NomFichier { get; set; }
        public DateTime DateDeclaration { get; set; }
        
        public TDeclarationFichier(){}

        public TDeclarationFichier(Guid P_Id, String P_NomFichier, DateTime P_DateDeclaration)
        {
            this.Id = P_Id;
            this.NomFichier = P_NomFichier ?? throw new Exception(nameof(P_NomFichier));
            this.DateDeclaration = P_DateDeclaration ;
        }

    }
}
