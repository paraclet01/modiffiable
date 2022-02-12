using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TCarteEntityTypeConfiguration : IEntityTypeConfiguration<TCarte>
    {
        public void Configure(EntityTypeBuilder<TCarte> CarteEntityTypeConfiguration)
        {
            CarteEntityTypeConfiguration.ToTable("Carte");

            CarteEntityTypeConfiguration.Property(e => e.Id)
                .ValueGeneratedNever();

            CarteEntityTypeConfiguration.Property(e => e.Code)
                .HasColumnName("Code")
                 .HasMaxLength(2)
                 .IsUnicode(false);

            CarteEntityTypeConfiguration.Property(e => e.Num_Carte)
               .HasColumnName("Num_Carte")
                .HasMaxLength(16)
                .IsUnicode(false);

            CarteEntityTypeConfiguration.Property(e => e.Agence)
               .HasColumnName("Agence")
                .HasMaxLength(5)
                .IsUnicode(false);

            CarteEntityTypeConfiguration.Property(e => e.RIB)
               .HasColumnName("RIB")
                .HasMaxLength(22)
                .IsUnicode(false);

            CarteEntityTypeConfiguration.Property(e => e.Cle_RIB)
               .HasColumnName("Cle_RIB")
                .HasMaxLength(2)
                .IsUnicode(false);

            CarteEntityTypeConfiguration.Property(e => e.Date_Fin_Validite)
               .HasColumnName("Date_Fin_Validite");

            CarteEntityTypeConfiguration.Property(e => e.Titulaire)
              .HasColumnName("Titulaire")
               .HasMaxLength(100)
               .IsUnicode(false);

            CarteEntityTypeConfiguration.Property(e => e.Type_Carte)
              .HasColumnName("Type_Carte")
               .HasMaxLength(1)
               .IsUnicode(false);

            CarteEntityTypeConfiguration.Property(e => e.Date_Opposition)
              .HasColumnName("Date_Opposition");

            CarteEntityTypeConfiguration.Property(e => e.Num_Ligne_Erreur)
              .HasColumnName("Num_Ligne_Erreur")
               .HasMaxLength(5)
               .IsUnicode(false);

            CarteEntityTypeConfiguration.Property(e => e.Etat)
              .HasColumnName("Etat")
               .HasMaxLength(1)
               .IsUnicode(false);

            CarteEntityTypeConfiguration.Property(e => e.Date_Detection)
               .HasColumnName("Date_Detection");

            CarteEntityTypeConfiguration.Property(e => e.Date_Declaration)
                .HasColumnName("Date_Declaration");

            //==> CIP V2
            CarteEntityTypeConfiguration.Property(e => e.Porteur)
                    .HasColumnName("Porteur")
                    .HasMaxLength(30)
                    .IsUnicode(false);

        }
    }
}
