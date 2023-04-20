using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryservice _subCategoryService;

        public CategoryController(ICategoryService categoryService, ISubCategoryservice subCategoryservice)
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryservice;
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

        [HttpPost(Routes.Categories.CreateSubCategory)]
        public async Task<ActionResult<SubCategoryResponse>> CreateSubCategory(
            [FromRoute] int categoryId,
            [FromBody] CreateSubCategoryRequest newSubCategory,
            CancellationToken cancellationToken)
        {
            if (newSubCategory is null)
                return BadRequest();

            var result = await _subCategoryService.CreateAsync(categoryId, newSubCategory, cancellationToken);

            return Created($"api/categories/{categoryId}/sub-categories/{result.Id}/products", result);
        }

        [HttpGet(Routes.Categories.GetAllCategories)]
        public async Task<ActionResult<ICollection<CategoryResponse>>> GetAllCategories()
        {
            var result = await _categoryService.GetAll();

            return Ok(result.Select(c => c.ToDto()));
        }

        [HttpGet(Routes.Categories.GetSubCategories)]
        public async Task<ActionResult<IEnumerable<SubCategoryResponse>>> GetSubCategories(
            [FromRoute] int categoryId,
            CancellationToken cancellationToken)
        {
            var category = await _categoryService.FindAsync(categoryId, cancellationToken);

            if (category is null)
                return BadRequest();

            var result = await _subCategoryService
                .Query()
                .Where(sc => sc.CategoryId == categoryId)
                .ToListAsync();

            return Ok(result.Select(sc => sc.ToDto()));
        }

        [HttpPatch(Routes.Categories.UpdateCategoryName)]
        public async Task<ActionResult> UpdateCategoryName(
            [FromBody] string newName,
            [FromRoute] int categoryId,
            CancellationToken cancellationToken = default)
        {
            var category = await _categoryService.FindAsync(categoryId, cancellationToken);

            if (category is null)
                return BadRequest();

            category.Name = newName;

            await _categoryService.UpdateAsync(category, cancellationToken);

            return Accepted();
        }

        [HttpPatch(Routes.Categories.UpdateSubCategoryName)]
        public async Task<ActionResult> UpdateSubCategoryName(
            [FromBody] string newName,
            [FromRoute] int subCategoryId,
            CancellationToken cancellationToken = default)
        {
            var subCategory = await _subCategoryService.FindAsync(subCategoryId, cancellationToken);

            if (subCategory is null)
                return BadRequest();

            subCategory.Name = newName;

            await _subCategoryService.UpdateAsync(subCategory, cancellationToken);

            return Accepted();
        }
    }
}
