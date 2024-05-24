using System;
using Back.Enums;

namespace Back.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Cart Cart { get; set; }
        public int CartId { get; set; }
        public OrderStatusEnum Status { get; set; }
        public string Area { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }
}