using FluentMigrator.Runner;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using ServicesAPI.Domain.Interfaces;
using ServicesAPI.Persistence;
using ServicesAPI.Persistence.Migrations;
using ServicesAPI.Persistence.Repositories;
using ServicesAPI.Persistence.Settings;
using ServicesAPI.Web.Settings;
using System.Reflection;

namespace ServicesAPI.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration, string contextSettingsSectionName)
        {
            services.Configure<ContextSettings>(configuration.GetSection(contextSettingsSectionName));

            services.AddFluentMigratorCore().ConfigureRunner(c => c.AddSqlServer2016()
                .WithGlobalConnectionString(configuration
                                            .GetSection(contextSettingsSectionName)
                                            .GetSection(nameof(ContextSettings.RegularConnectionString)).Value)
                .ScanIn(typeof(Database).Assembly).For.Migrations());

            services.AddSingleton<Database>();
            services.AddSingleton<ServicesContext>();
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ISpecializationRepository, SpecializationRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
        public static void ConfigureLogger(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment, string elasticUriSection)
        {
            services.AddSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration.Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration[elasticUriSection]))
                    {
                        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                        AutoRegisterTemplate = true,
                        NumberOfShards = 2,
                        NumberOfReplicas = 1
                    })
                    .Enrich.WithProperty("Environment", environment.EnvironmentName)
                    .ReadFrom.Configuration(configuration);
            });
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
        }

        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration, string massTransitSettingsName)
        {
            var settings = configuration.GetSection(massTransitSettingsName).Get<MassTransitSettings>();
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(settings.Host, settings.VirtualHost, h =>
                    {
                        h.Username(settings.UserName);
                        h.Password(settings.Password);
                    });
                    cfg.AddRawJsonSerializer();
                    cfg.ConfigureEndpoints(context);
                });
            });
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                           Name = "Bearer",
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
