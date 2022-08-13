using Microsoft.EntityFrameworkCore;
using OPTICIP.DataAccessLayer.EntityConfigurations;
using OPTICIP.Entities.DataEntities;
using OPTICIP.Entities.Models;
using OPTICIP.SeedWork;
using System.Threading;
using System.Threading.Tasks;

namespace OPTICIP.DataAccessLayer.Models
{
    public partial class CIPContext : DbContext, IUnitOfWork
    {
        public static string _sConnectionString = "";
        public CIPContext()
        {
        }

    public CIPContext(DbContextOptions<CIPContext> options)
            : base(options)
        {
        }

        public static void InitialiserChaineParDefaut(string pConnectionStr)
        {
            _sConnectionString = pConnectionStr;
        }

        public DbSet<TCompte> TCompte { get; set; }
        public DbSet<TPersPhysique> TPersPhysique { get; set; }
        public DbSet<TPersMorale> TPersMorale { get; set; }
        public DbSet<TCarte> TCarte { get; set; }
        public DbSet<TIncidentCheque> TIncidentCheque { get; set; }
        public DbSet<TChequeIrregulier> TChequeIrregulier { get; set; }
        public DbSet<TIncidentEffet> TIncidentEffet { get; set; }
        public DbSet<TErreurEnregistrement> TErreurEnregistrement { get; set; }
        public DbSet<TErreurRetour> TErreurRetour { get; set; }
        public DbSet<TUsers> TUsers { get; set; }
        public DbSet<TLettre> TLettre { get; set; }
        public DbSet<TLettreLot> TLettreLot { get; set; }
        public DbSet<TDonnees_A_Declarer> TDonnees_A_Declarer { get; set; }
        public DbSet<THistorique_Lanc_Proc> THistorique_Lanc_Proc { get; set; }
        public DbSet<THistorique_Declarations> THistorique_Declarations { get; set; }
        public DbSet<TDeclarationFichier> TDeclarationFichier { get; set; }
        public DbSet<TDeclarationFichier_EltEnr> TDeclarationFichier_EltEnr { get; set; }
        public DbSet<TDonneeRetire> TDonneeRetire { get; set; }
        public DbSet<TLog> TLog { get; set; }
        public DbSet<TAgences> TAgences { get; set; }
        public DbSet<TParametres> TParametres { get; set; }

        //-----
        //public DbSet<CIP1_TEMPViewModel> CIP1_TEMPViewModel { get; set; }
        public virtual DbSet<OPTICIP.Entities.DataEntities.Cip1Temp> Cip1Temps { get; set; }
        public virtual DbSet<Cip2Temp> Cip2Temps { get; set; }
        public virtual DbSet<Cip3Temp> Cip3Temps { get; set; }
        public virtual DbSet<Cip4Temp> Cip4Temps { get; set; }
        public virtual DbSet<Cip5Temp> Cip5Temps { get; set; }
        public virtual DbSet<Cip6Temp> Cip6Temps { get; set; }
        public virtual DbSet<Cip7Temp> Cip7Temps { get; set; }

        public virtual DbSet<SuiviImportation> SuiviImportations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(@"Server=BLUELAPTOP\GLH_SQLSERVER;Database=CIP;User Id=sa;Password=Glh_sa");
                //optionsBuilder.UseSqlServer("Server=localhost;Database=CIP;User Id=sa;Password=password");
                if (_sConnectionString != "")
                    optionsBuilder.UseSqlServer(_sConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TCompteEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TLettreEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TLettreLotEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TPersMoraleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TPersPhysiqueEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TCarteEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TIncidentChequeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TChequeIrregulierEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TIncidentEffetEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TUsersEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TDeclarationFichierEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TDeclarationFichier_EltEnrEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TDonnees_A_DeclarerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new THistorique_Lanc_ProcEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TDonneeRetireEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TErreurEnregistrementTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TErreurRetourTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TLogEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TAgencesEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TParametresEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new THistorique_DeclarationsEntityTypeConfiguration());
            //OnModelCreatingPartial(modelBuilder);

