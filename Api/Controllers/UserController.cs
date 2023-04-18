using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(Routes.User.Create)]
        public async Task<ActionResult<CreateUserResponse>> CreateAsync(
            [FromBody] CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _userService.CreateAsync(request, cancellationToken);

            return Ok(result);
        }

        [HttpGet(Routes.User.Find)]
        public async Task<ActionResult<UserResponse>> FindAsync(int id)
        {
            var result = await _userService.FindAsync(id);

            if (result is null)
                return NotFound();

            return Ok(result.ToUserResponse());
        }
    }
}
