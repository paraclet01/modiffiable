using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OPTICIP.API.Infrastructure.AutofacModules;
using OPTICIP.DataAccessLayer.Models;
using System;
using System.Reflection;
using log4net;
using log4net.Config;
using Logger;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace OPTICIP.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("OracleQuery.json", optional: false, reloadOnChange: true);

            configurationBuilder.AddEnvironmentVariables();

            //configurationBuilder.Build();
            Configuration = configurationBuilder.Build();
            
        }

        public static IConfiguration GetConfiguration()
        {
            return Configuration;
        }

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc(options => options.EnableEndpointRouting = false);

            String sTokenIssuer = Configuration["TokenIssuer"];

            services.AddDbContext<CIPContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"],
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });
                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });

            services.AddDbContext<CIPReportContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"],
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });
                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
            });


            //Ajout de la section de validation des token d'authentification
            services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication(options =>
            {
                //options.Authority = sTokenIssuer ?? "https://localhost:443";
                options.RequireHttpsMetadata = false;

                options.ApiName = "OPTICIP.API";
            });

            services.AddSwaggerGen(options =>
            {
#pragma warning disable CS0618 // Le type ou le membre est obsolète
                options.DescribeAllEnumsAsStrings();
#pragma warning restore CS0618 // Le type ou le membre est obsolète
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "XCIP - REST API",
                    Version = "v1",
                    Description = "XCIP REST API LIST"
                });
            });

            // active CORS POlICY
            services.AddCors();

            // active session
            services.AddSession();

            services.AddOptions();

            // configure autofac
            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"], Configuration["ConnectionStringCoreDB"], Configuration["ReportingDirectory"]
                , Configuration["LDAPAdminLogin"], Configuration["LDAPAdminPassword"], Configuration["LDAPAdminPath"], Configuration["RootRetourFilesDirectory"], Configuration["AccesCoreBD"]));

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }

            app.UseSession();
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            //app.UseCors(builder => builder.WithOrigins("http://localhost:53955").AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseMvc();
            //app.UseEndpoints(endpoints=>endpoints.MapControllers());
            app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "XCIP.API V1");
            });


            //==> YFS le 11/11/2021: Initialiser la chaine de connexion par defaut
            CIPContext.InitialiserChaineParDefaut(Configuration["ConnectionString"]);
            CIPReportContext.InitialiserChaineParDefaut(Configuration["ConnectionString"]);
            //Fin

            //==> YFS le 10/11/2021: Initialisation pour les logs
            var logPath = $@"{env.ContentRootPath}\Logs";
            if (!System.IO.Directory.Exists(logPath))
                System.IO.Directory.CreateDirectory(logPath);

            GlobalContext.Properties["versionning"] = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            GlobalContext.Properties["LogDir"] = logPath;
            XmlConfigurator.Configure();
            ApplicationLogger.LogInformation("Demarrage du service WEB OPTICIP.API");
            //Fin
        }
    }
}
