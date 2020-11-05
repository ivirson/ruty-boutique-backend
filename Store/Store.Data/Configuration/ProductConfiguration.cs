using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Models.Domain;

namespace Store.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .HasMany(p => p.Ratings)
                .WithOne(pr => pr.Product)
                .HasForeignKey(pr => pr.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasMany(p => p.Log)
               .WithOne(pl => pl.Product)
               .HasForeignKey(pl => pl.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasMany(p => p.Sizes)
               .WithOne(ps => ps.Product)
               .HasForeignKey(ps => ps.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
