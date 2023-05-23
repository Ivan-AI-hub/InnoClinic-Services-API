using AutoMapper;
using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Commands.Services.Edit
{
    internal class EditServiceHandler : IRequestHandler<EditService>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public EditServiceHandler(IServiceRepository serviceRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _serviceRepository = serviceRepository;
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
        }
    }
}
