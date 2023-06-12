using AutoMapper;
using MassTransit;
using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;
using SharedEvents.Models;

namespace ServicesAPI.Application.Commands.Specializations.Create
{
    public class CreateSpecializationHandler : IRequestHandler<CreateSpecialization, Specialization>
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public CreateSpecializationHandler(ISpecializationRepository specializationRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _specializationRepository = specializationRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Specialization> Handle(CreateSpecialization request, CancellationToken cancellationToken)
        {
            var specialization = _mapper.Map<Specialization>(request);
            await _specializationRepository.CreateAsync(specialization, cancellationToken);
            await _publishEndpoint.Publish(new SpecializationCreated(specialization.Id, request.Name, request.IsActive), cancellationToken);

            return specialization;
        }
    }
}
