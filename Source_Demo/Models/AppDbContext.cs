using Microsoft.EntityFrameworkCore;

namespace YourNamespace.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Khai báo DbSet cho mỗi bảng trong database
        // public DbSet<User> Account { get; set; }
        // public DbSet<Product> ...
    }
}
