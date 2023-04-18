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
        public async Task<ActionResult<CreateUserResponse>> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await _userService.CreateAsync(request, cancellationToken);

            return Ok(result);
        }
    }
}
