using Dapper;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ServicesContext _context;

        public CategoryRepository(ServicesContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category category, CancellationToken cancellationToken = default)
        {
            var query = "INSERT INTO Categories(Id, Name, TimeSlotSize) " +
            "VALUES (@Id, @Name, @TimeSlotSize)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", category.Id);
            parameters.Add("Name", category.Name);
            parameters.Add("TimeSlotSize", category.TimeSlotSize);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = "SELECT * FROM Categories WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            using (var connection = _context.CreateConnection())
            {
                var category = await connection.QueryFirstOrDefaultAsync<Category>(query, parameters);
                return category;
            }
        }

        public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var query = "SELECT * FROM Categories WHERE Name = @Name";
            var parameters = new DynamicParameters();
            parameters.Add("Name", name);

            using (var connection = _context.CreateConnection())
            {
                var category = await connection.QueryFirstOrDefaultAsync<Category>(query, parameters);
                return category;
            }
        }
    }
}
