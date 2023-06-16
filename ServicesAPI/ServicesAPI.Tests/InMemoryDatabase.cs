using ServiceStack.OrmLite;
using System.Data;

namespace ServicesAPI.Tests
{
    public class InMemoryDatabase
    {
        private readonly OrmLiteConnectionFactory dbFactory =
            new OrmLiteConnectionFactory("Server=(localdb)\\mssqllocaldb;Trusted_Connection=True;", SqlServer2016Dialect.Instance);

        public IDbConnection OpenConnection() => this.dbFactory.OpenDbConnection();

        public void CreateTable<T>(string tableName)
        {
            using (IDbConnection connection = OpenConnection())
            {
                connection.CreateTableIfNotExists<T>(tableName);
            }
        }
    }
}
