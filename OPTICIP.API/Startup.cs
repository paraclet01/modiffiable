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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;
using System.Text;
using Newtonsoft.Json;
//using System.Net.Mail;
//using System.Text.RegularExpressions;

namespace OPTICIP.API
{
    public class ErrorDto
    {
        public int Code { get; set; }
        public string Message { get; set; }

        // other fields

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

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

            services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressMapClientErrors = true;
                //options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
                //    "https://httpstatuses.com/404";
                //options.DisableImplicitFromServicesParameters = true;
            });
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
                , Configuration["LDAPAdminLogin"], Configuration["LDAPAdminPassword"], Configuration["LDAPAdminPath"], Configuration["RootRetourFilesDirectory"], Configuration["AccesCoreBD"], Configuration["DelaiLettreDefaut"], Configuration["TailleBlockDeclaration"]));


            //string mailSource = "cpt@gmail.com";
            //var ret = IsValidEmailAddress(ref mailSource);
            ////ret = char.IsLetter('@');

            return new AutofacServiceProvider(container.Build());
        }

        //public static bool IsValidEmailAddress(ref string valeurChamp)
        //{

        //    try
        //    {
        //         //emailPattern =  @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        //        string pattern = @"^[a-zA-Z0-9._%+-]+(?<![_.-])+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        //        if (String.IsNullOrEmpty(valeurChamp))
        //            return false;

        //        if (valeurChamp.Trim() == "0" || valeurChamp.Trim() == @"N\A" || valeurChamp.Trim() == @"N/A")
        //        {
        //            valeurChamp = "";
        //            return false;
        //        }

        //        string[] listMail = valeurChamp.Split(new char[] { ',', ';', '/' });
        //        valeurChamp = listMail[0].Trim();
        //        return !Regex.IsMatch(valeurChamp, pattern);
        //    }
        //    catch (FormatException)
        //    {
        //        return true;
        //    }

        //}

        //bool champEmailInvalide(ref string valeurChamp)
        //{
        //    try
        //    {
        //        if (String.IsNullOrEmpty(valeurChamp))
        //            return false;

        //        if (valeurChamp.Trim() == "0" || valeurChamp.Trim() == @"N\A" || valeurChamp.Trim() == @"N/A")
        //        {
        //            valeurChamp = "";
        //            return false;
        //        }

        //        string[] listMail = valeurChamp.Split(new char[] { ',', ';', '/' });
        //        valeurChamp = listMail[0].Trim();
        //        var mailAddress = new MailAddress(valeurChamp);
        //        return false;
        //    }
        //    catch (FormatException)
        //    {
        //        return true;
        //    }
        //}

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


            //app.UseExceptionHandler(c => c.Run(async context =>
            //{
            //    var exception = context.Features
            //        .Get<IExceptionHandlerPathFeature>()
            //        .Error;
            //    var response = new { error = exception.Message };
            //    await context.Response.WriteAsJsonAsync(response);
            //}));

            //app.UseExceptionHandler(errorApp =>
            //{
            //    errorApp.Run(async context =>
            //    {
            //        context.Response.StatusCode = 500; // or another Status accordingly to Exception Type
            //        context.Response.ContentType = "application/json";

            //        var error = context.Features.Get<IExceptionHandlerFeature>();
            //        if (error != null)
            //        {
            //            var ex = error.Error;

            //            await context.Response.WriteAsync(new ErrorDto()
            //            {
            //                Code = 508,
            //                Message = ex.Message // or your custom message
            //                // other custom data
            //            }.ToString(), Encoding.UTF8);
            //        }
            //    });
            //});
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
