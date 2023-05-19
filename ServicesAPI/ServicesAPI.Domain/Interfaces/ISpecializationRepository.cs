namespace ServicesAPI.Domain.Interfaces
{
    public interface ISpecializationRepository
    {
        public IQueryable<Specialization> GetAll(int pageSize, int pageNumber, IFiltrator<Specialization> filtrator);
        public Task<Specialization> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task AddAsync(Specialization specialization, CancellationToken cancellationToken = default);
        public Task EditAsync(Guid id, Specialization newSpecialization, CancellationToken cancellationToken = default);
        public Task EditStatusAsync(Guid id, bool newStatus, CancellationToken cancellationToken = default);
    }
}
