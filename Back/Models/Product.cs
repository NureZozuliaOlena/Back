using Back.Enums;

namespace Back.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public byte[] Image { get; set; }
        public CategoryEnum Category { get; set; }
    }
}
