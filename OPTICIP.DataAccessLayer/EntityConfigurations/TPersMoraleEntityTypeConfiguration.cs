using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.Models;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TPersMoraleEntityTypeConfiguration : IEntityTypeConfiguration<TPersMorale>
    {
        public void Configure(EntityTypeBuilder<TPersMorale> PersMoraleEntityTypeConfiguration)
        {
            PersMoraleEntityTypeConfiguration.ToTable("Pers_Morale");

            PersMoraleEntityTypeConfiguration.Property(e => e.Id).ValueGeneratedNever();

            PersMoraleEntityTypeConfiguration.Property(e => e.Code)
                .HasColumnName("Code")
                .HasMaxLength(2)
                .IsUnicode(false);


            PersMoraleEntityTypeConfiguration.Property(e => e.Num_Enr)
                .HasColumnName("Num_Enr")
                .HasMaxLength(100)
                .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Cle_RIB)
              .HasColumnName("Cle_RIB")
              .HasMaxLength(2)
              .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Agence)
              .HasColumnName("Agence")
               .HasMaxLength(5)
               .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.RIB)
                .HasColumnName("RIB")
                .HasMaxLength(22)
                .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Code_Pays)
                .HasColumnName("Code_Pays")
                .HasMaxLength(2)
                .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Cat_Personne)
                .HasColumnName("Cat_Personne")
                .HasMaxLength(1)
                .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Iden_Personne)
                .HasColumnName("Iden_Personne")
                .HasMaxLength(50)
                .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Raison_Soc)
                .HasColumnName("Raison_Soc")
                .HasMaxLength(50)
                .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Sigle)
                .HasColumnName("Sigle")
                .HasMaxLength(15)
                .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Code_Activite)
                .HasColumnName("Code_Activite")
                .HasMaxLength(8)
                .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Responsable)
                 .HasColumnName("Responsable")
                .HasMaxLength(1)
                .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Mandataire)
                .HasColumnName("Mandataire")
                .HasMaxLength(1)
                .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Adresse)
                .HasColumnName("Adresse")
                .HasMaxLength(50)
                .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Ville)
                .HasColumnName("Ville")
                .HasMaxLength(30)
                .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Iden_BCEAO)
                .HasColumnName("Iden_BCEAO")
                .HasMaxLength(10)
                .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Num_Ligne_Erreur)
                .HasColumnName("Num_Ligne_Erreur")
               .HasMaxLength(8)
               .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Etat)
                .HasColumnName("Etat")
               .HasMaxLength(1)
               .IsUnicode(false);

            PersMoraleEntityTypeConfiguration.Property(e => e.Date_Detection)
                .HasColumnName("Date_Detection");

            PersMoraleEntityTypeConfiguration.Property(e => e.Date_Declaration)
                .HasColumnName("Date_Declaration");

            //==> CIP V2
            PersMoraleEntityTypeConfiguration.Property(e => e.Email)
                    .HasColumnName("Email")
                    .HasMaxLength(30)
                    .IsUnicode(false);
        }
    }
}
