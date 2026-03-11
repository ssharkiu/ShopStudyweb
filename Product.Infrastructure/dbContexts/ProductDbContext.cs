using Microsoft.EntityFrameworkCore;
using Product.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.dbContexts
{
    public class ProductDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product.Domain.Entity.Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public ProductDbContext(DbContextOptions options):base (options)
        {        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
