using AutoMapper;
using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Commands.Services.Edit
{
    internal class EditServiceHandler : IRequestHandler<EditService>
    {
        private IServiceRepository _serviceRepository;
        private IMapper _mapper;

        public EditServiceHandler(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public Task Handle(EditService request, CancellationToken cancellationToken)
        {
            var service = _mapper.Map<Service>(request);
            return _serviceRepository.EditAsync(request.Id, service, cancellationToken);
        }
    }
}
