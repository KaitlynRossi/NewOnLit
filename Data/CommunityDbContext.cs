using Microsoft.EntityFrameworkCore;
using OnLit.Models;

namespace OnLit.Data
{
    public class CommunityDbContext : DbContext
    {
        public CommunityDbContext(DbContextOptions<CommunityDbContext> options)
            : base(options) { }

        public DbSet<CommunityPost> CommunityPosts { get; set; }
    }
}