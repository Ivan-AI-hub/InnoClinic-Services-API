using MediatR;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Commands.Specializations.ChangeStatus
{
    public class ChangeSpecializationStatusHandler : IRequestHandler<ChangeSpecializationStatus>
    {
        private readonly ISpecializationRepository _specializationRepository;

        public ChangeSpecializationStatusHandler(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }

        public Task Handle(ChangeSpecializationStatus request, CancellationToken cancellationToken)
        {
            return _specializationRepository.EditStatusAsync(request.Id, request.IsActive, cancellationToken);
        }
    }
}
