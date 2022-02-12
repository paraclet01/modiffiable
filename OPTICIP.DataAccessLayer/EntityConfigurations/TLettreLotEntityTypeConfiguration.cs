using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.DataEntities;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TLettreLotEntityTypeConfiguration : IEntityTypeConfiguration<TLettreLot>
    {

        public void Configure(EntityTypeBuilder<TLettreLot> LettreEntityTypeConfiguration)
        {
            LettreEntityTypeConfiguration.ToTable("LettreLot");

            LettreEntityTypeConfiguration.Property(e => e.Id)
                .ValueGeneratedNever();

            LettreEntityTypeConfiguration.Property(e => e.Chemin)
                .HasColumnName("Chemin")
                .HasMaxLength(200)
                .IsUnicode(false);

            LettreEntityTypeConfiguration.Property(e => e.Type_Lettre)
                .HasColumnName("Type_Lettre")
                .HasMaxLength(100)
                .IsUnicode(false);

            LettreEntityTypeConfiguration.Property(e => e.Nom_Lettre)
                .HasColumnName("Nom_Lettre")
                .HasMaxLength(100)
                .IsUnicode(false);

            LettreEntityTypeConfiguration.Property(e => e.Date_Generation)
            .HasColumnName("Date_Generation");
        }
    }

}
