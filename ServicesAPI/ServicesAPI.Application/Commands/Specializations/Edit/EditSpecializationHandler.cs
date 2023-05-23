using AutoMapper;
using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;

namespace ServicesAPI.Application.Commands.Specializations.Edit
{
    internal class EditSpecializationHandler : IRequestHandler<EditSpecialization>
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IMapper _mapper;

        public EditSpecializationHandler(ISpecializationRepository specializationRepository, IMapper mapper)
        {
            _specializationRepository = specializationRepository;
            _mapper = mapper;
        }

        public Task Handle(EditSpecialization request, CancellationToken cancellationToken)
        {
            var specialization = _mapper.Map<Specialization>(request);
            return _specializationRepository.EditAsync(request.Id, specialization, cancellationToken);
        }
    }
}
