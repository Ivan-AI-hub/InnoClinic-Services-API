namespace ServicesAPI.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Creates a new category in database
        /// </summary>
        public Task CreateAsync(Category category, CancellationToken cancellationToken = default);

        /// <returns>the category with a specific id if it was found or null</returns>
        public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <returns>the category with a specific name if it was found or null</returns>
        public Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
