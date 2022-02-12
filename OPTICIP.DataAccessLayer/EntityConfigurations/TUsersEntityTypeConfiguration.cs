using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TUsersEntityTypeConfiguration: IEntityTypeConfiguration<TUsers>
    {
        public void Configure(EntityTypeBuilder<TUsers> UsersEntityTypeConfiguration)
        {
            UsersEntityTypeConfiguration.ToTable("Utilisateurs");

            UsersEntityTypeConfiguration.Property(e => e.Id).ValueGeneratedNever();

            UsersEntityTypeConfiguration.Property(e => e.Nom)
              .HasColumnName("Nom")
              .HasMaxLength(50)
              .IsUnicode(false);

            UsersEntityTypeConfiguration.Property(e => e.Prenoms)
              .HasColumnName("Prenoms")
              .HasMaxLength(50)
              .IsUnicode(false);

            UsersEntityTypeConfiguration.Property(e => e.Login)
                .HasColumnName("login")
                .HasMaxLength(50)
                .IsUnicode(false);

            UsersEntityTypeConfiguration.Property(e => e.MotPasse)
                .HasColumnName("motpasse")
                .HasMaxLength(50)
                .IsUnicode(false);

            UsersEntityTypeConfiguration.Property(e => e.Profil).HasColumnName("IdProfil");

            UsersEntityTypeConfiguration.Property(e => e.Statut).HasColumnName("Statut");
        }
    }
}
