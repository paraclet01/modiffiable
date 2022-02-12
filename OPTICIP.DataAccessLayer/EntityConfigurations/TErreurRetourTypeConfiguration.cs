using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    class TErreurRetourTypeConfiguration : IEntityTypeConfiguration<TErreurRetour>
    {
        public void Configure(EntityTypeBuilder<TErreurRetour> ErreurRetourTypeConfiguration)
        {
            ErreurRetourTypeConfiguration.ToTable("ErreurRetour");

            ErreurRetourTypeConfiguration.Property(e => e.Id)
                .ValueGeneratedNever();

            ErreurRetourTypeConfiguration.Property(e => e.EnregistrementID)
              .HasColumnName("EnregistrementID");

            ErreurRetourTypeConfiguration.Property(e => e.EnregistrementTable)
                .HasColumnName("EnregistrementTable")
                .HasMaxLength(100)
                 .IsUnicode(false);

            ErreurRetourTypeConfiguration.Property(e => e.Numero_Ligne_Erreur)
               .HasColumnName("Numero_Ligne_Erreur")
                .IsUnicode(false);

            ErreurRetourTypeConfiguration.Property(e => e.Champ_Erreur)
              .HasColumnName("Champ_Erreur")
               .IsUnicode(false);

            ErreurRetourTypeConfiguration.Property(e => e.Code_Erreur)
              .HasColumnName("Code_Erreur")
               .IsUnicode(false);
        }
    }
}
