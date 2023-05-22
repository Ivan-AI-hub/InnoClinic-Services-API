using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Queries.Services.GetById
{
    public class GetServiceByIdHandler : IRequestHandler<GetServiceById, Service>
    {
        private IServiceRepository _serviceRepository;
        public GetServiceByIdHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<Service> Handle(GetServiceById request, CancellationToken cancellationToken)
        {
            var service = await  _serviceRepository.GetByIdAsync(request.id, cancellationToken);
            if (service == null)
            {
                throw new ServiceNotFoundException(request.id);
            }

            return service;
        }
    }
}
