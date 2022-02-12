using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TDeclarationFichier_EltEnrEntityTypeConfiguration : IEntityTypeConfiguration<TDeclarationFichier_EltEnr>
    {
        public void Configure(EntityTypeBuilder<TDeclarationFichier_EltEnr> DeclarationFichier_EltEnrEntityTypeConfiguration)
        {
            DeclarationFichier_EltEnrEntityTypeConfiguration.ToTable("DeclarationFichier_EltEnr");

            DeclarationFichier_EltEnrEntityTypeConfiguration.Property(e => e.Id).ValueGeneratedNever();

            DeclarationFichier_EltEnrEntityTypeConfiguration.Property(e => e.NomFichier)
              .HasColumnName("NomFichier")
              .HasMaxLength(50)
              .IsUnicode(false);

            DeclarationFichier_EltEnrEntityTypeConfiguration.Property(e => e.Num_Enr)
             .HasColumnName("Num_Enr")
             .HasMaxLength(100)
             .IsUnicode(false);

            DeclarationFichier_EltEnrEntityTypeConfiguration.Property(e => e.Code)
             .HasColumnName("Code")
             .HasMaxLength(2)
             .IsUnicode(false);

            DeclarationFichier_EltEnrEntityTypeConfiguration.Property(e => e.DateDeclaration)
            .HasColumnName("DateDeclaration");

            DeclarationFichier_EltEnrEntityTypeConfiguration.Property(e => e.NumeroOrdre)
             .HasColumnName("NumeroOrdre");

            DeclarationFichier_EltEnrEntityTypeConfiguration.Property(e => e.NombreCompte)
             .HasColumnName("NombreCompte");
        }
    }
}
