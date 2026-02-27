/*
 * Product.cs
 * 产品实体类
 * 
 * 功能：
 * - 定义产品的核心属性，包括ID、标题、描述、图片URL等
 * - 管理产品与分类的关联关系
 * - 处理产品变体（如规格、颜色、价格等）
 * - 提供产品状态管理方法（推荐、可见性、软删除等）
 * 
 * 设计说明：
 * - 实现IAggregateRoot接口，作为领域驱动设计中的聚合根
 * - 使用私有setter封装属性修改，确保状态变更通过方法进行
 * - 采用软删除机制，保留数据同时标记删除状态
 * - 提供防御性编程，避免无效操作和状态不一致
 */
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
        public bool Featured { get; private set; }

        // 是否可见（私有setter）
        public bool Visible { get; private set; }

        // 是否软删除（私有setter）
        public bool Deleted { get; private set; }

        public Product()
        {
            
        }
        public Product(string title, string description,string imageUrl,Guid categoryId,List<ProductVariant>variants,bool featured=false,bool visible=true ,bool deleted=false)
        {
            Id= Guid.NewGuid();
            Title= title;
            Description= description;
            ImageUrl = imageUrl;
            CategoryId= categoryId;
            Variants= variants;
            Featured= featured;
            Visible= visible;
            Deleted= deleted;
        }

        public void ActiveFeatured()//激活推荐商品
        {
            if (Featured)
            {
                Console.WriteLine("该商品已经是特色商品");
                return;
            }
            this.Featured = true;
        }

        public void DrawbackFeatured()//取消推荐商品    
        {
            if (!Featured)
            {
                Console.WriteLine("该商品不是特色商品");
                return;
            }
            this.Featured = false;
        }

        public void AddVariant(ProductVariant variant)//添加套餐
        {
            if (this.Variants.Contains(variant))
            {
                Console.WriteLine("该商品已包含该套餐");
                return;
            }
            this.Variants.Add(variant);
        }

            public void RemoveVariant(ProductVariant variant)//删除套餐
        {
            if (!this.Variants.Contains(variant))
            {
                Console.WriteLine("该商品不包含该套餐");
                return;
            }
            this.Variants.Remove(variant);  
        }

        public void ProductVisible()//设置可见性
        {
            if (Visible)
            {
                Console.WriteLine("该商品已经是可见状态");
                return;
            }
            this.Visible = true;
        }
        public void ProductInvisible()//设置不可见性
        {
            if (!Visible)
            {
                Console.WriteLine("该商品已经是不可见状态");
                return;
            }
            this.Visible = false;
        }

        public void ProductDelete()//软删除商品
        {
            if (Deleted)
            {
                Console.WriteLine("该商品已经被删除");
                return;
            }
            this.Visible = false;
            this.Deleted = true;
        }
    }
}
