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

        [HttpGet]
        [Route(Endpoints.User.GetAll)]
        public ActionResult GetAll()
        {
            return Ok();
        }
    }
}
