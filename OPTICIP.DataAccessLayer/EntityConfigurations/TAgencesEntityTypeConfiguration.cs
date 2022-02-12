using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TAgencesEntityTypeConfiguration : IEntityTypeConfiguration<TAgences>
    {
        public void Configure(EntityTypeBuilder<TAgences> AgencesEntityTypeConfiguration)
        {
            AgencesEntityTypeConfiguration.ToTable("Agences");

            AgencesEntityTypeConfiguration.Property(e => e.Id).ValueGeneratedNever();

            AgencesEntityTypeConfiguration.Property(e => e.CodeAgencce)
              .HasColumnName("CodeAgencce")
              .HasMaxLength(10)
              .IsUnicode(false);

            AgencesEntityTypeConfiguration.Property(e => e.Libelle)
              .HasColumnName("Libelle")
              .HasMaxLength(100)
              .IsUnicode(false);

            AgencesEntityTypeConfiguration.Property(e => e.Description)
                .HasColumnName("Description")
                .HasMaxLength(250)
                .IsUnicode(false);

            AgencesEntityTypeConfiguration.Property(e => e.Statut).HasColumnName("Statut");
        }
    }
}
