using System;
using System.Threading.Tasks;
using AuthentificationBase;
using AuthentificationBase.Model;
using AuthentificationService.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AuthentificationService.Contract;
using System.Linq;

namespace AuthentificationService.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppSecurity _appSecurity;

        public AuthService(UserManager<User> userManager,
            AppSecurity appSecurity)
        {
            _userManager = userManager;
            _appSecurity = appSecurity;
        }

        public async Task<ObjectResult> SignUp(SignUpModel signUpModel)
        {
            var userExists = await _userManager.FindByNameAsync(signUpModel.Username);

            if (userExists != null)
            {
                return new ConflictObjectResult(new SignUpResponse
                {
                    Status = "Error",
                    Message = "User exists"
                });
            }

            var user = new User
            {
                Email = signUpModel.Email,
                UserName = signUpModel.Username,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, signUpModel.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new SignUpResponse
                {
                    Status = "Error",
                    Message = result.Errors.FirstOrDefault().Description
                });
            }

            return new OkObjectResult(new SignUpResponse
            {
                Status = "Success",
                Message = "User created successfully!"
            });
        }

        public async Task<ObjectResult> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                return new UnauthorizedObjectResult(new LoginResponse
                {
                    Username = loginModel.Username
                });
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var token = _appSecurity.GetToken(user.Id, DateTime.Now, userRoles);

            return new OkObjectResult(new LoginResponse
            {
                Username = loginModel.Username,
                Token = token
            });
        }
    }
}