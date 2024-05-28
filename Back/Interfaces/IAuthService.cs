using Back.Models;
using Back.ViewModels;
using System.Threading.Tasks;

namespace Back.Interfaces
{
    public interface IAuthService
    {
        public AuthResult Login(LoginViewModel model);
        public AuthResult ManagerRegister(CreateManagerViewModel model);
        public AuthResult UserRegister(CreateUserViewModel model);
        public AuthResult UserGoogleRegister(CreateUserGoogleViewModel model);
        public AuthResult UserFacebookRegister(CreateUserFacebookViewModel model);
    }
}
