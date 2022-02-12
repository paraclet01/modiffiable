using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.DataEntities;
using OPTICIP.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TLettreEntityTypeConfiguration : IEntityTypeConfiguration<TLettre>
    {

        public void Configure(EntityTypeBuilder<TLettre> LettreEntityTypeConfiguration)
        {
            LettreEntityTypeConfiguration.ToTable("Lettre");

            LettreEntityTypeConfiguration.Property(e => e.Id)
                .ValueGeneratedNever();

            LettreEntityTypeConfiguration.Property(e => e.Nooper)
               .HasColumnName("Nooper")
               .HasMaxLength(7)
               .IsUnicode(false);

            LettreEntityTypeConfiguration.Property(e => e.Client)
                .HasColumnName("Client")
                .HasMaxLength(6)
                .IsUnicode(false);

            LettreEntityTypeConfiguration.Property(e => e.Numero_Compte)
                .HasColumnName("Numero_Compte")
                .HasMaxLength(11);

            LettreEntityTypeConfiguration.Property(e => e.Numero_Cheque)
                .HasColumnName("Numero_Cheque")
                .HasMaxLength(7);

            LettreEntityTypeConfiguration.Property(e => e.Montant_Incident)
                .HasColumnName("Montant_Incident")
                .HasMaxLength(100);

            LettreEntityTypeConfiguration.Property(e => e.Date_Incident)
                .HasColumnName("Date_Incident");

            LettreEntityTypeConfiguration.Property(e => e.Chemin)
                .HasColumnName("Chemin")
                .HasMaxLength(200)
                .IsUnicode(false);

            LettreEntityTypeConfiguration.Property(e => e.Type_Lettre)
                .HasColumnName("Type_Lettre")
                .HasMaxLength(100)
                .IsUnicode(false);

            LettreEntityTypeConfiguration.Property(e => e.Nom_Lettre)
                .HasColumnName("Nom_Lettre")
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }

}
