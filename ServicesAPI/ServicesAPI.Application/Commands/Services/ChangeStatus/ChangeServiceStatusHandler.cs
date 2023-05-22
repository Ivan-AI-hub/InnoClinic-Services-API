using MediatR;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Commands.Services.ChangeStatus
{
    internal class ChangeServiceStatusHandler : IRequestHandler<ChangeServiceStatus>
    {
        private IServiceRepository _serviceRepository;

        public ChangeServiceStatusHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public Task Handle(ChangeServiceStatus request, CancellationToken cancellationToken)
        {
            return _serviceRepository.EditStatusAsync(request.Id, request.Status, cancellationToken);
        }
    }
}