            //---------
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            modelBuilder.Entity<Cip1Temp>(entity =>
            {
                entity.HasKey(e => e.XcipRowid)
                    .HasName("PK__CIP1_TEM__898EFAB5F3C310DF");

                entity.ToTable("CIP1_TEMP");

                entity.Property(e => e.XcipRowid)
                    .ValueGeneratedNever()
                    .HasColumnName("Xcip_ROWID");

                entity.Property(e => e.Clerib)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CLERIB");

                entity.Property(e => e.Codbnq)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODBNQ");

                entity.Property(e => e.Codgch)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODGCH");

                entity.Property(e => e.Compte)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("COMPTE");

                entity.Property(e => e.DateImportation).HasColumnType("datetime");

                entity.Property(e => e.DateModification).HasColumnType("datetime");

                entity.Property(e => e.Datenv)
                    .HasColumnType("datetime")
                    .HasColumnName("DATENV");

                entity.Property(e => e.Datfrm)
                    .HasColumnType("datetime")
                    .HasColumnName("DATFRM");

                entity.Property(e => e.Datmaj)
                    .HasColumnType("datetime")
                    .HasColumnName("DATMAJ");

                entity.Property(e => e.Datouv)
                    .HasColumnType("datetime")
                    .HasColumnName("DATOUV");

                entity.Property(e => e.Explmaj)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("EXPLMAJ");

                entity.Property(e => e.Numlig)
                    .HasColumnType("decimal(5, 0)")
                    .HasColumnName("NUMLIG");

                entity.Property(e => e.Numseq)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("NUMSEQ");

                entity.Property(e => e.State)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.StatutXcip).HasColumnName("Statut_Xcip");

                entity.Property(e => e.Type)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.Property(e => e.Valide)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALIDE");
            });

