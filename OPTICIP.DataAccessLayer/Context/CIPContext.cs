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
        private static string _sConnectionString = "";
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
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await base.SaveChangesAsync();
            return true;
        }
    }
}
