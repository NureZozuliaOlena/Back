using Back.Enums;

namespace Back.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserAuthMethodEnum AuthMethod { get; set; }
    }
}
