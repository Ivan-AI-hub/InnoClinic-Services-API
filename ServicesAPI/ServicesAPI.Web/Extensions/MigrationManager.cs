using FluentMigrator.Runner;
using ServicesAPI.Persistence.Migrations;

namespace ServicesAPI.Web.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

                databaseService.CreateDatabase("ServicesBase");

                migrationService.ListMigrations();
                migrationService.MigrateUp();

            }
            return host;
        }
    }
}
