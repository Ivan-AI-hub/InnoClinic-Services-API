using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.Application.Commands.Services.ChangeStatus;
using ServicesAPI.Application.Commands.Services.Create;
using ServicesAPI.Application.Commands.Services.Edit;
using ServicesAPI.Application.Queries.Services.GetByCategory;
using ServicesAPI.Application.Queries.Services.GetById;
using ServicesAPI.Domain;
using ServicesAPI.Presentation.Models.ErrorModels;

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
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetByCategory([FromRoute] GetActiveServicesByCategory request, CancellationToken cancellationToken = default)
        {
            var services = await _mediator.Send(request, cancellationToken);
            return Ok(services);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Service), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> GetServiceById([FromRoute] GetServiceById request, CancellationToken cancellationToken = default)
        {
            var service = await _mediator.Send(request, cancellationToken);
            return Ok(service);
        }

        [HttpPut("{id}/status")]
        [ProducesResponseType(202)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, bool status, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new ChangeServiceStatus(id, status), cancellationToken);
            return Accepted();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> EditService([FromRoute] Guid id,
                                                     string name,
                                                     int price,
                                                     bool status,
                                                     Guid specializationId,
                                                     string categoryName,
                                                     CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new EditService(id, name, price, status, specializationId, categoryName), cancellationToken);
            return Accepted();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Service), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> CreateService(CreateService request, CancellationToken cancellationToken = default)
        {
            var service = await _mediator.Send(request, cancellationToken);
            return Ok(service);
        }
    }
}
