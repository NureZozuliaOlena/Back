using Back.Enums;
using Back.Models;

namespace Back.ViewModels
{
    public class CreateManagerViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Manager ToModel()
        {
            return new Manager() { Name = Name, Email = Email, Password = Password, AuthMethod = UserAuthMethodEnum.Default};
        }
    }
}
