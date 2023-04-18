using Application.Interfaces;
using Common.Dtos;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICityService _cityService;

        public UserController(IUserService userService, ICityService cityService)
        {
            _userService = userService;
            _cityService = cityService;
        }

        [HttpPost(Routes.User.Create)]
        public async Task<ActionResult<int>> Create([FromBody] UserDto user, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                City = await _cityService.FindOrCreateAsync(user.CityName, user.CountyName, user.CountryName),
                Phone = user.Phone,
                PreciseLocation = user.Latitude.HasValue && user.Longitude.HasValue
                    ? new PreciseLocation
                    {
                        Latitude = user.Latitude!.Value,
                        Longitude = user.Longitude!.Value,
                        LocationType = LocationType.UserLocation,
                    }
                    : null,
            };

            var creationResult = await _userService.CreateAsync(newUser, cancellationToken);

            return Ok(creationResult);
        }

        [HttpGet(Routes.User.GetAll)]
        public async Task<ActionResult<ICollection<User>>> GetAll(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAll(cancellationToken);

            return Ok(users);
        }
    }
}
