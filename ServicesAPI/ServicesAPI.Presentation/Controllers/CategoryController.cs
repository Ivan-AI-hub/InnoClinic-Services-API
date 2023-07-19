using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.Application.Commands.Categories.Create;
using ServicesAPI.Presentation.Models.ErrorModels;

namespace ServicesAPI.Presentation.Controllers
{
    [ApiController]
    [Route("categories/")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 500)]
        public async Task<IActionResult> CreateCategory(CreateCategory request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(request, cancellationToken);
            return NoContent();
        }

    }
}
