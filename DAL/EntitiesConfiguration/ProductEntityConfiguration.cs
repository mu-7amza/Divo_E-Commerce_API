using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntitiesConfiguration
{
    internal class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
            builder.Property(p => p.Price).HasPrecision(18, 2);
            builder.Property(p => p.PictureUrl).IsRequired();

            builder.HasOne(p => p.Brand).WithMany(Brand => Brand.Products)
                .HasForeignKey(p => p.BrandId);
            builder.HasOne(p => p.Category).WithMany(C => C.Products)
                .HasForeignKey(p => p.CategoryId);

        }
    }
}
