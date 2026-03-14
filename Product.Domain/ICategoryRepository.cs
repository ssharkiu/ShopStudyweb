/*
 * ICategoryRepository.cs
 * 分类仓储接口
 * 
 * 作用：
 * - 定义分类数据访问的抽象接口，遵循领域驱动设计中的仓储模式
 * - 提供分类相关的查询方法，如查找所有分类
 * - 为上层应用提供统一的数据访问接口，屏蔽底层数据存储细节
 * 
 * 设计说明：
 * - 采用异步方法设计，提高系统响应性能
 * - 方法命名清晰，直接反映业务意图
 * - 返回Task对象，支持异步操作
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// 查找所有分类
        /// </summary>
        /// <returns>分类列表</returns>
        Task<List<Product.Domain.Entity.Category>> FindAllCategoryAsync();
    }
}
