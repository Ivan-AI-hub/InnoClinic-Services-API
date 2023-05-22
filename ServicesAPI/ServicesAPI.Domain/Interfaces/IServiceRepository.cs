namespace ServicesAPI.Domain.Interfaces
{
    public interface IServiceRepository
    {
        public IQueryable<Service> GetAll(int pageSize, int pageNumber);
        public IQueryable<Service> GetActiveServicesByCategories(int pageSize, int pageNumber, string categoryName);
        public Task<Service?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task AddAsync(Service service, CancellationToken cancellationToken = default);
        public Task EditAsync(Guid id, Service newService, CancellationToken cancellationToken = default);
        public Task EditStatusAsync(Guid id, bool newStatus, CancellationToken cancellationToken = default);
    }
}
