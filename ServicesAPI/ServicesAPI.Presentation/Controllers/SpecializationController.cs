using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.Application.Commands.Specializations.ChangeStatus;
using ServicesAPI.Application.Commands.Specializations.Create;
using ServicesAPI.Application.Commands.Specializations.Edit;
using ServicesAPI.Application.Queries.Specializations.GetById;
using ServicesAPI.Application.Queries.Specializations.GetInfo;

namespace SpecializationsAPI.Presentation.Controllers
{
    [ApiController]
    [Route("Specializations")]
    public class SpecializationController : ControllerBase
    {
        private IMediator _mediator;
        public SpecializationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{PageSize}/{PageNumber}")]
        public async Task<IActionResult> GetSpecializationsInfo(GetSpecializationsInfo request, CancellationToken cancellationToken = default)
        {
            var services = await _mediator.Send(request, cancellationToken);
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(GetSpecializationById request, CancellationToken cancellationToken = default)
        {
            var service = await _mediator.Send(request, cancellationToken);
            return Ok(service);
        }

        [HttpPut("{Id}/status")]
        public async Task<IActionResult> ChangeStatus(ChangeSpecializationStatus request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(request, cancellationToken);
            return Accepted();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditSpecialization(EditSpecialization request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(request, cancellationToken);
            return Accepted();
        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> CreateSpecialization(CreateSpecialization request, CancellationToken cancellationToken = default)
        {
            var service = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction("GetById", service.Id, service);
        }
    }
}
