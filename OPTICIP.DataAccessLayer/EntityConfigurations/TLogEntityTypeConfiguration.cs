using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.DataEntities;
using OPTICIP.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TLogEntityTypeConfiguration : IEntityTypeConfiguration<TLog>
    {

        public void Configure(EntityTypeBuilder<TLog> EntityTypeConfiguration)
        {
            EntityTypeConfiguration.ToTable("Log");

            EntityTypeConfiguration.Property(e => e.Id)
                .ValueGeneratedNever();

            EntityTypeConfiguration.Property(e => e.Action)
               .HasColumnName("Action")
               .HasMaxLength(200);

            EntityTypeConfiguration.Property(e => e.Details_Action)
                .HasColumnName("Details_Action")
                .HasMaxLength(300);

            EntityTypeConfiguration.Property(e => e.ID_Utilisateur)
                .HasColumnName("ID_Utilisateur");

            EntityTypeConfiguration.Property(e => e.Date)
                .HasColumnName("Date");
        }
    }

}
