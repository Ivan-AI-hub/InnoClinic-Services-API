using AutoMapper;
using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Commands.Specializations.Create
{
    internal class CreateSpecializationHandler : IRequestHandler<CreateSpecialization, Specialization>
    {
        private ISpecializationRepository _specializationRepository;
        private IMapper _mapper;

        public CreateSpecializationHandler(ISpecializationRepository specializationRepository, IMapper mapper)
        {
            _specializationRepository = specializationRepository;
            _mapper = mapper;
        }

        public async Task<Specialization> Handle(CreateSpecialization request, CancellationToken cancellationToken)
        {
            var specialization = _mapper.Map<Specialization>(request);
            await _specializationRepository.CreateAsync(specialization, cancellationToken);
            return specialization;
        }
    }
}
