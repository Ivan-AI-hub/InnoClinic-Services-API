using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Queries.Specializations.GetInfo
{
    public class GetSpecializationsInfoHandler : IRequestHandler<GetSpecializationsInfo, IEnumerable<Specialization>>
    {
        public ISpecializationRepository _specializationRepository;
        public GetSpecializationsInfoHandler(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }
        public async Task<IEnumerable<Specialization>> Handle(GetSpecializationsInfo request, CancellationToken cancellationToken)
        {
            var specializations = await _specializationRepository.GetSpecializationsWithoutServicesAsync(request.PageSize, request.PageNumber, cancellationToken);
            return specializations;
        }
    }
}
