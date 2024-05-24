using Back.Enums;
using Back.Models;

namespace Back.Interfaces
{
    public interface IUserRepository
    {
        public bool CanLogin(string email, string password, UserAuthMethodEnum authMethod);
        public UserBase GetUser(string login);
        public bool EmailIsUnique(string email);
        public void AddUser(UserBase user);
    }
}