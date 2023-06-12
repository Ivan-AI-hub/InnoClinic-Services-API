using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Queries.Services.GetByCategory
{
    public class GetActiveServicesByCategoryHandler : IRequestHandler<GetActiveServicesByCategory, IEnumerable<Service>>
    {
        private readonly IServiceRepository _serviceRepository;
        public GetActiveServicesByCategoryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<IEnumerable<Service>> Handle(GetActiveServicesByCategory request, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.GetActiveServicesByCategoryAsync(request.PageSize, request.PageNumber, request.ServiceCategoryName);
            return services;
        }
    }
}
