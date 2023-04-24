using Api.Constants;
using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(AuthConstants.UserPolicyName)]
        [HttpPost(Routes.Products.Create)]
        public async Task<ActionResult<ProductResponse>> Create(
            [FromBody] CreateProductRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _productService.CreateAsync(request, cancellationToken);

            return Created($"/api/products/{result.Id}", result);
        }

        [HttpGet(Routes.Products.GetAllFromCategory)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllFromCategory(
            [FromRoute] int categoryId,
            CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllFromCategory(categoryId, cancellationToken);

            return Ok(result);
        }

        [HttpGet(Routes.Products.GetAllFromSubCategory)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllFromSubCategory(
            [FromRoute] int categoryId,
            [FromRoute] int subCategoryId,
            CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllFromSubCategory(subCategoryId, cancellationToken);

            return Ok(result);
        }

        [HttpGet(Routes.Products.FindById)]
        public async Task<ActionResult<ProductResponse>> FindById(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var product = await _productService.FindById(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [Authorize(AuthConstants.UserPolicyName)]
        [HttpPatch(Routes.Products.UpdateAvailability)]
        public async Task<ActionResult> UpdateAvailability(
            [FromRoute] int id,
            [FromBody] UpdateProductAvailabilityRequest request,
            CancellationToken cancellationToken)
        {
            await _productService.UpdateAvailability(
                id,
                Enum.Parse<ProductAvailability>(request.ProductAvailability, true),
                cancellationToken);

            return Ok();
        }

        [Authorize(AuthConstants.UserPolicyName)]
        [HttpPatch(Routes.Products.Renew)]
        public async Task<ActionResult> Renew(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _productService.Renew(id, cancellationToken);

            return Ok();
        }

        [Authorize(AuthConstants.UserPolicyName)]
        [HttpDelete(Routes.Products.Delete)]
        public async Task<ActionResult> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _productService.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
