using Autofac;
using OPTICIP.API.Application.Queries.Implementation;
using OPTICIP.API.Application.Queries.Interfaces;
using OPTICIP.DataAccessLayer.DataAccessRepository;
using OPTICIP.DataAccessLayer.Models;
using OPTICIP.Entities.Models;
using OPTICIP.IContractLayer.DataAccessRepository;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;

namespace OPTICIP.API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public string PrefixCompte;
        public string QueriesConnectionString { get; set; }
        public string QueriesConnectionStringCoreDB { get; set; }
        public string ReportingDirectory { get; set; }
        public string RootRetourFilesDirectory { get; set; }
        private string LDAPAdminLogin { get; set; }
        private string LDAPAdminPassword { get; set; }
        private string LDAPAdminPath { get; set; }
        private string AccesCoreBD { get; set; }
        private int iDelaiLettreDefaut { get; set; }
        private int iTailleBlockDeclaration { get; set; }


        public ApplicationModule(string qconstr, string qconstrCoreDb, string reportingDirectory, string ldapAdminLogin, string ldapAdminPassword, string ldapAdminPath, string rootRetourFilesDirectory, string accesCoreBD, string delaiLettre, string tailleBlock)
        {
            QueriesConnectionString = qconstr;
            QueriesConnectionStringCoreDB = qconstrCoreDb;
            ReportingDirectory = reportingDirectory;
            RootRetourFilesDirectory = rootRetourFilesDirectory;

            LDAPAdminPath = ldapAdminPath;
            LDAPAdminPassword = ldapAdminPassword;
            LDAPAdminLogin = ldapAdminLogin;

            AccesCoreBD = accesCoreBD;
            iDelaiLettreDefaut = string.IsNullOrEmpty(delaiLettre) ? 0 : int.Parse(delaiLettre);
            iTailleBlockDeclaration = string.IsNullOrEmpty(tailleBlock) ? 0 : int.Parse(tailleBlock);            
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new UsersQuerie(QueriesConnectionString, new RepositoryFactory(new CIPContext()), LDAPAdminLogin, LDAPAdminPassword, LDAPAdminPath))
                   .As<IUsersQuerie>()
                   .InstancePerLifetimeScope();

            builder.Register(c => new SyntheseQueries(QueriesConnectionString))
              .As<ISyntheseQueries>()
              .InstancePerLifetimeScope();

            if (AccesCoreBD == "0")
            {
                builder.Register(c => new DeclarationQueries(QueriesConnectionString,
                  new SqlConnection(QueriesConnectionString),
                  new SqlConnection(QueriesConnectionString),
                  new RepositoryFactory(new CIPContext()), iTailleBlockDeclaration))
                     .As<IDeclarationQueries>()
                     .InstancePerLifetimeScope();

                builder.Register(c => new StatistiquesQueries(QueriesConnectionString,
                    new SqlConnection(QueriesConnectionString),
                    new SqlConnection(QueriesConnectionString)))
                       .As<IStatistiquesQueries>()
                       .InstancePerLifetimeScope();

                builder.Register(c => new DetectionQueries(
                    new SqlConnection(QueriesConnectionString), new SqlConnection(QueriesConnectionString), new RepositoryFactory(new CIPContext())))
                      .As<IDetectionQueries>()
                      .InstancePerLifetimeScope();

                builder.Register(c => new PreparationQueries(new SqlConnection(QueriesConnectionString), new RepositoryFactory(new CIPContext()),
                                      new DeclarationQueries(QueriesConnectionString,
                    new SqlConnection(QueriesConnectionString),
                    new SqlConnection(QueriesConnectionString),
                    new RepositoryFactory(new CIPContext()), iTailleBlockDeclaration), QueriesConnectionString, new ParametresQuerie(QueriesConnectionString)))
                     .As<IPreparationQueries>()
                     .InstancePerLifetimeScope();

                builder.Register(c => new ReportingQueries(new SqlConnection(QueriesConnectionString),
                    new RepositoryFactory(new CIPContext()), QueriesConnectionString, ReportingDirectory, iDelaiLettreDefaut))
                    .As<IReportingQueries>()
                    .InstancePerLifetimeScope();

                builder.Register(c => new SqlConnection(QueriesConnectionString))
                  .As<IDbConnection>()
                  .InstancePerLifetimeScope();
            }
            else
            {
                builder.Register(c => new DeclarationQueries(QueriesConnectionString,
                  new OracleConnection(QueriesConnectionStringCoreDB),
                  new SqlConnection(QueriesConnectionString),
                  new RepositoryFactory(new CIPContext()), iTailleBlockDeclaration))
                     .As<IDeclarationQueries>()
                     .InstancePerLifetimeScope();


                builder.Register(c => new StatistiquesQueries(QueriesConnectionString,
                    new OracleConnection(QueriesConnectionStringCoreDB),
                    new SqlConnection(QueriesConnectionString)))
                       .As<IStatistiquesQueries>()
                       .InstancePerLifetimeScope();

                builder.Register(c => new DetectionQueries(
                    new OracleConnection(QueriesConnectionStringCoreDB), new SqlConnection(QueriesConnectionString), new RepositoryFactory(new CIPContext())))
                      .As<IDetectionQueries>()
                      .InstancePerLifetimeScope();

                builder.Register(c => new PreparationQueries(new OracleConnection(QueriesConnectionStringCoreDB), new RepositoryFactory(new CIPContext()),
                                      new DeclarationQueries(QueriesConnectionString,
                    new OracleConnection(QueriesConnectionStringCoreDB),
                    new SqlConnection(QueriesConnectionString),
                    new RepositoryFactory(new CIPContext()), iTailleBlockDeclaration), QueriesConnectionString, new ParametresQuerie(QueriesConnectionString)))
                     .As<IPreparationQueries>()
                     .InstancePerLifetimeScope();

                builder.Register(c => new ReportingQueries(new OracleConnection(QueriesConnectionStringCoreDB),
                    new RepositoryFactory(new CIPContext()), QueriesConnectionString, ReportingDirectory, iDelaiLettreDefaut))
                    .As<IReportingQueries>()
                    .InstancePerLifetimeScope();

                builder.Register(c => new OracleConnection(QueriesConnectionStringCoreDB))
                  .As<IDbConnection>()
                  .InstancePerLifetimeScope();
            }

            builder.Register(c => new ParametresQuerie(QueriesConnectionString))
                  .As<IParametresQuerie>()
                  .InstancePerLifetimeScope();

            builder.Register(c => new RetourQueries(new RepositoryFactory(new CIPContext()), QueriesConnectionString, RootRetourFilesDirectory))
             .As<IRetourQueries>()
             .InstancePerLifetimeScope();

            builder.RegisterType<CompteRepository>()
                   .As<ICompteRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<PersPhysiqueRepository>()
                   .As<IPersPhysiqueRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<PersMoraleRepository>()
                   .As<IPersMoraleRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<IncidentChequeRepository>()
           .As<IIncidentChequeRepository>()
           .InstancePerLifetimeScope();

            builder.RegisterType<ChequeIrregulierRepository>()
           .As<IChequeIrregulierRepository>()
           .InstancePerLifetimeScope();

            builder.RegisterType<IncidentEffetRepository>()
           .As<IIncidentEffetRepository>()
           .InstancePerLifetimeScope();

            builder.RegisterType<UsersRepository>()
                   .As<IUsersRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<DeclarationFichierRepository>()
                  .As<IDeclarationFichierRepository>()
                  .InstancePerLifetimeScope();

            builder.RegisterType<DonneeRetireRepository>()
                  .As<IDonneeRetireRepository>()
                  .InstancePerLifetimeScope();

            builder.RegisterType<FileReader>()
                .As<IFileReader>()
                .InstancePerLifetimeScope();

            builder.Register(c => new SqlConnection(QueriesConnectionString))
             .As<SqlConnection>()
             .InstancePerLifetimeScope();

            builder.RegisterType<AgencesRepository>()
                  .As<IGeneriqueRepository<TAgences>>()
                  .InstancePerLifetimeScope();

            builder.RegisterType<ParametresRepository>()
                 .As<IGeneriqueRepository<TParametres>>()
                 .InstancePerLifetimeScope();

            builder.RegisterType<RepositoryFactory>()
                  .As<IRepositoryFactory>()
                  .InstancePerLifetimeScope();
        }
    }
}
