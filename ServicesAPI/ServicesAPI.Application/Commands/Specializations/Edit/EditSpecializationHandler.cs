using AutoMapper;
using MediatR;
using ServicesAPI.Domain;
using ServicesAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAPI.Application.Commands.Specializations.Edit
{
    internal class EditSpecializationHandler : IRequestHandler<EditSpecialization>
    {
        private ISpecializationRepository _specializationRepository;
        private IMapper _mapper;

        public EditSpecializationHandler(ISpecializationRepository specializationRepository, IMapper mapper)
        {
            _specializationRepository = specializationRepository;
            _mapper = mapper;
        }

        public Task Handle(EditSpecialization request, CancellationToken cancellationToken)
        {
            var specialization = _mapper.Map<Specialization>(request);
            specialization.Services.AddRange(request.Services);
            return _specializationRepository.EditAsync(request.id, specialization, cancellationToken);
        }
    }
}
