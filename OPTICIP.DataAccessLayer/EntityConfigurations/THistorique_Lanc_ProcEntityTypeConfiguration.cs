using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.DataEntities;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class THistorique_Lanc_ProcEntityTypeConfiguration : IEntityTypeConfiguration<THistorique_Lanc_Proc>
    {
        public void Configure(EntityTypeBuilder<THistorique_Lanc_Proc> historique_Lanc_ProcEntityTypeConfiguration)
        {
            historique_Lanc_ProcEntityTypeConfiguration.ToTable("Historique_Lanc_Proc");

            historique_Lanc_ProcEntityTypeConfiguration.Property(e => e.Id)
                .ValueGeneratedNever();

            historique_Lanc_ProcEntityTypeConfiguration.Property(e => e.Nom)
               .HasColumnName("Nom")
               .HasMaxLength(100)
               .IsUnicode(false);

            historique_Lanc_ProcEntityTypeConfiguration.Property(e => e.DateDebut)
             .HasColumnName("DateDebut");

            historique_Lanc_ProcEntityTypeConfiguration.Property(e => e.DateFin)
             .HasColumnName("DateFin");

            historique_Lanc_ProcEntityTypeConfiguration.Property(e => e.Resultat)
            .HasColumnName("Resultat");
        }
    }
}
