using Microsoft.AspNetCore.Mvc;
using POCWebAPIJWT.Auth;

namespace POCWebAPIJWT.Controllers
{
    [ServiceFilter(typeof(CustomAuth))]
    [ApiController]
    public class SecuredEndPointsController : ControllerBase
    {
        [HttpGet("api/restricted")]
        public IActionResult RestrictedAreA()
        {
            this.HttpContext.Items.TryGetValue("username", out var username);
            return Ok($"Welcome {username?.ToString()}! You are authorized");
        }
    }
}
