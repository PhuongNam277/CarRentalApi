using MediatR;
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
            // B1: Tạo yêu cầu
            var query = new GetAllCategoriesQuery();
            // B2: Gửi đi
            var result = await _mediator.Send(query);
            // B3: Trả về kết quả
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var query = new GetCategoryByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCategory([FromBody] CreateCategoryCommand command)
        {
            var categoryId = await _mediator.Send(command);
            if (categoryId == 0) { return BadRequest(); }
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryId }, categoryId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryCommand command)
        {
            if(id != command.Id)
            {
                return BadRequest($"ID trong URL ({id}) phải khớp với ID trong body ({command.Id})");
            }
            var result = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoryAsync([FromBody] DeleteCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            if(!result) { return NotFound(); }
            return NoContent();
        }
    }
}
