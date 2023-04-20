using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost(Routes.Categories.CreateCategory)]
        public async Task<ActionResult<CategoryResponse>> CreateCategory(
            [FromBody] CreateCategoryRequest newCategory,
            CancellationToken cancellationToken)
        {
            if (newCategory is null)
                return BadRequest();

            var result = await _categoryService.CreateAsync(newCategory, cancellationToken);

            return Created($"api/categories/{result.Id}/sub-categories", result);
        }

        [HttpGet(Routes.Categories.GetAllCategories)]
        public async Task<ActionResult<ICollection<CategoryResponse>>> GetAllCategories()
        {
            var result = await _categoryService.GetAll();

            return Ok(result.Select(c => c.ToDto()));
        }

        [HttpPatch(Routes.Categories.UpdateCategoryName)]
        public async Task<ActionResult> UpdateCategoryName(
            [FromBody] UpdateNameRequest request,
            [FromRoute] int categoryId,
            CancellationToken cancellationToken = default)
        {
            var category = await _categoryService.FindAsync(categoryId, cancellationToken);

            if (category is null)
                return BadRequest();

            category.Name = request.Name;

            await _categoryService.UpdateAsync(category, cancellationToken);

            return Accepted();
        }

        [HttpDelete(Routes.Categories.DeleteCategory)]
        public async Task<ActionResult> DeleteCategory(
            [FromRoute] int categoryId,
            CancellationToken cancellationToken = default)
        {
            var category = await _categoryService.FindAsync(categoryId, cancellationToken);

            if (category is null)
                return BadRequest();

            await _categoryService.DeleteAsync(categoryId, cancellationToken);

            return Accepted();
        }
    }
}
