using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.Application.Commands.Specializations.ChangeStatus;
using ServicesAPI.Application.Commands.Specializations.Create;
using ServicesAPI.Application.Commands.Specializations.Edit;
using ServicesAPI.Application.Queries.Specializations.GetById;
using ServicesAPI.Application.Queries.Specializations.GetInfo;
using ServicesAPI.Domain;
using ServicesAPI.Presentation.Models.ErrorModels;
using ServicesAPI.Presentation.Models.RequestModels;

namespace ServicesAPI.Presentation.Controllers
{
    [ApiController]
    [Route("specializations")]
    public class SpecializationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SpecializationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{PageSize}/{PageNumber}")]
        [ProducesResponseType(typeof(IEnumerable<Specialization>), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetSpecializationsInfo([FromRoute] GetSpecializationsInfo request, CancellationToken cancellationToken = default)
        {
            var specializations = await _mediator.Send(request, cancellationToken);
            return Ok(specializations);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Specialization), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetById([FromRoute] GetSpecializationById request, CancellationToken cancellationToken = default)
        {
            var specialization = await _mediator.Send(request, cancellationToken);
            return Ok(specialization);
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromBody] ChangeSpecializationStatusRequestModel model, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new ChangeSpecializationStatus(id, model.IsActive), cancellationToken);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> EditSpecialization([FromRoute] Guid id, [FromBody] EditSpecializationRequestModel model, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new EditSpecialization(id, model.Name, model.IsActive), cancellationToken);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Specialization), 201)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> CreateSpecialization(CreateSpecialization request, CancellationToken cancellationToken = default)
        {
            var specialization = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { Id = specialization.Id }, specialization);
        }
    }
}
