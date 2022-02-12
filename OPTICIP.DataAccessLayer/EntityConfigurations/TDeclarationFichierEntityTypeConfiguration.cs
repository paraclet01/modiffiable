using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TDeclarationFichierEntityTypeConfiguration : IEntityTypeConfiguration<TDeclarationFichier>
    {
        public void Configure(EntityTypeBuilder<TDeclarationFichier> DeclarationFichierEntityTypeConfiguration)
        {
            DeclarationFichierEntityTypeConfiguration.ToTable("DeclarationFichier");

            DeclarationFichierEntityTypeConfiguration.Property(e => e.Id).ValueGeneratedNever();

            DeclarationFichierEntityTypeConfiguration.Property(e => e.NomFichier)
              .HasColumnName("NomFichier")
              .HasMaxLength(50)
              .IsUnicode(false);

            DeclarationFichierEntityTypeConfiguration.Property(e => e.DateDeclaration)
            .HasColumnName("DateDeclaration");
        }
    }
}
