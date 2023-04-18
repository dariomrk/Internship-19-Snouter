using Application.Interfaces;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICrudService<User, int> _userService;

        public UserController(ICrudService<User, int> userService)
        {
            _userService = userService;
        }

        [HttpPost(Routes.User.Create)]
        public ActionResult Create([FromBody] User user, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpGet(Routes.User.GetAll)]
        public async Task<ActionResult<ICollection<User>>> GetAll(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAll(cancellationToken);

            return Ok(users);
        }
    }
}
