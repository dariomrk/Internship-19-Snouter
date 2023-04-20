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

        [HttpPost(Routes.Users.Create)]
        public async Task<ActionResult<UserResponse>> CreateAsync(
            [FromBody] UserRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _userService.CreateAsync(request, cancellationToken);

            return Created($"/api/users/{result.Id}", result);
        }

        [HttpGet(Routes.Users.GetAll)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllAsync()
        {
            var result = await _userService.GetAll();

            return Ok(result.Select(u => u.ToDto()));
        }

        [HttpGet(Routes.Users.Find)]
        public async Task<ActionResult<UserResponse>> FindAsync(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var result = await _userService.FindAsync(id, cancellationToken);

            if (result is null)
                return NotFound();

            return Ok(result.ToDto());
        }

        [HttpPut(Routes.Users.Update)]
        public async Task<ActionResult<UserResponse>> UpdateAsync(
            [FromRoute] int id,
            [FromBody] UserRequest request,
            CancellationToken cancellationToken)
        {
            var response = await _userService.UpdateAsync(id, request, cancellationToken);

            return Ok(response);
        }

        [HttpDelete(Routes.Users.Delete)]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _userService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
