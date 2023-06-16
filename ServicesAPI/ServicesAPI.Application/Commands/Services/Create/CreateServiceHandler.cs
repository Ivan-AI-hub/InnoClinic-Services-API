using AutoMapper;
using MassTransit;
using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;
using SharedEvents.Models;

namespace ServicesAPI.Application.Commands.Services.Create
{
    public class CreateServiceHandler : IRequestHandler<CreateService, Service>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        public CreateServiceHandler(IServiceRepository serviceRepository, ICategoryRepository categoryRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _serviceRepository = serviceRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Service> Handle(CreateService request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByNameAsync(request.CategoryName, cancellationToken);
            if (category == null)
            {
                throw new CategoryNotFoundException(request.CategoryName);
            }
            var service = _mapper.Map<Service>(request);
            service.Category = category;
            await _serviceRepository.CreateAsync(service, cancellationToken);

            await _publishEndpoint.Publish(new ServiceCreated(service.Id, service.Name, service.Price.ToString()), cancellationToken);
            return service;
        }
    }
}
