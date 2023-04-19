using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ICountyService _countyService;
        private readonly ICityService _cityService;

        public LocationController(
            ICountryService countryService,
            ICountyService countyService,
            ICityService cityService)
        {
            _countryService = countryService;
            _countyService = countyService;
            _cityService = cityService;
        }

        [HttpGet(Routes.Location.GetAll)]
        public async Task<ActionResult<IEnumerable<CountryResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var countries = await _countryService.GetAll();

            return Ok(countries.Select(c => c.ToDto()));
        }

        [HttpGet(Routes.Location.FindCountyById)]
        public async Task<ActionResult<CountyResponse>> FindCountyById(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var county = await _countyService.FindAsync(id, cancellationToken);

            if (county is null)
                return NotFound();

            return Ok(county.ToDto());
        }

        [HttpGet(Routes.Location.FindCountyByName)]
        public async Task<ActionResult<CountyResponse>> FindCountyByName(
            [FromRoute] string name,
            CancellationToken cancellationToken)
        {
            var county = await _countyService.FindByNameAsync(name, cancellationToken);

            if (county is null)
                return NotFound();

            return Ok(county.ToDto());
        }

        [HttpPost(Routes.Location.CreateCity)]
        public async Task<ActionResult<CityResponse>> CreateCity(
            [FromRoute] int id,
            [FromBody] CreateCityRequest city,
            CancellationToken cancellationToken = default)
        {
            if (city is null)
                return BadRequest();

            var county = await _countyService.FindAsync(id, cancellationToken);

            if (county is null)
                return BadRequest();

            var result = await _cityService.CreateAsync(county.Id, city);

            return Created($"/api/counties/{county.Id}/cities/{result.Id}", result);
        }

    }
}
