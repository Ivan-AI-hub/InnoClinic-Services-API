using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Queries.Specializations.GetInfo
{
    internal class GetSpecializationsInfoHandler : IRequestHandler<GetSpecializationsInfo, IEnumerable<Specialization>>
    {
        public ISpecializationRepository _specializationRepository;
        public GetSpecializationsInfoHandler(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }
        public Task<IEnumerable<Specialization>> Handle(GetSpecializationsInfo request, CancellationToken cancellationToken)
        {
            var specializations = _specializationRepository.GetSpecializationsWithoutServices(request.PageSize, request.PageNumber);
            return Task.FromResult(specializations.AsEnumerable());
        }
    }
}
