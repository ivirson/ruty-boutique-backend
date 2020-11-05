using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Store.Data.Configuration;
using Store.Models.Audit;
using Store.Models.Core;
using Store.Models.Domain;

namespace Store.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<ProductLog> ProductLogs { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=StoreDB;Trusted_Connection=True;");
        }
    }
}
