using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OPTICIP.DataAccessLayer.EntityConfigurations;

namespace OPTICIP.DataAccessLayer.Models
{
    public partial class CIPReportContext : DbContext
    {
        public static string _sConnectionString = "";
        public CIPReportContext()
        {
        }

        public CIPReportContext(DbContextOptions<CIPReportContext> options)
            : base(options)
        {
        }

        public static void InitialiserChaineParDefaut(string pConnectionStr)
        {
            _sConnectionString = pConnectionStr;
        }

        public virtual DbSet<AttNonPaiementCheque> AttNonPaiementCheque { get; set; }
        public virtual DbSet<AttNonPaiementEffet> AttNonPaiementEffet { get; set; }
        public virtual DbSet<CertificatNonPaiement> CertificatNonPaiement { get; set; }
        public virtual DbSet<DonneesIncidentChq> DonneesIncidentChq { get; set; }
        public virtual DbSet<DonneesMandataireIncident> DonneesMandataireIncident { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CIP_V2;Trusted_Connection=True;");
                if (_sConnectionString != "")
                    optionsBuilder.UseSqlServer(_sConnectionString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<AttNonPaiementCheque>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Agencelib)
                    .HasColumnName("agencelib")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Banque)
                    .HasColumnName("banque")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Compte)
                    .HasColumnName("compte")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DateInsertion).HasColumnType("datetime");

                entity.Property(e => e.DateRegularisation)
                    .HasColumnName("date_regularisation")
                    .HasColumnType("datetime");

                entity.Property(e => e.Montchq)
                    .HasColumnName("montchq")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nompre)
                    .HasColumnName("nompre")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nooper)
                    .HasColumnName("nooper")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Numcheq)
                    .HasColumnName("numcheq")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PenaliteLiberatoire)
                    .HasColumnName("penalite_liberatoire")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AttNonPaiementEffet>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Adrpostal)
                    .HasColumnName("adrpostal")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Beneficiaire)
                    .HasColumnName("beneficiaire")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Compte)
                    .HasColumnName("compte")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DateInsertion).HasColumnType("datetime");

                entity.Property(e => e.Datech)
                    .HasColumnName("datech")
                    .HasColumnType("datetime");

                entity.Property(e => e.Datrej)
                    .HasColumnName("datrej")
                    .HasColumnType("datetime");

                entity.Property(e => e.Librej)
                    .HasColumnName("librej")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Montant)
                    .HasColumnName("montant")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nooper)
                    .HasColumnName("nooper")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CertificatNonPaiement>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Agencelib)
                    .HasColumnName("agencelib")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BankDest)
                    .HasColumnName("bank_dest")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Chqref)
                    .HasColumnName("chqref")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Client)
                    .HasColumnName("client")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Compte)
                    .HasColumnName("compte")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Datdep)
                    .HasColumnName("datdep")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateInsertion).HasColumnType("datetime");

                entity.Property(e => e.Datinc)
                    .HasColumnName("datinc")
                    .HasColumnType("datetime");

                entity.Property(e => e.Datpre)
                    .HasColumnName("datpre")
                    .HasColumnType("datetime");

                entity.Property(e => e.Mntrej)
                    .HasColumnName("mntrej")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MotifLibelle)
                    .HasColumnName("motif_libelle")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NomClient)
                    .HasColumnName("nom_client")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nooper)
                    .HasColumnName("nooper")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DonneesIncidentChq>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.adrgeo)
                    .HasColumnName("adrgeo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.adrpostal)
                    .HasColumnName("adrpostal")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.agencelib)
                    .HasColumnName("agencelib")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.benef)
                    .HasColumnName("benef")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.compte)
                    .HasColumnName("compte")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.dateInsertion).HasColumnType("datetime");

                entity.Property(e => e.datemi)
                    .HasColumnName("datemi")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.datinc)
                    .HasColumnName("datinc")
                    .HasColumnType("datetime");

                entity.Property(e => e.datpre)
                    .HasColumnName("datpre")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.montchq)
                    .HasColumnName("montchq")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.motif)
                    .HasColumnName("motif")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.nompre)
                    .HasColumnName("nompre")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.nooper)
                    .HasColumnName("nooper")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.numcheq)
                    .HasColumnName("numcheq")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.pays)
                    .HasColumnName("pays")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.solde_incident)
                    .HasColumnName("solde_incident")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.libinf)
                    .HasColumnName("libinf")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ville)
                    .HasColumnName("ville")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DonneesMandataireIncident>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Adrgeo)
                    .HasColumnName("adrgeo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Adrpostal)
                    .HasColumnName("adrpostal")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Benef)
                    .HasColumnName("benef")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Civilite)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Compte)
                    .HasColumnName("compte")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DateInsertion).HasColumnType("datetime");

                entity.Property(e => e.Datemi)
                    .HasColumnName("datemi")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Datinc)
                    .HasColumnName("datinc")
                    .HasColumnType("datetime");

                entity.Property(e => e.Datpre)
                    .HasColumnName("datpre")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Idp)
                    .HasColumnName("idp")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Montchq)
                    .HasColumnName("montchq")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Motif)
                    .HasColumnName("motif")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .HasColumnName("nom")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nom_Client)
                    .HasColumnName("Nom_Client")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nompre)
                    .HasColumnName("nompre")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nooper)
                    .HasColumnName("nooper")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Numcheq)
                    .HasColumnName("numcheq")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Pays)
                    .HasColumnName("pays")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Prenom)
                    .HasColumnName("prenom")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TypeIncident)
                    .HasColumnName("type_incident")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ville)
                    .HasColumnName("ville")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
