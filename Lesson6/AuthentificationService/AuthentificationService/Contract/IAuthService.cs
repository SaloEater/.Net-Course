using System.Threading.Tasks;
using AuthentificationService.Model;
using Microsoft.AspNetCore.Mvc;

namespace AuthentificationService.Contract
{
    public interface IAuthService
    {
        Task<ObjectResult> SignUp(SignUpModel signUpModel);
        Task<ObjectResult> Login(LoginModel loginModel);
    }
}