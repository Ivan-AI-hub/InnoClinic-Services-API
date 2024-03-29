﻿using Dapper;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Persistence.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ServicesContext _context;

        public ServiceRepository(ServicesContext servicesContext)
        {
            _context = servicesContext;
        }


        public async Task CreateAsync(Service service, CancellationToken cancellationToken = default)
        {
            var query = "INSERT INTO Services(Id, Name, Price, Status, SpecializationId, CategoryId) " +
                "VALUES (@Id, @Name, @Price, @Status, @SpecializationId, @CategoryId)";

            var parameters = new DynamicParameters();
            parameters.Add("Id", service.Id);
            parameters.Add("Name", service.Name);
            parameters.Add("Price", service.Price);
            parameters.Add("Status", service.Status);
            parameters.Add("SpecializationId", service.SpecializationId);
            parameters.Add("CategoryId", service.CategoryId);

            using (var connection = _context.CreateConnection())
            {
                var comand = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
                await connection.ExecuteAsync(comand);
            }
        }

        public async Task EditAsync(Guid id, Service newService, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE Services SET Name = @Name, " +
                                            "Price = @Price," +
                                            "Status = @Status, " +
                                            "SpecializationId = @SpecializationId, " +
                                            "CategoryId = @CategoryId " +
                                            "WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("Name", newService.Name);
            parameters.Add("Price", newService.Price);
            parameters.Add("Status", newService.Status);
            parameters.Add("SpecializationId", newService.SpecializationId);
            parameters.Add("CategoryId", newService.CategoryId);

            using (var connection = _context.CreateConnection())
            {
                var comand = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
                await connection.ExecuteAsync(comand);
            }
        }

        public async Task EditStatusAsync(Guid id, bool newStatus, CancellationToken cancellationToken = default)
        {
            var query = "UPDATE Services SET Status = @Status WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("Status", newStatus);

            using (var connection = _context.CreateConnection())
            {
                var comand = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
                await connection.ExecuteAsync(comand);
            }
        }

        public async Task<IEnumerable<Service>> GetActiveServicesByCategoryAsync(int pageSize, int pageNumber, string categoryName, CancellationToken cancellationToken = default)
        {
            var query = "SELECT * " +
                        "FROM Services JOIN Categories ON Categories.Id = CategoryId " +
                        "WHERE Status = @Status AND Categories.Name = @CategoryName " +
                        "ORDER BY Services.Id " +
                        "OFFSET @Skip ROWS " +
                        "FETCH NEXT @Take ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("Status", true);
            parameters.Add("CategoryName", categoryName);
            parameters.Add("Skip", pageSize * (pageNumber - 1));
            parameters.Add("Take", pageSize);

            using (var connection = _context.CreateConnection())
            {
                var comand = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
                var services = await connection.QueryAsync<Service, Category, Service>(comand,
                                                       (service, category) => { service.Category = category; return service; });
                return services;
            }
        }

        public async Task<IEnumerable<Service>> GetAllAsync(int pageSize, int pageNumber, CancellationToken cancellationToken = default)
        {
            var query = "SELECT * FROM Services " +
                        "ORDER BY Id " +
                        "OFFSET @Skip ROWS " +
                        "FETCH NEXT @Take ROWS ONLY"; ;

            var parameters = new DynamicParameters();
            parameters.Add("Skip", pageSize * (pageNumber - 1));
            parameters.Add("Take", pageSize);
            using (var connection = _context.CreateConnection())
            {
                var comand = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
                var services = await connection.QueryAsync<Service>(comand);
                return services;
            }
        }

        public async Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = "SELECT * FROM Services WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            using (var connection = _context.CreateConnection())
            {
                var comand = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
                var service = await connection.QueryFirstOrDefaultAsync<Service>(comand);
                return service;
            }
        }

        public async Task<IEnumerable<Service>> GetServicesBySpecializationIdAsync(Guid specializationId, CancellationToken cancellationToken = default)
        {
            var query = "SELECT * FROM Services JOIN Categories ON Categories.Id = CategoryId " +
                "WHERE SpecializationId = @SpecializationId";
            var parameters = new DynamicParameters();
            parameters.Add("SpecializationId", specializationId);

            using (var connection = _context.CreateConnection())
            {
                var comand = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
                var service = await connection.QueryAsync<Service, Category, Service>(comand,
                                                            (service, category) => { service.Category = category; return service; });
                return service;
            }
        }

        public bool IsServiceExist(Guid id)
        {
            var query = "select count(1) from Services where Id=@id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            using (var connection = _context.CreateConnection())
            {
                var service = connection.ExecuteScalar<bool>(query, parameters);
                return service;
            }
        }
    }
}
