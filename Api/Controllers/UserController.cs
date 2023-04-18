using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route(Endpoints.User.GetAll)]
        public ActionResult GetAll()
        {
            return Ok();
        }
    }
}
