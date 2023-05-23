using AutoMapper;
using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Commands.Services.Create
{
    internal class CreateServiceHandler : IRequestHandler<CreateService, Service>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CreateServiceHandler(IServiceRepository serviceRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
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
            return service;
        }
    }
}
