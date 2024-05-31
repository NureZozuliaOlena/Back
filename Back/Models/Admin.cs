using System.Data;

namespace Back.Models
{
    public class Admin : UserBase
    {
        public Admin()
        {
            Role = "Admin";
        }
    }
}
