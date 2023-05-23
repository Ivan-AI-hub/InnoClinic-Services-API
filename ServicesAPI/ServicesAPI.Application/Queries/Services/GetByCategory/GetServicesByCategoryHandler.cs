using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Queries.Services.GetByCategory
{
    public class GetServicesByCategoryHandler : IRequestHandler<GetServicesByCategory, IEnumerable<Service>>
    {
        private readonly IServiceRepository _serviceRepository;
        public GetServicesByCategoryHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public Task<IEnumerable<Service>> Handle(GetServicesByCategory request, CancellationToken cancellationToken)
        {
            var services = _serviceRepository.GetActiveServicesByCategories(request.PageSize, request.PageNumber, request.ServiceCategoryName);
            return Task.FromResult(services.AsEnumerable());
        }
    }
}
