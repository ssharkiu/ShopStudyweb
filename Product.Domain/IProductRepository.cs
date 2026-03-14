/*
 * IProductRepository.cs
 * 产品仓储接口
 * 
 * 作用：
 * - 定义产品数据访问的抽象接口，遵循领域驱动设计中的仓储模式
 * - 提供产品相关的各种查询方法，如查找所有产品、根据ID查找、根据分类查找等
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
    public interface IProductRepository
    {
        /// <summary>
        /// 查找所有产品
        /// </summary>
        /// <returns>产品列表</returns>
        Task<List<Product.Domain.Entity.Product>> FindAllProductAsync();

        /// <summary>
        /// 根据ID查找产品
        /// </summary>
        /// <param name="productId">产品ID</param>
        /// <returns>产品对象</returns>
        Task<Product.Domain.Entity.Product> FindProductByIdAsync(Guid productId);

        /// <summary>
        /// 根据分类查找产品
        /// </summary>
        /// <param name="category">分类名称</param>
        /// <returns>产品列表</returns>
        Task<List<Product.Domain.Entity.Product>> FindProductByCategoryAsync(string category);

        /// <summary>
        /// 根据搜索文本查找产品
        /// </summary>
        /// <param name="searchText">搜索文本</param>
        /// <returns>产品列表</returns>
        Task<List<Product.Domain.Entity.Product>> FindProductBySearchAsync(string searchText);

        /// <summary>
        /// 获取搜索建议
        /// </summary>
        /// <param name="searchText">搜索文本</param>
        /// <returns>产品列表</returns>
        Task<List<string>> GetProductSearchSuggestionsAsync(string searchText);

        /// <summary>
        /// 查找特色产品
        /// </summary>
        /// <returns>产品列表</returns>
        Task<List<Product.Domain.Entity.Product>> FindProductsByFeatureAsync();
    }
}
