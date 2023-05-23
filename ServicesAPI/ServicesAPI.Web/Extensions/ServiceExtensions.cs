using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using ServicesAPI.Domain.Interfaces;
using ServicesAPI.Persistence;
using ServicesAPI.Persistence.Migrations;
using ServicesAPI.Persistence.Repositories;
using ServicesAPI.Persistence.Settings;

namespace ServicesAPI.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration, string contextSettingsSectionName)
        {
            services.Configure<ContextSettings>(configuration.GetSection(contextSettingsSectionName));
            services.AddFluentMigratorCore().ConfigureRunner(c => c.AddSqlServer2016()
                .WithGlobalConnectionString(configuration.GetSection(contextSettingsSectionName).GetSection("RegularConnectionString").Value)
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

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
