using MediatR;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Commands.Services.ChangeStatus
{
    internal class ChangeServiceStatusHandler : IRequestHandler<ChangeServiceStatus>
    {
        private readonly IServiceRepository _serviceRepository;

        public ChangeServiceStatusHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public Task Handle(ChangeServiceStatus request, CancellationToken cancellationToken)
        {
            if (!_serviceRepository.IsServiceExist(request.Id))
            {
                throw new ServiceNotFoundException(request.Id);
            }
            return _serviceRepository.EditStatusAsync(request.Id, request.Status, cancellationToken);
        }
    }
}
