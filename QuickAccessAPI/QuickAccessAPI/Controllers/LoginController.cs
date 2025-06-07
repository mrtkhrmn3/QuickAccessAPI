using Microsoft.AspNetCore.Mvc;
using QuickAccessAPI.Dto;
using QuickAccessAPI.Interfaces.IServices;

namespace QuickAccessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var response = await _loginService.LoginAsync(loginRequest);
            if (response == null)
                return Unauthorized("Invalid username or password.");

            return Ok(response);
        }
    }
}
