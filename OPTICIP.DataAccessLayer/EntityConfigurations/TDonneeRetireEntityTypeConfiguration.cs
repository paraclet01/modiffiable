using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TDonneeRetireEntityTypeConfiguration : IEntityTypeConfiguration<TDonneeRetire>
    {
        public void Configure(EntityTypeBuilder<TDonneeRetire> DonneeRetireEntityTypeConfiguration)
        {
            DonneeRetireEntityTypeConfiguration.ToTable("DonneeRetire");

            DonneeRetireEntityTypeConfiguration.Property(e => e.Id).ValueGeneratedNever();
            DonneeRetireEntityTypeConfiguration.Property(e => e.IdItem).ValueGeneratedNever();

            DonneeRetireEntityTypeConfiguration.Property(e => e.Table_Item)
              .HasColumnName("Table_Item")
              .HasMaxLength(50)
              .IsUnicode(false);

            DonneeRetireEntityTypeConfiguration.Property(e => e.CreatedBy)
              .HasColumnName("CreatedBy")
              .HasMaxLength(50)
              .IsUnicode(false);

            DonneeRetireEntityTypeConfiguration.Property(e => e.CreatedOn)
                .HasColumnName("CreatedOn");
        }
    }
}
