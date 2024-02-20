using Application.Contracts;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public ActionResult<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(_authenticationService.Authenticate(request));
        }
    }
}
