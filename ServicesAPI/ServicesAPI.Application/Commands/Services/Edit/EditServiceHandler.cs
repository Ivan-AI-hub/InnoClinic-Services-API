using AutoMapper;
using MassTransit;
using MediatR;
using ServicesAPI.Application.SharedModels;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Commands.Services.Edit
{
    internal class EditServiceHandler : IRequestHandler<EditService>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICategoryRepository _categoryRepository;

        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public EditServiceHandler(IServiceRepository serviceRepository, ICategoryRepository categoryRepository, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(EditService request, CancellationToken cancellationToken)
        {
            if (!_serviceRepository.IsServiceExist(request.Id))
            {
                throw new ServiceNotFoundException(request.Id);
            }

            var category = await _categoryRepository.GetByNameAsync(request.CategoryName, cancellationToken);
            if (category == null)
            {
                throw new CategoryNotFoundException(request.CategoryName);
            }

            var service = _mapper.Map<Service>(request);
            service.Category = category;
            await _serviceRepository.EditAsync(request.Id, service, cancellationToken);

            await _publishEndpoint.Publish(new ServiceNameUpdatedEvent(request.Id, request.Name));
        }
    }
}
