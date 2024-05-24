namespace Back.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Grade { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}