using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    class TErreurEnregistrementTypeConfiguration : IEntityTypeConfiguration<TErreurEnregistrement>
    {
        public void Configure(EntityTypeBuilder<TErreurEnregistrement> ErreurEnregistrementTypeConfiguration)
        {
            ErreurEnregistrementTypeConfiguration.ToTable("ErreurEnregistrement");

            ErreurEnregistrementTypeConfiguration.Property(e => e.Id)
                .ValueGeneratedNever();

            ErreurEnregistrementTypeConfiguration.Property(e => e.EnregistrementID)
              .HasColumnName("EnregistrementID");

            ErreurEnregistrementTypeConfiguration.Property(e => e.EnregistrementTable)
                .HasColumnName("EnregistrementTable")
                .HasMaxLength(100)
                 .IsUnicode(false);

            ErreurEnregistrementTypeConfiguration.Property(e => e.Texte)
               .HasColumnName("Texte")
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
