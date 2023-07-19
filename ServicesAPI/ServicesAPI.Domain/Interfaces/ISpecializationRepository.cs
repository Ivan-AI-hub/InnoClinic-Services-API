namespace ServicesAPI.Domain.Interfaces
{
    public interface ISpecializationRepository
    {
        /// <returns>specializations without services</returns>
        public Task<IEnumerable<Specialization>> GetSpecializationsWithoutServicesAsync(int pageSize, int pageNumber, CancellationToken cancellationToken = default);

        /// <returns>the specialization with a specific ID if it was found or null</returns>
        public Task<Specialization?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        /// <returns>the specialization with a specific ID if it was found or null</returns>
        public Task<Specialization?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

        /// <returns>True if the specialization with ID = <paramref name="id"/> exist in the database or False</returns>
        public bool IsSpecializationExist(Guid id);

        /// <summary>
        /// Creates a new specialization in database
        /// </summary>
        public Task CreateAsync(Specialization specialization, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the specialization with ID = <paramref name="id"/> in database
        /// </summary>
        public Task EditAsync(Guid id, Specialization newSpecialization, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates status in specialization with ID = <paramref name="id"/>
        /// </summary>
        public Task EditStatusAsync(Guid id, bool newStatus, CancellationToken cancellationToken = default);
    }
}
