namespace ASPProject.Models{
    public class ShoppingCart
    {
        public int Id { get; set; } = default!;
        public int BookId { get; set; } = default!;
        public int Quantity { get; set; } = default!;

        public Book Book { get; set; } = default!;
    }
}