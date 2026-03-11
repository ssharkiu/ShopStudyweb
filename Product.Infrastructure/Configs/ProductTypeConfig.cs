using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Product.Infrastructure.Configs
{
    public class ProductTypeConfig:IEntityTypeConfiguration<Product.Domain.Entity.ProductType>
    {
        public void Configure(EntityTypeBuilder<Product.Domain.Entity.ProductType> builder)
        {
            builder.ToTable($"T_{nameof(Product.Domain.Entity.ProductType)}");
        }
    }
}
