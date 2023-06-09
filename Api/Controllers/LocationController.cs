﻿using Application.Interfaces;
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

        [HttpPost(Routes.Locations.CreateCity)]
        public async Task<ActionResult<CityResponse>> CreateCity(
            [FromRoute] int countyId,
            [FromBody] CreateCityRequest city,
            CancellationToken cancellationToken = default)
        {
            var result = await _cityService.CreateAsync(countyId, city);

            return Created($"/api/counties/{countyId}/cities/{result.Id}", result);
        }

        [HttpGet(Routes.Locations.GetAll)]
        public async Task<ActionResult<IEnumerable<CountryResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var countries = await _countryService.GetAll();

            return Ok(countries.Select(c => c.ToDto()));
        }

        [HttpGet(Routes.Locations.FindCountyById)]
        public async Task<ActionResult<CountyResponse>> FindCountyById(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var county = await _countyService.FindAsync(id, cancellationToken);

            if (county is null)
                return NotFound();

            return Ok(county.ToDto());
        }

        [HttpGet(Routes.Locations.FindCountyByName)]
        public async Task<ActionResult<CountyResponse>> FindCountyByName(
            [FromRoute] string name,
            CancellationToken cancellationToken)
        {
            var county = await _countyService.FindByNameAsync(name, cancellationToken);

            if (county is null)
                return NotFound();

            return Ok(county.ToDto());
        }

        [HttpGet(Routes.Locations.FindCityById)]
        public async Task<ActionResult<CityResponse>> FindCityById(
            [FromRoute] int countyId,
            [FromRoute] int cityId,
            CancellationToken cancellationToken)
        {
            var county = await _countyService.FindAsync(countyId, cancellationToken);

            if (county is null)
                return NotFound();

            var city = county.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
                return NotFound();

            return Ok(city.ToDto());
        }

        [HttpPatch(Routes.Locations.UpdateCityName)]
        public async Task<ActionResult> UpdateCityName(
            [FromRoute] int countyId,
            [FromRoute] int cityId,
            [FromBody] UpdateNameRequest request,
            CancellationToken cancellationToken = default)
        {
            var county = await _countyService.FindAsync(countyId, cancellationToken);

            if (county is null)
                return NotFound();

            var city = county.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
                return BadRequest();

            await _cityService.UpdateNameAsync(county.Id, city.Id, request.Name, cancellationToken);

            return Accepted();
        }

        [HttpDelete(Routes.Locations.DeleteCity)]
        public async Task<ActionResult> DeleteCity(
            [FromRoute] int countyId,
            [FromRoute] int cityId,
            CancellationToken cancellationToken = default)
        {
            var county = await _countyService.FindAsync(countyId, cancellationToken);

            if (county is null)
                return BadRequest();

            var city = county.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
                return BadRequest();

            await _cityService.DeleteAsync(cityId, cancellationToken);

            return NoContent();
        }
    }
}
