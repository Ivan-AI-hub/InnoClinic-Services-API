﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesAPI.Application.Commands.Categories.Create;

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
        public async Task<IActionResult> CreateCategory(CreateCategory request, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(request, cancellationToken);
            return Ok();
        }

    }
}