            modelBuilder.Entity<Cip2Temp>(entity =>
            {
                entity.HasKey(e => e.XcipRowid)
                    .HasName("PK__CIP2_TEM__898EFAB557FABD88");

                entity.ToTable("CIP2_TEMP");

                entity.Property(e => e.XcipRowid)
                    .ValueGeneratedNever()
                    .HasColumnName("Xcip_ROWID");

                entity.Property(e => e.Adr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADR");

                entity.Property(e => e.Adrcontact)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADRCONTACT");

                entity.Property(e => e.Clerib)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CLERIB");

                entity.Property(e => e.Codbnq)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODBNQ");

                entity.Property(e => e.Codgch)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODGCH");

                entity.Property(e => e.Commnais)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("COMMNAIS");

                entity.Property(e => e.Compte)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("COMPTE");

                entity.Property(e => e.DateImportation).HasColumnType("datetime");

                entity.Property(e => e.DateModification).HasColumnType("datetime");

                entity.Property(e => e.Datenv)
                    .HasColumnType("datetime")
                    .HasColumnName("DATENV");

                entity.Property(e => e.Datmaj)
                    .HasColumnType("datetime")
                    .HasColumnName("DATMAJ");

                entity.Property(e => e.Datnais)
                    .HasColumnType("datetime")
                    .HasColumnName("DATNAIS");

                entity.Property(e => e.Emailcontact)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EMAILCONTACT");

                entity.Property(e => e.Emailtitu)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EMAILTITU");

                entity.Property(e => e.Explmaj)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("EXPLMAJ");

                entity.Property(e => e.Iso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ISO");

                entity.Property(e => e.Mand)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MAND");

                entity.Property(e => e.Nomcontact)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NOMCONTACT");

                entity.Property(e => e.Nommari)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("NOMMARI");

                entity.Property(e => e.Nommere)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("NOMMERE");

                entity.Property(e => e.Nomnais)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("NOMNAIS");

                entity.Property(e => e.Numid)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("NUMID");

                entity.Property(e => e.Numlig)
                    .HasColumnType("decimal(5, 0)")
                    .HasColumnName("NUMLIG");

                entity.Property(e => e.Numseq)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("NUMSEQ");

                entity.Property(e => e.Payadr)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("PAYADR");

                entity.Property(e => e.Pnomcontact)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PNOMCONTACT");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("PRENOM");

                entity.Property(e => e.Residumoa)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("RESIDUMOA");

                entity.Property(e => e.Resp)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("RESP");

                entity.Property(e => e.Sexe)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SEXE");

                entity.Property(e => e.State)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.StatutXcip).HasColumnName("Statut_Xcip");

                entity.Property(e => e.Type)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.Property(e => e.Valide)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALIDE");

                entity.Property(e => e.Ville)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("VILLE");
            });

            modelBuilder.Entity<Cip3Temp>(entity =>
            {
                entity.HasKey(e => e.XcipRowid)
                    .HasName("PK__CIP3_TEM__898EFAB5E413A0C0");

                entity.ToTable("CIP3_TEMP");

                entity.Property(e => e.XcipRowid)
                    .ValueGeneratedNever()
                    .HasColumnName("Xcip_ROWID");

                entity.Property(e => e.Adr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ADR");

                entity.Property(e => e.Apegr)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("APEGR");

                entity.Property(e => e.Clerib)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CLERIB");

                entity.Property(e => e.Codape)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CODAPE");

                entity.Property(e => e.Codbnq)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODBNQ");

                entity.Property(e => e.Codgch)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODGCH");

                entity.Property(e => e.Compte)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("COMPTE");

                entity.Property(e => e.DateImportation).HasColumnType("datetime");

                entity.Property(e => e.DateModification).HasColumnType("datetime");

                entity.Property(e => e.Datenv)
                    .HasColumnType("datetime")
                    .HasColumnName("DATENV");

                entity.Property(e => e.Datmaj)
                    .HasColumnType("datetime")
                    .HasColumnName("DATMAJ");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Explmaj)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("EXPLMAJ");

                entity.Property(e => e.Forme)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FORME");

                entity.Property(e => e.Iso)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("ISO");

                entity.Property(e => e.Juricat)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("JURICAT");

                entity.Property(e => e.Mand)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MAND");

                entity.Property(e => e.Numlig)
                    .HasColumnType("decimal(5, 0)")
                    .HasColumnName("NUMLIG");

                entity.Property(e => e.Numseq)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("NUMSEQ");

                entity.Property(e => e.Pays)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("PAYS");

                entity.Property(e => e.Rcsno)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("RCSNO");

                entity.Property(e => e.Resp)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("RESP");

                entity.Property(e => e.Sigle)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SIGLE");

                entity.Property(e => e.State)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.StatutXcip).HasColumnName("Statut_Xcip");

                entity.Property(e => e.Type)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.Property(e => e.Valide)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALIDE");

                entity.Property(e => e.Ville)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("VILLE");
            });

