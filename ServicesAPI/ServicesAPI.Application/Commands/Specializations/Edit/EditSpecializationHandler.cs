using AutoMapper;
using MassTransit;
using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Exceptions;
using ServicesAPI.Domain.Interfaces;
using SharedEvents.Models;

namespace ServicesAPI.Application.Commands.Specializations.Edit
{
    internal class EditSpecializationHandler : IRequestHandler<EditSpecialization>
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public EditSpecializationHandler(ISpecializationRepository specializationRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _specializationRepository = specializationRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(EditSpecialization request, CancellationToken cancellationToken)
        {
            if (!_specializationRepository.IsSpecializationExist(request.Id))
            {
                throw new SpecializationNotFoundException(request.Id);
            }
            var specialization = _mapper.Map<Specialization>(request);
            await _specializationRepository.EditAsync(request.Id, specialization, cancellationToken);
            await _publishEndpoint.Publish(new SpecializationUpdated(request.Id, request.Name, request.IsActive));
        }
    }
}
