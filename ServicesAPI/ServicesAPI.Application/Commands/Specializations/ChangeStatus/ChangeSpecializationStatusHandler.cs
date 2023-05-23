using MediatR;
using ServicesAPI.Domain.Exceptions;
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
            if (!_specializationRepository.IsSpecializationExist(request.Id))
            {
                throw new SpecializationNotFoundException(request.Id);
            }
            return _specializationRepository.EditStatusAsync(request.Id, request.IsActive, cancellationToken);
        }
    }
}
