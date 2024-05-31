using Back.Enums;

namespace Back.Models
{
    public class User : UserBase
    {
        public User()
        {
            Role = "User";
        }

        public string Phone { get; set; }
        public byte[] Image { get; set; }
    }
}
