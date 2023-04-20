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
        private readonly ISubCategoryservice _subCategoryservice;

        public CategoryController(ICategoryService categoryService, ISubCategoryservice subCategoryservice)
        {
            _categoryService = categoryService;
            _subCategoryservice = subCategoryservice;
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

            var result = await _subCategoryservice.CreateAsync(categoryId, newSubCategory, cancellationToken);

            return Created($"api/categories/{categoryId}/sub-categories/{result.Id}/products", result);
        }

        [HttpGet(Routes.Categories.GetAllCategories)]
        public async Task<ActionResult<ICollection<CategoryResponse>>> GetAllCategories()
        {
            var result = await _categoryService.GetAll();

            return Ok(result.Select(c => c.ToDto()));
        }

    }
}
