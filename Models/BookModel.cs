namespace ASPProject.Models  // Ensure this matches the namespace you're referencing
{
    public class Book
    {
        public int Id { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Author { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Genre { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
    }
}