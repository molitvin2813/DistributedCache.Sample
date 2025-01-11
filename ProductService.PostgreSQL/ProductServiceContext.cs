using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Models;
using ProductService.PostgreSQL.EntityTypeConfiguration;

namespace ProductService.PostgreSQL
{
    public class ProductServiceContext : DbContext, IProductServiceContext
    {
        public ProductServiceContext() { }
        public ProductServiceContext(DbContextOptions<ProductServiceContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
