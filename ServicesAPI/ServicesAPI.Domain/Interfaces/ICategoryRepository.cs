namespace ServicesAPI.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        public Task CreateAsync(Category category, CancellationToken cancellationToken = default);
        public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
