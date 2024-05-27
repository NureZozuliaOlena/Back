using Back.Enums;

namespace Back.ViewModels
{
    public class ChangeOrderStatusViewModel
    {
        public int OrderId { get; set; }
        public OrderStatusEnum Status { get; set; }
    }
}
