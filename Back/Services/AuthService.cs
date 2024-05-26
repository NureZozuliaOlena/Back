using Back.Interfaces;
using Back.Models;
using Back.ViewModels;

namespace Back.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public AuthResult Login(LoginViewModel model)
        {
            if (!_userRepository.CanLogin(model.Email, model.Password, model.AuthMethod))
            {
                var res = new AuthResult(false);
                res.SetErrorString("Incorrect authorization choice / password or email entered incorectly");
                return res;
            }

            var token = _jwtService.CreateToken(_userRepository.GetUser(model.Email));
            return new AuthResult(true, token);
        }

        public AuthResult ManagerRegister(CreateManagerViewModel model)
        {
            if (!_userRepository.EmailIsUnique(model.Email))
            {
                var res = new AuthResult(false);
                res.SetErrorString("this email is already taken");
                return res;
            }

            _userRepository.AddUser(model.ToModel());

            var token = _jwtService.CreateToken(_userRepository.GetUser(model.Email));
            return new AuthResult(true, token);
        }

        public AuthResult UserFacebookRegister(CreateUserFacebookViewModel model)
        {
            if (!_userRepository.EmailIsUnique(model.Email))
            {
                var res = new AuthResult(false);
                res.SetErrorString("this email is already taken");
                return res;
            }

            _userRepository.AddUser(model.ToModel());

            var token = _jwtService.CreateToken(_userRepository.GetUser(model.Email));
            return new AuthResult(true, token);
        }

        public AuthResult UserGoogleRegister(CreateUserGoogleViewModel model)
        {
            if (!_userRepository.EmailIsUnique(model.Email))
            {
                var res = new AuthResult(false);
                res.SetErrorString("this email is already taken");
                return res;
            }

            _userRepository.AddUser(model.ToModel());

            var token = _jwtService.CreateToken(_userRepository.GetUser(model.Email));
            return new AuthResult(true, token);
        }

        public AuthResult UserRegister(CreateUserViewModel model)
        {
            if (!_userRepository.EmailIsUnique(model.Email))
            {
                var res = new AuthResult(false);
                res.SetErrorString("this email is already taken");
                return res;
            }

            _userRepository.AddUser(model.ToModel());

            var token = _jwtService.CreateToken(_userRepository.GetUser(model.Email));
            return new AuthResult(true, token);
        }
    }
}
