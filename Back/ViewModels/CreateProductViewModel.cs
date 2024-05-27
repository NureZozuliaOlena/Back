using Back.Enums;
using Back.Models;
using Microsoft.AspNetCore.Http;

namespace Back.ViewModels
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public IFormFile Image { get; set; }
        public CategoryEnum Category { get; set; }

        public Product ToModel()
        {
            return new Product()
            {
                Name = Name,
                Description = Description,
                Cost = Cost,
                Category = Category,
            };
        }
    }
}
