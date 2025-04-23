using ASPProject.Models;
using Microsoft.EntityFrameworkCore;
using OnLit.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Customer> User { get; set; }
     public DbSet<CommunityPost> Community { get; set; }
}
