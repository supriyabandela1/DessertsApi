using DessertsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DessertsApi.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
          //  Console.WriteLine($"DB Path: {Database.GetDbConnection().DataSource}");
        }
        public DbSet<Dessert> Desserts { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
