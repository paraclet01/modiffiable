using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class THistorique_DeclarationsEntityTypeConfiguration : IEntityTypeConfiguration<THistorique_Declarations>
    {

        public void Configure(EntityTypeBuilder<THistorique_Declarations> Historique_DeclarationsEntityTypeConfiguration)
        {
            Historique_DeclarationsEntityTypeConfiguration.ToTable("Historique_Declarations");

            Historique_DeclarationsEntityTypeConfiguration.Property(e => e.Id)
                .ValueGeneratedNever();

            Historique_DeclarationsEntityTypeConfiguration.Property(e => e.Agence)
               .HasColumnName("Agence")
               .HasMaxLength(500)
               .IsUnicode(false);

            Historique_DeclarationsEntityTypeConfiguration.Property(e => e.Id_Declaration)
                .HasColumnName("Id_Declaration");

            Historique_DeclarationsEntityTypeConfiguration.Property(e => e.RecordID)
                .HasColumnName("RecordID");

            Historique_DeclarationsEntityTypeConfiguration.Property(e => e.TableSource)
                .HasColumnName("TableSource")
                .HasMaxLength(100)
                .IsUnicode(false);

            Historique_DeclarationsEntityTypeConfiguration.Property(e => e.Text)
                .HasColumnName("Text")
                .HasMaxLength(500)
                .IsUnicode(false);

            Historique_DeclarationsEntityTypeConfiguration.Property(e => e.Ordre)
                .HasColumnName("Ordre");
        }
    }
}
