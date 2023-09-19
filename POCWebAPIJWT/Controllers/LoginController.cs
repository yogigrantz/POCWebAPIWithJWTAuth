using Microsoft.AspNetCore.Mvc;
using POCWebAPIJWT.Auth;

namespace POCWebAPIJWT.Controllers
{

    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuth _auth;

        public LoginController(IAuth auth)
        {
            this._auth = auth;
        }

        [HttpGet("api/login")]
        public IActionResult Get()
        {
            return Ok("Please log in");
        }

        [HttpPost("api/login")]
        public IActionResult Post(UserDTO user)
        {
            string? authRslt = _auth.Authenticate(user);
            if (authRslt != null)
            {
                return Ok(authRslt);
            }
            else
                return Unauthorized();
        }

        [HttpGet("api/UnAuthorized")]
        public IActionResult UnAuthorized()
        {
            return Unauthorized();
        }

        [HttpGet("api/Error")]
        public IActionResult Error()
        {
            return BadRequest();
        }

    }
}
