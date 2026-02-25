using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entity
{
    public class Product:IAggregateRoot
    {
        // 产品唯一标识（主键）
        public Guid Id { get; init; }

        // 产品标题（私有setter，只能通过构造函数/内部方法修改）
        public String Title { get; private set; }

        // 产品描述（私有setter）
        public String Description { get; private set; }

        // 图片URL
        public String ImageUrl { get; private set; }

        // 产品分类（导航属性，可空，私有setter）
        public Category? Category { get; private set; }

        // 分类ID（外键，私有setter）
        public Guid CategoryId { get; private set; }

        // 产品变体（如规格、颜色、价格等，私有setter）
        public List<ProductVariant> Variants { get; private set; }

        // 是否为推荐产品（私有setter）
        public bool Feature { get; private set; }

        // 是否可见（私有setter）
        public bool Visible { get; private set; }

        // 是否软删除（私有setter）
        public bool Deleted { get; private set; }

        public Product()
        {
            
        }
        public Product(string title, string description,string imageUrl,Guid categoryId,List<ProductVariant>variants,bool feature=false,bool visible=true ,bool deleted=false)
        {
            Id= Guid.NewGuid();
            Title= title;
            Description= description;
            ImageUrl = imageUrl;
            CategoryId= categoryId;
            Variants= variants;
            Feature= feature;
            Visible= visible;
            Deleted= deleted;
        }
    }
}