            modelBuilder.Entity<Cip4Temp>(entity =>
            {
                entity.HasKey(e => e.XcipRowid)
                    .HasName("PK__CIP4_TEM__898EFAB563F21183");

                entity.ToTable("CIP4_TEMP");

                entity.Property(e => e.XcipRowid)
                    .ValueGeneratedNever()
                    .HasColumnName("Xcip_ROWID");

                entity.Property(e => e.Clerib)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CLERIB");

                entity.Property(e => e.Codbnq)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODBNQ");

                entity.Property(e => e.Codgch)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODGCH");

                entity.Property(e => e.Compte)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("COMPTE");

                entity.Property(e => e.Ctype)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("CTYPE");

                entity.Property(e => e.DateImportation).HasColumnType("datetime");

                entity.Property(e => e.DateModification).HasColumnType("datetime");

                entity.Property(e => e.Datenv)
                    .HasColumnType("datetime")
                    .HasColumnName("DATENV");

                entity.Property(e => e.Datetimeoppo)
                    .HasColumnType("datetime")
                    .HasColumnName("DATETIMEOPPO");

                entity.Property(e => e.Datetimevali)
                    .HasColumnType("datetime")
                    .HasColumnName("DATETIMEVALI");

                entity.Property(e => e.Datmaj)
                    .HasColumnType("datetime")
                    .HasColumnName("DATMAJ");

                entity.Property(e => e.Explmaj)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("EXPLMAJ");

                entity.Property(e => e.Idcarte)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("IDCARTE");

                entity.Property(e => e.Numlig)
                    .HasColumnType("decimal(5, 0)")
                    .HasColumnName("NUMLIG");

                entity.Property(e => e.NumseqIdp)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("NUMSEQ_IDP");

                entity.Property(e => e.Porteur)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PORTEUR");

                entity.Property(e => e.State)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.StatutXcip).HasColumnName("Statut_Xcip");

                entity.Property(e => e.Type)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.Property(e => e.Valide)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALIDE");
            });

            modelBuilder.Entity<Cip5Temp>(entity =>
            {
                entity.HasKey(e => e.XcipRowid)
                    .HasName("PK__CIP5_TEM__898EFAB517F7FBE4");

                entity.ToTable("CIP5_TEMP");

                entity.Property(e => e.XcipRowid)
                    .ValueGeneratedNever()
                    .HasColumnName("Xcip_ROWID");

                entity.Property(e => e.Benefnom)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("BENEFNOM");

                entity.Property(e => e.Benefprenom)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("BENEFPRENOM");

                entity.Property(e => e.Chqref)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("CHQREF");

                entity.Property(e => e.Clerib)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CLERIB");

                entity.Property(e => e.Codbnq)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODBNQ");

                entity.Property(e => e.Codgch)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODGCH");

                entity.Property(e => e.Compte)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("COMPTE");

                entity.Property(e => e.Darreg)
                    .HasColumnType("datetime")
                    .HasColumnName("DARREG");

                entity.Property(e => e.Datchq)
                    .HasColumnType("datetime")
                    .HasColumnName("DATCHQ");

                entity.Property(e => e.DateImportation).HasColumnType("datetime");

                entity.Property(e => e.DateModification).HasColumnType("datetime");

                entity.Property(e => e.Datenv)
                    .HasColumnType("datetime")
                    .HasColumnName("DATENV");

                entity.Property(e => e.Datinc)
                    .HasColumnType("datetime")
                    .HasColumnName("DATINC");

                entity.Property(e => e.Datmaj)
                    .HasColumnType("datetime")
                    .HasColumnName("DATMAJ");

                entity.Property(e => e.Datpre)
                    .HasColumnType("datetime")
                    .HasColumnName("DATPRE");

                entity.Property(e => e.Datreg)
                    .HasColumnType("datetime")
                    .HasColumnName("DATREG");

                entity.Property(e => e.Explmaj)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("EXPLMAJ");

                entity.Property(e => e.Mntfrf).HasColumnName("MNTFRF");

                entity.Property(e => e.Mntrej).HasColumnName("MNTREJ");

                entity.Property(e => e.Montpen).HasColumnName("MONTPEN");

                entity.Property(e => e.Motifcode)
                    .HasColumnType("decimal(1, 0)")
                    .HasColumnName("MOTIFCODE");

                entity.Property(e => e.Motifdesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MOTIFDESC");

                entity.Property(e => e.Numlig)
                    .HasColumnType("decimal(5, 0)")
                    .HasColumnName("NUMLIG");

                entity.Property(e => e.Numpen)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NUMPEN");

                entity.Property(e => e.Numseq)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("NUMSEQ");

                entity.Property(e => e.State)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.StatutXcip).HasColumnName("Statut_Xcip");

                entity.Property(e => e.Type)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.Property(e => e.Typinc)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("TYPINC");

                entity.Property(e => e.Valide)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALIDE");
            });

            modelBuilder.Entity<Cip6Temp>(entity =>
            {
                entity.HasKey(e => e.XcipRowid)
                    .HasName("PK__CIP6_TEM__898EFAB56C3F4D77");

                entity.ToTable("CIP6_TEMP");

                entity.Property(e => e.XcipRowid)
                    .ValueGeneratedNever()
                    .HasColumnName("Xcip_ROWID");

                entity.Property(e => e.Chqref1)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("CHQREF1");

                entity.Property(e => e.Chqref2)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("CHQREF2");

                entity.Property(e => e.Clerib)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CLERIB");

                entity.Property(e => e.Codbnq)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODBNQ");

                entity.Property(e => e.Codgch)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODGCH");

                entity.Property(e => e.Compte)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("COMPTE");

                entity.Property(e => e.DateImportation).HasColumnType("datetime");

                entity.Property(e => e.DateModification).HasColumnType("datetime");

                entity.Property(e => e.Datenv)
                    .HasColumnType("datetime")
                    .HasColumnName("DATENV");

                entity.Property(e => e.Datmaj)
                    .HasColumnType("datetime")
                    .HasColumnName("DATMAJ");

                entity.Property(e => e.Datoppo)
                    .HasColumnType("datetime")
                    .HasColumnName("DATOPPO");

                entity.Property(e => e.Explmaj)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("EXPLMAJ");

                entity.Property(e => e.Motif)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MOTIF");

                entity.Property(e => e.Numlig)
                    .HasColumnType("decimal(5, 0)")
                    .HasColumnName("NUMLIG");

                entity.Property(e => e.Numseq)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("NUMSEQ");

                entity.Property(e => e.State)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.StatutXcip).HasColumnName("Statut_Xcip");

                entity.Property(e => e.Type)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.Property(e => e.Valide)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALIDE");
            });

            modelBuilder.Entity<Cip7Temp>(entity =>
            {
                entity.HasKey(e => e.XcipRowid)
                    .HasName("PK__CIP7_TEM__898EFAB51F906B2E");

                entity.ToTable("CIP7_TEMP");

                entity.Property(e => e.XcipRowid)
                    .ValueGeneratedNever()
                    .HasColumnName("Xcip_ROWID");

                entity.Property(e => e.Avidom)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("AVIDOM");

                entity.Property(e => e.Clerib)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CLERIB");

                entity.Property(e => e.Codbnq)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODBNQ");

                entity.Property(e => e.Codgch)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CODGCH");

                entity.Property(e => e.Compte)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("COMPTE");

                entity.Property(e => e.DateImportation).HasColumnType("datetime");

                entity.Property(e => e.DateModification).HasColumnType("datetime");

                entity.Property(e => e.Datech)
                    .HasColumnType("datetime")
                    .HasColumnName("DATECH");

                entity.Property(e => e.Datenv)
                    .HasColumnType("datetime")
                    .HasColumnName("DATENV");

                entity.Property(e => e.Datmaj)
                    .HasColumnType("datetime")
                    .HasColumnName("DATMAJ");

                entity.Property(e => e.Datref)
                    .HasColumnType("datetime")
                    .HasColumnName("DATREF");

                entity.Property(e => e.Explmaj)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("EXPLMAJ");

                entity.Property(e => e.Mnt).HasColumnName("MNT");

                entity.Property(e => e.Motif)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MOTIF");

                entity.Property(e => e.Motifdesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MOTIFDESC");

                entity.Property(e => e.Numlig)
                    .HasColumnType("decimal(5, 0)")
                    .HasColumnName("NUMLIG");

                entity.Property(e => e.Numseq)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("NUMSEQ");

                entity.Property(e => e.Ordpai)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ORDPAI");

                entity.Property(e => e.State)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.StatutXcip).HasColumnName("Statut_Xcip");

                entity.Property(e => e.Type)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.Property(e => e.Typeff)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPEFF");

                entity.Property(e => e.Valide)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VALIDE");
            });

            modelBuilder.Entity<SuiviImportation>(entity =>
            {
                entity.HasKey(e => e.XcipRowid)
                    .HasName("PK__SuiviImp__898EFAB5E591449E");

                entity.ToTable("SuiviImportation");

                entity.Property(e => e.XcipRowid)
                    .HasColumnName("Xcip_ROWID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateImportation)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateModification).HasColumnType("datetime");

                entity.Property(e => e.NombreLignesIns).HasColumnName("NombreLignesINS");

                entity.Property(e => e.NombreLignesMaj).HasColumnName("NombreLignesMAJ");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync();
            return true;
        }
    }
}
