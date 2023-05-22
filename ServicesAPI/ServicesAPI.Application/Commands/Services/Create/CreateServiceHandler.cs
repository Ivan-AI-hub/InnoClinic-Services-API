using AutoMapper;
using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Commands.Services.Create
{
    internal class CreateServiceHandler : IRequestHandler<CreateService, Service>
    {
        private IServiceRepository _serviceRepository;
        private IMapper _mapper;
        public CreateServiceHandler(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<Service> Handle(CreateService request, CancellationToken cancellationToken)
        {
            var service = _mapper.Map<Service>(request);
            await _serviceRepository.CreateAsync(service, cancellationToken);
            return service;
        }
    }
}
