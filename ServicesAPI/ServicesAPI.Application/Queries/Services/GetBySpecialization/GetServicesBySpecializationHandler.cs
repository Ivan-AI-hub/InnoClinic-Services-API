using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Queries.Services.GetBySpecialization
{
    public class GetServicesBySpecializationHandler : IRequestHandler<GetServicesBySpecialization, IEnumerable<Service>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ISpecializationRepository _specializationRepository;
        public GetServicesBySpecializationHandler(IServiceRepository serviceRepository, ISpecializationRepository specializationRepository)
        {
            _serviceRepository = serviceRepository;
            _specializationRepository = specializationRepository;
        }
        public async Task<IEnumerable<Service>> Handle(GetServicesBySpecialization request, CancellationToken cancellationToken)
        {
            var specialization = await _specializationRepository.GetByNameAsync(request.SpecializationName, cancellationToken);
            if (specialization == null)
            {
                throw new SpecializationNotFoundException(request.SpecializationName);
            }
            var services = await _serviceRepository.GetServicesBySpecializationIdAsync(specialization.Id, cancellationToken);

            return services;
        }
    }
}
