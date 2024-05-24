using Back.Enums;
using Back.Interfaces;
using Back.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Back.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppContext _context;

        public UserRepository(AppContext context)
        {
            _context = context;
        }

        public bool CanLogin(string email, string password, UserAuthMethodEnum authMethod)
        {
            var user = _context.UserBases.FirstOrDefault(c => c.Email == email);

            return user is not null && user.Password == password && user.AuthMethod == authMethod;
        }

        public UserBase GetUser(string login)
        {
            return _context.UserBases.FirstOrDefault(c => c.Email == login);
        }

        public bool EmailIsUnique(string email)
        {
            return !_context.UserBases.Any(c => c.Email == email);
        }

        public void AddUser(UserBase user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }
    }
}
