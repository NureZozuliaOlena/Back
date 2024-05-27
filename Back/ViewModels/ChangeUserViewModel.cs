using Microsoft.AspNetCore.Http;

namespace Back.ViewModels
{
    public class ChangeUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public IFormFile Image { get; set; }

    }
}
