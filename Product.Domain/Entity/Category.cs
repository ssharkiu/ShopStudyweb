/*
 * Category.cs
 * 分类实体类
 * 
 * 功能：
 * - 定义分类的核心属性，包括ID、名称、URL等
 * - 管理分类的可见性和删除状态
 * - 作为产品的分类导航属性
 * 
 * 设计说明：
 * - 提供无参构造函数和带参构造函数
 * - 使用私有setter封装属性修改
 * - 采用软删除机制，保留数据同时标记删除状态
 * - 支持分类的基本管理功能
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entity
{
    public class Category
    {
        // 分类唯一标识（主键）
        public Guid Id { get; init; }
        
        // 分类名称
        public String Name { get; private set; }
        
        // 分类URL
        public String Url { get; private set; }
        
        // 是否可见
        public bool Visible { get; private set; }
        
        // 是否软删除
        public bool Deleted { get; private set; }
        public Category()
        {
            
        }
        public Category(string name,string url,bool visible=true,bool deleted=false)
        {
            Id = Guid.NewGuid();
            Name = name;
            Url = url;
            Visible = visible;
            Deleted = deleted;
        }

        public void CategoryVisible()//设置可见性
        {
            if (Visible)
            {
                Console.WriteLine("该分类已经是可见状态");
                return;
            }
            this.Visible = true;
        }
        public void CategoryInvisible()//设置不可见性
        {
            if (!Visible)
            {
                Console.WriteLine("该分类已经是不可见状态");
                return;
            }
            this.Visible = false;
        }
        public void CategoryDelete()//设置删除状态
        {
            if (Deleted)
            {
                Console.WriteLine("该分类已经是删除状态");
                return;
            }
            this.Deleted = true;
        }
        
    }
}
