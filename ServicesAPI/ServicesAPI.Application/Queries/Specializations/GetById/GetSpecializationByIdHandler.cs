using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Queries.Specializations.GetById
{
    internal class GetSpecializationByIdHandler : IRequestHandler<GetSpecializationById, Specialization>
    {
        public ISpecializationRepository _specializationRepository;
        public GetSpecializationByIdHandler(ISpecializationRepository specializationRepository)
        {
            _specializationRepository = specializationRepository;
        }
        public async Task<Specialization> Handle(GetSpecializationById request, CancellationToken cancellationToken)
        {
            var specialization = await _specializationRepository.GetByIdAsync(request.Id);
            if (specialization == null)
            {
                throw new SpecializationNotFoundException(request.Id);
            }

            return specialization;
        }
    }
}
