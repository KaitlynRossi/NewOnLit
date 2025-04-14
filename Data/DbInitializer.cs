using ASPProject.Models;
using Microsoft.EntityFrameworkCore;

public static class DbInitializer
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        if (context.Books.Any()) return; 

        context.SaveChanges();
    }
}
