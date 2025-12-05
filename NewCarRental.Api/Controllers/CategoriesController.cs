using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewCarRental.Application.Features.Categories.Queries.GetAllCategories;

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

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetCategoryById(int id)
        //{
        //    var result = await _categoryService.GetCategoryByIdAsync(id);
        //    if (result == null)
        //    {
        //        _logger.LogWarning("Category với ID {CategoryID} không tìm thấy", id);
        //        return NotFound();
        //    }
        //    return Ok(result);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddNewCategoryAsync(CategoryCreateDto categoryCreateDto)
        //{
        //    var result = await _categoryService.AddCategoryAsync(categoryCreateDto);
        //    if (result == null) return BadRequest();
        //    return CreatedAtAction(nameof(GetCategoryById), new { id = result.CategoryId }, result);
        //}
    }
}
