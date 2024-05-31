using Back.Models;

namespace Back.Interfaces
{
    public interface IJwtService
    {
        string CreateToken(UserBase user);
    }
}
