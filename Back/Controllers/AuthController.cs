using Back.Interfaces;
using Back.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Back.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginViewModel viewModel)
        {
            return new JsonResult(_authService.Login(viewModel));
        }

        [HttpPost("reg/user")]
        public IActionResult UserRegister(CreateUserViewModel model)
        {
            return new JsonResult(_authService.UserRegister(model));
        }

        [HttpPost("reg/user/google")]
        public IActionResult UserGoogleRegister(CreateUserGoogleViewModel model)
        {
            return new JsonResult(_authService.UserGoogleRegister(model));
        }

        [HttpPost("reg/user/facebook")]
        public IActionResult UserFacebookRegister(CreateUserFacebookViewModel model)
        {
            return new JsonResult(_authService.UserFacebookRegister(model));
        }

        [HttpPost("reg/manager")]
        public IActionResult ManagerRegister(CreateManagerViewModel model)
        {
            return new JsonResult(_authService.ManagerRegister(model));
        }

        [Authorize]
        [HttpPost("checkSignIn")]
        public IActionResult Check()
        {
            return Ok();
        }
    }
}
