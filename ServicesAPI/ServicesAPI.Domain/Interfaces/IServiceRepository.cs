namespace ServicesAPI.Domain.Interfaces
{
    public interface IServiceRepository
    {
        /// <returns>all services</returns>
        public Task<IEnumerable<Service>> GetAllAsync(int pageSize, int pageNumber, CancellationToken cancellationToken = default);

        /// <returns>all actives services that are contained in a category with the name = <paramref name="categoryName"/></returns>
        public Task<IEnumerable<Service>> GetActiveServicesByCategoryAsync(int pageSize, int pageNumber, string categoryName, CancellationToken cancellationToken = default);

        /// <returns>all services that are contained in a specialization with the ID = <paramref name="specializationId"/></returns>
        public Task<IEnumerable<Service>> GetServicesBySpecializationIdAsync(Guid specializationId, CancellationToken cancellationToken = default);

        /// <returns>the service with a specific ID if it was found or null</returns>
        public Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <returns>True if the service with ID = <paramref name="id"/> exist in the database or False</returns>
        public bool IsServiceExist(Guid id);

        /// <summary>
        /// Creates a new service in database
        /// </summary>
        public Task CreateAsync(Service service, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the service with ID = <paramref name="id"/> in database
        /// </summary>
        public Task EditAsync(Guid id, Service newService, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates status in service with ID = <paramref name="id"/>
        /// </summary>
        public Task EditStatusAsync(Guid id, bool newStatus, CancellationToken cancellationToken = default);
    }
}
