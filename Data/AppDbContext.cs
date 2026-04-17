using Microsoft.EntityFrameworkCore;
using RecipeManager.Models;

namespace RecipeManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Recipe> Recipes { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Purchase> Purchases { get; set; } = default!;
    }
}
