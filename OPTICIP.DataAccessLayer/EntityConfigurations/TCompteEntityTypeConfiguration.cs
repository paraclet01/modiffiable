using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.Models;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TCompteEntityTypeConfiguration: IEntityTypeConfiguration<TCompte>
    {
        public void Configure(EntityTypeBuilder<TCompte> CompteEntityTypeConfiguration)
        {
            CompteEntityTypeConfiguration.ToTable("Compte");

            CompteEntityTypeConfiguration.Property(e => e.Id).ValueGeneratedNever();

            CompteEntityTypeConfiguration.Property(e => e.Cle_RIB)
               .HasColumnName("Cle_RIB")
               .HasMaxLength(2)
               .IsUnicode(false);

            CompteEntityTypeConfiguration.Property(e => e.Agence)
            .HasColumnName("Agence")
             .HasMaxLength(5)
             .IsUnicode(false);

            CompteEntityTypeConfiguration.Property(e => e.Code)
                .HasMaxLength(2)
                .IsUnicode(false);

            CompteEntityTypeConfiguration.Property(e => e.Date_Fermerture).HasColumnName("Date_Fermerture");

            CompteEntityTypeConfiguration.Property(e => e.Date_Ouverture).HasColumnName("Date_Ouverture");

            CompteEntityTypeConfiguration.Property(e => e.Date_Detection).HasColumnName("Date_Detection");

            CompteEntityTypeConfiguration.Property(e => e.Date_Declaration).HasColumnName("Date_Declaration");

            CompteEntityTypeConfiguration.Property(e => e.Num_Enr)
                .HasColumnName("Num_Enr")
                .HasMaxLength(100)
                .IsUnicode(false);

            CompteEntityTypeConfiguration.Property(e => e.Num_Ligne_Erreur)
                .HasColumnName("Num_Ligne_Erreur")
                .HasMaxLength(8)
                .IsUnicode(false);

            CompteEntityTypeConfiguration.Property(e => e.Rib)
                .HasColumnName("RIB")
                .HasMaxLength(22)
                .IsUnicode(false);

            CompteEntityTypeConfiguration.Property(e => e.Etat)
               .HasColumnName("Etat")
               .HasMaxLength(1)
               .IsUnicode(false);

        }
    }
}
