using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.Application.Commands.Services.ChangeStatus;
using ServicesAPI.Application.Commands.Services.Create;
using ServicesAPI.Application.Commands.Services.Edit;
using ServicesAPI.Application.Queries.Services.GetByCategory;
using ServicesAPI.Application.Queries.Services.GetById;

namespace ServicesAPI.Presentation.Controllers
{
    [ApiController]
    [Route("services/")]
    public class ServiceController : ControllerBase
    {
        private IMediator _mediator;
        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{ServiceCategoryName}/{PageSize}/{PageNumber}")]
        public async Task<IActionResult> GetByCategory([FromRoute] GetServicesByCategory request, CancellationToken cancellationToken = default)
        {
            var services = await _mediator.Send(request, cancellationToken);
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceInfo([FromRoute] GetServiceById request, CancellationToken cancellationToken = default)
        {
            var service = await _mediator.Send(request, cancellationToken);
            return Ok(service);
        }

        [HttpPut("{Id}/status")]
        public async Task<IActionResult> ChangeStatus(ChangeServiceStatus request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(request, cancellationToken);
            return Accepted();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> EditService(EditService request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(request, cancellationToken);
            return Accepted();
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(CreateService request, CancellationToken cancellationToken = default)
        {
            var service = await _mediator.Send(request, cancellationToken);
            return Ok(service);
        }
    }
}
