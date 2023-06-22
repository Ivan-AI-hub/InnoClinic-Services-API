using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.Application.Commands.Services.ChangeStatus;
using ServicesAPI.Application.Commands.Services.Create;
using ServicesAPI.Application.Commands.Services.Edit;
using ServicesAPI.Application.Queries.Services.GetByCategory;
using ServicesAPI.Application.Queries.Services.GetById;
using ServicesAPI.Domain;
using ServicesAPI.Presentation.Models.ErrorModels;
using ServicesAPI.Presentation.Models.RequestModels;

namespace ServicesAPI.Presentation.Controllers
{
    [ApiController]
    [Route("services/")]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{ServiceCategoryName}/{PageSize}/{PageNumber}")]
        [ProducesResponseType(typeof(IEnumerable<Service>), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetByCategory([FromRoute] GetActiveServicesByCategory request, CancellationToken cancellationToken = default)
        {
            var services = await _mediator.Send(request, cancellationToken);
            return Ok(services);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Service), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetServiceById([FromRoute] GetServiceById request, CancellationToken cancellationToken = default)
        {
            var service = await _mediator.Send(request, cancellationToken);
            return Ok(service);
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromBody] ChangeServiceStatusRequestModel model, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new ChangeServiceStatus(id, model.Status), cancellationToken);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> EditService([FromRoute] Guid id,
                                                     [FromBody] EditServiceRequestModel model,
                                                     CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new EditService(id, model.Name, model.Price, model.Status, model.SpecializationId, model.CategoryName), cancellationToken);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Service), 201)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> CreateService(CreateService request, CancellationToken cancellationToken = default)
        {
            var service = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(GetServiceById), new { Id = service.Id}, service);
        }
    }
}
