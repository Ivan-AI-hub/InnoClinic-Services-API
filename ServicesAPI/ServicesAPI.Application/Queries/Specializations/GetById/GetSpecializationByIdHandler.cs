using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Queries.Specializations.GetById
{
    public class GetSpecializationByIdHandler : IRequestHandler<GetSpecializationById, Specialization>
    {
        public ISpecializationRepository _specializationRepository;
        public IServiceRepository _serviceRepository;
        public GetSpecializationByIdHandler(ISpecializationRepository specializationRepository, IServiceRepository serviceRepository)
        {
            _specializationRepository = specializationRepository;
            _serviceRepository = serviceRepository;
        }
        public async Task<Specialization> Handle(GetSpecializationById request, CancellationToken cancellationToken)
        {
            var specialization = await _specializationRepository.GetByIdAsync(request.Id);
            if (specialization == null)
            {
                throw new SpecializationNotFoundException(request.Id);
            }
            specialization.Services.AddRange(await _serviceRepository.GetServicesBySpecializationIdAsync(request.Id));
            return specialization;
        }
    }
}
