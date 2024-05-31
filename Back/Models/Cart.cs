using System.Collections.Generic;

namespace Back.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public List<CartProduct> CartProducts { get; set; }
    }
}