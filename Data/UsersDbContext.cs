using Microsoft.EntityFrameworkCore;
using ASPProject.Models;

public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> opts) : base(opts) { }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Customer>().ToTable("Users");
    }
}
