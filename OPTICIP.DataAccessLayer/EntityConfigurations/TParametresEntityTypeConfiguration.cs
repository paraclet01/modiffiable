using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TParametresEntityTypeConfiguration : IEntityTypeConfiguration<TParametres>
    {
        public void Configure(EntityTypeBuilder<TParametres> ParametresEntityTypeConfiguration)
        {
            ParametresEntityTypeConfiguration.ToTable("Parametres");

            ParametresEntityTypeConfiguration.Property(e => e.Id).ValueGeneratedNever();

            ParametresEntityTypeConfiguration.Property(e => e.Code) 
              .HasColumnName("Code")
              .HasMaxLength(10)
              .IsUnicode(false);

            ParametresEntityTypeConfiguration.Property(e => e.Libelle)
              .HasColumnName("Libelle")
              .HasMaxLength(100)
              .IsUnicode(false);

            ParametresEntityTypeConfiguration.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(250)
                .IsUnicode(false);

        }
    }
}
