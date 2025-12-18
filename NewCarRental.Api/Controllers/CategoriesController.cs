using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewCarRental.Application.Features.Categories.Commands.CreateCategory;
using NewCarRental.Application.Features.Categories.Commands.DeleteCategory;
using NewCarRental.Application.Features.Categories.Commands.UpdateCategory;
using NewCarRental.Application.Features.Categories.Queries.GetAllCategories;
using NewCarRental.Application.Features.Categories.Queries.GetCategoryById;

namespace NewCarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(IMediator mediator, ILogger<CategoriesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var query = new GetAllCategoriesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var query = new GetCategoryByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCategory([FromBody] CreateCategoryCommand command)
        {
            var categoryId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryId }, categoryId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryAsync([FromBody] DeleteCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return NoContent();
        }
    }
}
