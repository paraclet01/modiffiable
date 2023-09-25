using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TDonnees_A_DeclarerEntityTypeConfiguration : IEntityTypeConfiguration<TDonnees_A_Declarer>
    {
        public void Configure(EntityTypeBuilder<TDonnees_A_Declarer> Donnees_A_DeclarerEntityTypeConfiguration)
        {
            Donnees_A_DeclarerEntityTypeConfiguration.ToTable("Donnees_A_Declarer");

            Donnees_A_DeclarerEntityTypeConfiguration.Property(e => e.Id)
                .ValueGeneratedNever();

            Donnees_A_DeclarerEntityTypeConfiguration.Property(e => e.Texte)
               .HasColumnName("Texte")
               .HasMaxLength(500)
               .IsUnicode(false);

            Donnees_A_DeclarerEntityTypeConfiguration.Property(e => e.Agence)
             .HasColumnName("Agence")
             .HasMaxLength(5)
             .IsUnicode(false);

            Donnees_A_DeclarerEntityTypeConfiguration.Property(e => e.RecordID)
             .HasColumnName("RecordID")
             .HasMaxLength(500)
             .IsUnicode(false);

            Donnees_A_DeclarerEntityTypeConfiguration.Property(e => e.TableSource)
            .HasColumnName("TableSource")
            .HasMaxLength(100)
            .IsUnicode(false);

            Donnees_A_DeclarerEntityTypeConfiguration.Property(e => e.Ordre)
              .HasColumnName("Ordre");

            Donnees_A_DeclarerEntityTypeConfiguration.Property(e => e.Rib)
            .HasColumnName("Rib")
            .HasMaxLength(100)
            .IsUnicode(false);

            Donnees_A_DeclarerEntityTypeConfiguration.Property(e => e.Type_Ligne)
              .HasColumnName("Type_Ligne");

        }
    }
}
