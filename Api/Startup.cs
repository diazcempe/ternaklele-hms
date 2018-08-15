using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Api.Services;
using AutoMapper;
using Common.Helpers.CustomMVCBinders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using SimpleInjector.Integration.AspNetCore.Mvc;

namespace Api
{
    public class Startup
    {
        #region SimpleInjector
        private readonly Container _container = new Container();
        #endregion

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            #region Serilog
            ConfigureSerilog();
            #endregion
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("en-GB");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-GB") };
                options.RequestCultureProviders.Clear();
            });

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new SubstituteNullWithEmptyStringContractResolver()
            };

            // Register DbContext to DI
            services.AddDbContext<TernakLeleHmsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TernakLeleHmsContext")));

            #region AutoMapper
            services.AddAutoMapper();
            #endregion

            services.AddMvc(options =>
                {
                    options.Filters.Add(new ProducesAttribute("application/json"));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Insert(0, new JsonTrimStringConverter());  // global input trimming for JSON Body source
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });

            #region SimpleInjector
            IntegrateSimpleInjector(services);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            #region Serilog
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();
            #endregion

            #region SimpleInjector
            InitializeContainer(app);
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            #region SimpleInjector
            _container.Verify();
            #endregion

            app.UseMvc();
        }

        #region SimpleInjector
        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_container));

            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

            // DbContext
            var optionsBuilder = new DbContextOptionsBuilder<TernakLeleHmsContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("TernakLeleHmsContext"));
            _container.Register(() => new TernakLeleHmsContext(optionsBuilder.Options), Lifestyle.Scoped);

            // Services
            _container.Register<IInventoriesService, InventoriesService>(Lifestyle.Scoped);

            // Repositories
            _container.Register<IInventoriesRepository, InventoriesRepository>(Lifestyle.Scoped);
            
            // Allow Simple Injector to resolve services from ASP.NET Core.
            _container.AutoCrossWireAspNetComponents(app);
        }
        #endregion

        #region Serilog
        /// <summary>
        /// Initialise serilog
        /// </summary>
        private void ConfigureSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration) // setup the usual literate console and rolling file sinks. configure events that is supposed to go to database sink
                .CreateLogger();
        }
        #endregion
    }
}
