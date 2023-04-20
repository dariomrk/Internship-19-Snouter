using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            ISubCategoryService subCategoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }

        [HttpPost(Routes.Products.Create)]
        public async Task<ActionResult<ProductResponse>> Create(
            [FromBody] CreateProductRequest request,
            CancellationToken cancellationToken)
        {

            var subCategory = await _subCategoryService.FindAsync(request.SubCategoryId, cancellationToken);

            if (subCategory is null)
                return BadRequest();

            var result = await _productService.CreateAsync(request, cancellationToken);

            return Created($"/api/products/{result.Id}", result);
        }

        [HttpGet(Routes.Products.GetAllFromCategory)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllFromCategory(
            [FromRoute] int categoryId,
            CancellationToken cancellationToken)
        {
            var category = await _categoryService.FindAsync(categoryId, cancellationToken);

            if (category is null)
                return BadRequest();

            var result = await _productService.GetAllFromCategory(categoryId, cancellationToken);

            return Ok(result);
        }

        [HttpGet(Routes.Products.GetAllFromSubCategory)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllFromSubCategory(
            [FromRoute] int categoryId,
            [FromRoute] int subCategoryId,
            CancellationToken cancellationToken)
        {
            var category = await _categoryService.FindAsync(categoryId, cancellationToken);

            if (category is null)
                return BadRequest();

            var subCategory = await _subCategoryService.FindAsync(subCategoryId, cancellationToken);

            if (subCategory is null)
                return BadRequest();

            var result = await _productService.GetAllFromSubCategory(subCategoryId, cancellationToken);

            return Ok(result);
        }
    }
}
