using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    namespace Api.Controllers
    {
        [ApiController]
        public class SubCategoryController : ControllerBase
        {
            private readonly ICategoryService _categoryService;
            private readonly ISubCategoryService _subCategoryService;

            public SubCategoryController(ICategoryService categoryService, ISubCategoryService subCategoryservice)
            {
                _categoryService = categoryService;
                _subCategoryService = subCategoryservice;
            }

            [HttpPost(Routes.SubCategories.CreateSubCategory)]
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

            [HttpGet(Routes.SubCategories.GetSubCategories)]
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

            [HttpPatch(Routes.SubCategories.UpdateSubCategoryName)]
            public async Task<ActionResult> UpdateSubCategoryName(
                [FromBody] UpdateNameRequest request,
                [FromRoute] int subCategoryId,
                CancellationToken cancellationToken = default)
            {
                var subCategory = await _subCategoryService.FindAsync(subCategoryId, cancellationToken);

                if (subCategory is null)
                    return BadRequest();

                subCategory.Name = request.Name;

                await _subCategoryService.UpdateAsync(subCategory, cancellationToken);

                return Accepted();
            }

            [HttpDelete(Routes.SubCategories.DeleteSubCategory)]
            public async Task<ActionResult> DeleteSubCategory(
                [FromRoute] int subCategoryId,
                CancellationToken cancellationToken = default)
            {
                var subCategory = await _subCategoryService.FindAsync(subCategoryId, cancellationToken);

                if (subCategory is null)
                    return BadRequest();

                await _subCategoryService.DeleteAsync(subCategoryId, cancellationToken);

                return Accepted();
            }
        }
    }
}
