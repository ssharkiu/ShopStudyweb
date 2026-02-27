/*
 * ProductType.cs
 * 产品类型实体类
 * 
 * 功能：
 * - 定义产品类型的核心属性，包括ID、名称等
 * - 支持产品变体的类型分类（如基础版、专业版等）
 * 
 * 设计说明：
 * - 提供无参构造函数和带参构造函数
 * - 使用私有setter封装属性修改
 * - 支持产品类型的基本管理功能
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entity
{
    public class ProductType
    {
        // 产品类型唯一标识（主键）
        public Guid Id { get; init; }
        
        // 产品类型名称（如基础版、专业版、专业Max版等）
        public string Name { get; private set; }
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
