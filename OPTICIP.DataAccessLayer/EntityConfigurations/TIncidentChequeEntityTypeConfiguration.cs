using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.Models;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TIncidentChequeEntityTypeConfiguration : IEntityTypeConfiguration<TIncidentCheque>
    {
        public void Configure(EntityTypeBuilder<TIncidentCheque> IncidentChequeEntityTypeConfiguration)
        {
            IncidentChequeEntityTypeConfiguration.ToTable("IncidentCheque");

            IncidentChequeEntityTypeConfiguration.Property(e => e.Id).ValueGeneratedNever();

            IncidentChequeEntityTypeConfiguration.Property(e => e.Code)
                .HasColumnName("Code")
                 .HasMaxLength(2)
                 .IsUnicode(false);

            IncidentChequeEntityTypeConfiguration.Property(e => e.Num_Enr)
               .HasColumnName("Num_Enr")
                .HasMaxLength(100)
                .IsUnicode(false);

            IncidentChequeEntityTypeConfiguration.Property(e => e.Agence)
            .HasColumnName("Agence")
             .HasMaxLength(5)
             .IsUnicode(false);


            IncidentChequeEntityTypeConfiguration.Property(e => e.RIB)
               .HasColumnName("RIB")
                .HasMaxLength(22)
                .IsUnicode(false);

            IncidentChequeEntityTypeConfiguration.Property(e => e.Cle_RIB)
               .HasColumnName("Cle_RIB")
                .HasMaxLength(2)
                .IsUnicode(false);

            IncidentChequeEntityTypeConfiguration.Property(e => e.Type_Incident)
              .HasColumnName("Type_Incident")
               .HasMaxLength(1)
               .IsUnicode(false);

            IncidentChequeEntityTypeConfiguration.Property(e => e.Date_Emission)
               .HasColumnName("Date_Emission");

            IncidentChequeEntityTypeConfiguration.Property(e => e.Date_Refus_Paiement)
               .HasColumnName("Date_Refus_Paiement");

            IncidentChequeEntityTypeConfiguration.Property(e => e.Date_Presentation)
               .HasColumnName("Date_Presentation");

            IncidentChequeEntityTypeConfiguration.Property(e => e.Point_Depart)
              .HasColumnName("Point_Depart");

            IncidentChequeEntityTypeConfiguration.Property(e => e.Montant_Nominal)
              .HasColumnName("Montant_Nominal");

            IncidentChequeEntityTypeConfiguration.Property(e => e.Montant_Insuffisance)
              .HasColumnName("Montant_Insuffisance");

            IncidentChequeEntityTypeConfiguration.Property(e => e.Numero_Cheque)
              .HasColumnName("Numero_Cheque")
              .HasMaxLength(7)
              .IsUnicode(false);

            IncidentChequeEntityTypeConfiguration.Property(e => e.Date_Regularisation)
              .HasColumnName("Date_Regularisation");

            IncidentChequeEntityTypeConfiguration.Property(e => e.Identifiant)
              .HasColumnName("Identifiant")
              .HasMaxLength(20)
              .IsUnicode(false);

            IncidentChequeEntityTypeConfiguration.Property(e => e.Num_Ligne_Erreur)
              .HasColumnName("Num_Ligne_Erreur")
              .HasMaxLength(8)
              .IsUnicode(false);

            IncidentChequeEntityTypeConfiguration.Property(e => e.Etat)
              .HasColumnName("Etat")
              .HasMaxLength(1)
              .IsUnicode(false);

            IncidentChequeEntityTypeConfiguration.Property(e => e.Date_Detection)
               .HasColumnName("Date_Detection");

            IncidentChequeEntityTypeConfiguration.Property(e => e.Date_Declaration)
                .HasColumnName("Date_Declaration");

            IncidentChequeEntityTypeConfiguration.Property(e => e.Num_Ligne_Erreur)
              .HasColumnName("Num_Ligne_Erreur")
              .HasMaxLength(8)
              .IsUnicode(false);

            //> CIP V2
            IncidentChequeEntityTypeConfiguration.Property(e => e.MontPen)
              .HasColumnName("MontPen");
            IncidentChequeEntityTypeConfiguration.Property(e => e.BenefNom)
              .HasColumnName("BenefNom")
              .HasMaxLength(30)
              .IsUnicode(false);
            IncidentChequeEntityTypeConfiguration.Property(e => e.BenefPrenom)
          .HasColumnName("BenefPrenom")
          .HasMaxLength(30)
          .IsUnicode(false);
            IncidentChequeEntityTypeConfiguration.Property(e => e.MotifCode)
          .HasColumnName("MotifCode");
            IncidentChequeEntityTypeConfiguration.Property(e => e.MotifDesc)
          .HasColumnName("MotifDesc")
          .HasMaxLength(50)
          .IsUnicode(false);

        }
    }
}
