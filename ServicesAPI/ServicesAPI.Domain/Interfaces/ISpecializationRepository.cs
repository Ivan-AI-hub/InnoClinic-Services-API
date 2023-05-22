namespace ServicesAPI.Domain.Interfaces
{
    public interface ISpecializationRepository
    {
        public IQueryable<Specialization> GetSpecializationsWithoutServices(int pageSize, int pageNumber);
        public Task<Specialization?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task CreateAsync(Specialization specialization, CancellationToken cancellationToken = default);
        public Task EditAsync(Guid id, Specialization newSpecialization, CancellationToken cancellationToken = default);
        public Task EditStatusAsync(Guid id, bool newStatus, CancellationToken cancellationToken = default);
    }
}
