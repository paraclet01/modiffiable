using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPTICIP.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.DataAccessLayer.EntityConfigurations
{
    public class TPersPhysiqueEntityTypeConfiguration : IEntityTypeConfiguration<TPersPhysique>
    {
        public void Configure(EntityTypeBuilder<TPersPhysique> PersPhysiqueEntityTypeConfiguration)
        {
            PersPhysiqueEntityTypeConfiguration.ToTable("Pers_Physique");

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Id)
                .ValueGeneratedNever();

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Adresse)
                .HasColumnName("Adresse")
                 .HasMaxLength(50)
                 .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Cle_RIB)
                .HasColumnName("Cle_RIB")
                .HasMaxLength(2)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Code)
                .HasColumnName("Code")
                .HasMaxLength(2)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Agence)
             .HasColumnName("Agence")
              .HasMaxLength(5)
              .IsUnicode(false);


            PersPhysiqueEntityTypeConfiguration.Property(e => e.Code_Pays)
                .HasColumnName("Code_Pays")
                .HasMaxLength(2)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Date_Naissance)
                .HasColumnName("Date_Naissance");

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Lieu_Naissance)
                .HasColumnName("Lieu_Naissance")
                .HasMaxLength(32)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Nationalite)
                .HasColumnName("Nationalite")
                .HasMaxLength(2)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Nom_Mari)
                .HasColumnName("Nom_Mari")
                .HasMaxLength(32)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Nom_Naissance)
                .HasColumnName("Nom_Naissance")
                .HasMaxLength(32)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Nom_Naissance_Mere)
                .HasColumnName("Nom_Naissance_Mere")
                .HasMaxLength(32)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Num_Carte_Iden)
                .HasColumnName("Num_Carte_Iden")
                .HasMaxLength(16)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Num_Enr)
                .HasColumnName("Num_Enr")
                .HasMaxLength(100)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Num_Ligne_Erreur)
                .HasColumnName("Num_Ligne_Erreur")
                .HasMaxLength(8)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Num_Reg_Com)
                .HasColumnName("Num_Reg_Com")
                .HasMaxLength(50)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Prenoms)
                .HasColumnName("Prenoms")
                .HasMaxLength(32)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Resident_UEMOA)
                .HasColumnName("Resident_UEMOA")
                .HasMaxLength(1)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Sexe)
               .HasColumnName("Sexe")
               .HasMaxLength(1)
               .IsFixedLength()
               .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Responsable)
               .HasColumnName("Mandataire")
               .HasMaxLength(1)
               .IsFixedLength()
               .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Responsable)
                .HasColumnName("Responsable")
                .HasMaxLength(10)
                .IsFixedLength()
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.RIB)
                .HasColumnName("RIB")
                .HasMaxLength(22)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Ville)
                .HasMaxLength(32)
                .IsUnicode(false);

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Date_Detection)
                .HasColumnName("Date_Detection");

            PersPhysiqueEntityTypeConfiguration.Property(e => e.Date_Declaration)
                .HasColumnName("Date_Declaration");

//==> CIP V2
        PersPhysiqueEntityTypeConfiguration.Property(e => e.EmailTitu)
                .HasColumnName("EmailTitu")
                .HasMaxLength(30)
                .IsUnicode(false);
        PersPhysiqueEntityTypeConfiguration.Property(e => e.NomContact)
                .HasColumnName("NomContact")
                .HasMaxLength(30)
                .IsUnicode(false);
        PersPhysiqueEntityTypeConfiguration.Property(e => e.PnomContact)
                .HasColumnName("PnomContact")
                .HasMaxLength(30)
                .IsUnicode(false);
        PersPhysiqueEntityTypeConfiguration.Property(e => e.AdrContact)
                .HasColumnName("AdrContact")
                .HasMaxLength(50)
                .IsUnicode(false);
        PersPhysiqueEntityTypeConfiguration.Property(e => e.EmailContact)
                .HasColumnName("EmailContact")
                .HasMaxLength(30)
                .IsUnicode(false);
    }
}
}
