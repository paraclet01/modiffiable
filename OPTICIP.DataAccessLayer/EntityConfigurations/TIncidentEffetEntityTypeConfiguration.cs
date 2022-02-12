using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.Models;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TIncidentEffetEntityTypeConfiguration : IEntityTypeConfiguration<TIncidentEffet>
    {
        public void Configure(EntityTypeBuilder<TIncidentEffet> IncidentEffetEntityTypeConfiguration)
        {
            IncidentEffetEntityTypeConfiguration.ToTable("IncidentEffet");

            IncidentEffetEntityTypeConfiguration.Property(e => e.Id).ValueGeneratedNever();

            IncidentEffetEntityTypeConfiguration.Property(e => e.Code)
                .HasColumnName("Code")
                 .HasMaxLength(2)
                 .IsUnicode(false);

            IncidentEffetEntityTypeConfiguration.Property(e => e.Num_Enr)
               .HasColumnName("Num_Enr")
                .HasMaxLength(100)
                .IsUnicode(false);

            IncidentEffetEntityTypeConfiguration.Property(e => e.Agence)
              .HasColumnName("Agence")
               .HasMaxLength(5)
               .IsUnicode(false);

            IncidentEffetEntityTypeConfiguration.Property(e => e.RIB)
               .HasColumnName("RIB")
                .HasMaxLength(22)
                .IsUnicode(false);

            IncidentEffetEntityTypeConfiguration.Property(e => e.Cle_RIB)
               .HasColumnName("Cle_RIB")
                .HasMaxLength(2)
                .IsUnicode(false);

            IncidentEffetEntityTypeConfiguration.Property(e => e.Type_Effet)
              .HasColumnName("Type_Effet")
               .HasMaxLength(1)
               .IsUnicode(false);

            IncidentEffetEntityTypeConfiguration.Property(e => e.Echeance)
               .HasColumnName("Echeance");

            IncidentEffetEntityTypeConfiguration.Property(e => e.Date_Refus_Paiement)
               .HasColumnName("Date_Refus_Paiement");

            IncidentEffetEntityTypeConfiguration.Property(e => e.Montant)
              .HasColumnName("Montant");

            IncidentEffetEntityTypeConfiguration.Property(e => e.Ordre_Paiement_Perm)
              .HasColumnName("Ordre_Paiement_Perm")
              .HasMaxLength(1)
              .IsUnicode(false);

            IncidentEffetEntityTypeConfiguration.Property(e => e.Avis_Domiciliation)
              .HasColumnName("Avis_Domiciliation")
              .HasMaxLength(1)
              .IsUnicode(false);

            IncidentEffetEntityTypeConfiguration.Property(e => e.Motif_Non_Paiement)
              .HasColumnName("Motif_Non_Paiement")
              .HasMaxLength(1)
              .IsUnicode(false);

            IncidentEffetEntityTypeConfiguration.Property(e => e.Num_Ligne_Erreur)
              .HasColumnName("Num_Ligne_Erreur")
              .HasMaxLength(8)
              .IsUnicode(false);

            IncidentEffetEntityTypeConfiguration.Property(e => e.Etat)
              .HasColumnName("Etat")
              .HasMaxLength(1)
              .IsUnicode(false);

            IncidentEffetEntityTypeConfiguration.Property(e => e.Date_Detection)
               .HasColumnName("Date_Detection");

            IncidentEffetEntityTypeConfiguration.Property(e => e.Date_Declaration)
                .HasColumnName("Date_Declaration");

//==> CIP V2
            IncidentEffetEntityTypeConfiguration.Property(e => e.MotifDesc)
              .HasColumnName("MotifDesc")
              .HasMaxLength(50)
              .IsUnicode(false);
        }
    }
}
