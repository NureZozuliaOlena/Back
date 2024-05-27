using Back.Enums;
using Back.Models;

namespace Back.ViewModels
{
    public class CreateUserFacebookViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public User ToModel()
        {
            return new User()
            {
                Email = Email,
                Name = Name,
                AuthMethod = UserAuthMethodEnum.Facebook
            };
        }
    }
}
