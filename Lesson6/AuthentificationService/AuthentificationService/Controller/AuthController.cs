using System;
using System.Threading.Tasks;
using AuthentificationService.Model;
using Microsoft.AspNetCore.Mvc;
using AuthentificationService.Contract;

namespace AuthentificationService.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromForm] SignUpModel signUpModel)
        {
            return await _authService.SignUp(signUpModel);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            return await _authService.Login(loginModel);
        }
    }
}