using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.Models;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TChequeIrregulierEntityTypeConfiguration : IEntityTypeConfiguration<TChequeIrregulier>
    {
        public void Configure(EntityTypeBuilder<TChequeIrregulier> ChequeIrregulierEntityTypeConfiguration)
        {
            ChequeIrregulierEntityTypeConfiguration.ToTable("ChequeIrregulier");

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.Id)
                .ValueGeneratedNever();

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.Code)
                .HasColumnName("Code")
                 .HasMaxLength(2)
                 .IsUnicode(false);

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.Num_Enr)
               .HasColumnName("Num_Enr")
                .HasMaxLength(100)
                .IsUnicode(false);

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.Agence)
               .HasColumnName("Agence")
                .HasMaxLength(5)
                .IsUnicode(false);

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.RIB)
               .HasColumnName("RIB")
                .HasMaxLength(22)
                .IsUnicode(false);

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.Cle_RIB)
               .HasColumnName("Cle_RIB")
                .HasMaxLength(2)
                .IsUnicode(false);

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.Type_Irregularite)
              .HasColumnName("Type_Irregularite")
               .HasMaxLength(1)
               .IsUnicode(false);

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.Debut_Lot)
               .HasColumnName("Debut_Lot");

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.Fin_Lot)
              .HasColumnName("Fin_Lot")
              .HasMaxLength(7)
              .IsUnicode(false);

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.Date_Opposition)
              .HasColumnName("Date_Opposition");

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.Num_Ligne_Erreur)
              .HasColumnName("Num_Ligne_Erreur")
              .HasMaxLength(8)
              .IsUnicode(false);

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.Etat)
              .HasColumnName("Etat")
              .HasMaxLength(1)
              .IsUnicode(false);

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.Date_Detection)
               .HasColumnName("Date_Detection");

            ChequeIrregulierEntityTypeConfiguration.Property(e => e.Date_Declaration)
                .HasColumnName("Date_Declaration");

        }
    }
}
