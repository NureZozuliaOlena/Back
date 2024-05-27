using Back.Models;

namespace Back.ViewModels
{
    public class CreateReviewViewModel
    {
        public string Text { get; set; }
        public int Grade { get; set; }
        public int ProductId { get; set; }
    }
}
