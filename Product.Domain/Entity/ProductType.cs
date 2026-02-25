using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entity
{
    public class ProductType
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }//基础>>Pro>>ProMax
        public ProductType()
        {
            
        }
        public ProductType(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
