using System.ComponentModel.DataAnnotations;
using Back.Enums;

namespace Back.Models
{
    public class UserBase
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public UserAuthMethodEnum AuthMethod { get; set; }
    }
}