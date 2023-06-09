using Dapper;

namespace ServicesAPI.Persistence.Migrations
{
    public class Database
    {
        private readonly ServicesContext _context;

        public Database(ServicesContext context)
        {
            _context = context;
        }

        public void CreateDatabase(string dbName)
        {
            var query = "SELECT * FROM sys.databases WHERE name = @name";
            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);

            using (var connection = _context.CreateMasterConnection())
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {dbName}");
            }
        }
    }
}
