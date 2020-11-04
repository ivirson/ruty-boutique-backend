﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.API.Data.Configurations
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
        }
    }
}
