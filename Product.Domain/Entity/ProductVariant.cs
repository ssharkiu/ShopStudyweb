/*
 * ProductVariant.cs
 * 产品变体实体类
 * 
 * 功能：
 * - 定义产品变体的核心属性，包括ID、产品关联、类型关联、价格等
 * - 管理变体的可见性和删除状态
 * - 支持产品的不同规格、颜色、价格等变体管理
 * 
 * 设计说明：
 * - 实现IAggregateRoot接口，作为领域驱动设计中的聚合根
 * - 使用私有setter封装属性修改
 * - 采用软删除机制，保留数据同时标记删除状态
 * - 支持与产品和产品类型的关联关系
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Product.Domain.Entity
{
    public class ProductVariant:IAggregateRoot
    {
        // 产品变体唯一标识（主键）
        public Guid Id { get; init; }
        
        // 关联的产品（导航属性，Json序列化时忽略）
        [JsonIgnore]
        public Product? Product { get; private set; }
        
        // 产品ID（外键）
        public Guid ProductId { get;private set; }

        // 关联的产品类型（导航属性）
        public ProductType? ProductType { get; private set; }
        
        // 产品类型ID（外键）
        public Guid ProductTypeId { get; private set; }
        
        // 销售价格
        public decimal Price {  get; private set; }
        
        // 原始价格
        public decimal OriginalPrice {  get; private set; }
        
        // 是否可见
        public bool Visible {  get; private set; }
        
        // 是否软删除
        public bool Deleted {  get; private set; }
        public ProductVariant()
        {
            
        }
        public ProductVariant(Guid productId,Guid typeId,decimal oringinalPrice,decimal price,bool visible=true,bool deleted=false)
        {
            Id=Guid.NewGuid();
            ProductId=productId;
            ProductTypeId=typeId;
            OriginalPrice=oringinalPrice;
            Price=price;
            Visible=visible;
            Deleted=deleted;
            //123
                
        }
        public void UpdatePrice(ProductType type)//更新价格
        {
            if (type.Name==ProductType.Name)
            {
                Console.WriteLine("已经是该类型的套餐");
                return;
            }
            ProductType=type;
        }
        public void VariantVisible()//设置可见性
        {
            if (Visible)
            {
                Console.WriteLine("该套餐(变体)已经是可见状态");
                return;
            }
            this.Visible = true;
        }
        public void VariantInvisible()//设置不可见性
        {
            if (!Visible)
            {
                Console.WriteLine("该套餐(变体)已经是不可见状态");
                return;
            }
            this.Visible = false;
        }
        public void VariantDelete()//设置删除状态
        {
            if (Deleted)
            {
                Console.WriteLine("该套餐(变体)已经是删除状态");
                return;
            }
            this.Deleted = true;
        }

    }
}
