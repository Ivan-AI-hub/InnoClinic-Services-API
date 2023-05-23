using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Queries.Services.GetById
{
    public class GetServiceByIdHandler : IRequestHandler<GetServiceById, Service>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISpecializationRepository _specializationRepository;
        public GetServiceByIdHandler(IServiceRepository serviceRepository, ICategoryRepository categoryRepository, ISpecializationRepository specializationRepository)
        {
            _serviceRepository = serviceRepository;
            _categoryRepository = categoryRepository;
            _specializationRepository = specializationRepository;
        }
        public async Task<Service> Handle(GetServiceById request, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByIdAsync(request.Id, cancellationToken);
            if (service == null)
            {
                throw new ServiceNotFoundException(request.Id);
            }

            service.Specialization = await _specializationRepository.GetByIdAsync(service.SpecializationId, cancellationToken);
            service.Category = await _categoryRepository.GetByIdAsync(service.CategoryId, cancellationToken);
            return service;
        }
    }
}
