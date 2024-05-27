using Back.Enums;
using Back.Models;

namespace Back.ViewModels
{
    public class CreateUserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User ToModel()
        {
            return new User() { Name = Name, Email = Email, Password = Password, AuthMethod = UserAuthMethodEnum.Default };
        }
    }
}
