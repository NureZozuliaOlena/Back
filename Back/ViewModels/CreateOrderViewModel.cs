using Back.Enums;
using Back.Models;
using System;

namespace Back.ViewModels
{
    public class CreateOrderViewModel
    {
        public int CartId { get; set; }
        public OrderStatusEnum Status { get; set; }
        public string Area { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }
}
