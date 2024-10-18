using Microsoft.EntityFrameworkCore;
using ProductInventory.Domain.Entities;

namespace ProductInventory.Persistence.EFCore.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
