using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpPost(Routes.Country.Create)]
        public async Task<ActionResult<CreateCountryResponse>> CreateAsync(
            CreateCountryRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await _countryService.CreateAsync(request, cancellationToken);

            return Ok(result);
        }
    }
}
