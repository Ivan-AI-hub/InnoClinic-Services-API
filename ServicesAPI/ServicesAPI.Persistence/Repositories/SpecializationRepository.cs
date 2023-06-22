using Dapper;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Persistence.Repositories
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly ServicesContext _context;
        public SpecializationRepository(ServicesContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Specialization specialization, CancellationToken cancellationToken = default)
        {
            var query = "INSERT INTO Specializations(Id, Name, IsActive) " +
                        "VALUES (@Id, @Name, @IsActive)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", specialization.Id);
            parameters.Add("Name", specialization.Name);
            parameters.Add("IsActive", specialization.IsActive);

            using (var connection = _context.CreateConnection())
            {
                var comand = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
                await connection.ExecuteAsync(comand);
            }
        }

        public async Task EditAsync(Guid id, Specialization newSpecialization, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE Specializations SET Name = @Name, " +
                                "IsActive = @IsActive " +
                                "WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("Name", newSpecialization.Name);
            parameters.Add("IsActive", newSpecialization.IsActive);

            using (var connection = _context.CreateConnection())
            {
                var comand = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
                await connection.ExecuteAsync(comand);
            }
        }

        public async Task EditStatusAsync(Guid id, bool newStatus, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE Specializations SET IsActive = @IsActive WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("IsActive", newStatus);

            using (var connection = _context.CreateConnection())
            {
                var comand = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
                await connection.ExecuteAsync(comand);
            }
        }

        public async Task<Specialization?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = "SELECT * FROM Specializations WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            using (var connection = _context.CreateConnection())
            {
                var comand = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
                return await connection.QueryFirstOrDefaultAsync<Specialization>(comand);
            }
        }

        public async Task<IEnumerable<Specialization>> GetSpecializationsWithoutServicesAsync(int pageSize, int pageNumber, CancellationToken cancellationToken = default)
        {
            var query = "SELECT * FROM Specializations " +
                "ORDER BY Specializations.Id " +
                "OFFSET @Skip ROWS " +
                "FETCH NEXT @Take ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("Skip", pageSize * (pageNumber - 1));
            parameters.Add("Take", pageSize);
            using (var connection = _context.CreateConnection())
            {
                var comand = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
                return await connection.QueryAsync<Specialization>(comand);
            }
        }

        public bool IsSpecializationExist(Guid id)
        {
            var query = "select count(1) from Specializations where Id=@id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            using (var connection = _context.CreateConnection())
            {
                return connection.ExecuteScalar<bool>(query, parameters);
            }
        }
    }
}
