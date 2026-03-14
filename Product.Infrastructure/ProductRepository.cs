/*
 * ProductRepository.cs
 * 产品仓储实现类
 * 
 * 作用：
 * - 实现 IProductRepository 接口，提供产品数据的实际访问操作
 * - 使用 Entity Framework Core 进行数据库操作
 * - 封装产品相关的查询逻辑，如查找所有产品、根据ID查找、根据分类查找、搜索等
 * - 处理产品及其变体的关联数据加载
 * 
 * 设计说明：
 * - 实现仓储模式，将数据访问逻辑与业务逻辑分离
 * - 使用异步方法提高系统性能
 * - 通过 Include 和 ThenInclude 加载关联数据
 * - 统一过滤已删除和不可见的数据
 */
using Microsoft.EntityFrameworkCore;
using Product.Domain;
using Product.Infrastructure.dbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure
{
    /// <summary>
    /// 产品仓储实现类
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        // 数据库上下文实例，用于执行数据库操作
        private readonly ProductDbContext _dbContext;

        /// <summary>
        /// 构造函数，通过依赖注入获取数据库上下文
        /// </summary>
        /// <param name="dbContext">产品数据库上下文</param>
        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 查找所有可见且未删除的产品
        /// </summary>
        /// <returns>产品列表，包含可见的变体信息</returns>
        public async Task<List<Domain.Entity.Product>> FindAllProductAsync()
        {
            return await _dbContext.Products
            // 加载产品的变体，只加载未删除且可见的变体
            .Include(p => p.Variants.Where(x => x.Deleted == false && x.Visible == true))
            // 过滤未删除且可见的产品
            .Where(x => x.Deleted == false && x.Visible == true).ToListAsync();
        }

        /// <summary>
        /// 根据分类URL查找产品
        /// </summary>
        /// <param name="categoryUrl">分类URL</param>
        /// <returns>该分类下的产品列表，包含可见的变体信息</returns>
        public async Task<List<Domain.Entity.Product>> FindProductByCategoryAsync(string categoryUrl)
        {
            return await _dbContext.Products
            // 过滤未删除、可见且属于指定分类的产品
            .Where(x => x.Deleted == false && x.Visible == true && x.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
            // 加载产品的变体，只加载未删除且可见的变体
            .Include(v => v.Variants.Where(x => x.Deleted == false && x.Visible == true))
            .ToListAsync();
        }

        /// <summary>
        /// 根据产品ID查找产品
        /// </summary>
        /// <param name="productId">产品ID</param>
        /// <returns>产品对象，包含变体和产品类型信息</returns>
        public async Task<Domain.Entity.Product> FindProductByIdAsync(Guid productId)
        {
            var result = await _dbContext.Products
            // 加载产品的变体，只加载未删除且可见的变体
            .Include(x => x.Variants.Where(x => x.Deleted == false && x.Visible == true))
            // 加载变体的产品类型信息
            .ThenInclude(x => x.ProductType)
            // 查找指定ID且未删除、可见的产品
            .SingleOrDefaultAsync(x => x.Id == productId && x.Deleted == false && x.Visible == true);
            return result;
        }

        /// <summary>
        /// 使用LINQ根据搜索文本查找产品（内部方法）
        /// </summary>
        /// <param name="searchText">搜索文本</param>
        /// <returns>匹配的产品列表，包含可见的变体信息</returns>
        public async Task<List<Domain.Entity.Product>> FindProductBySearchOnLinq(string searchText)
        {
            var result = await _dbContext.Products
             // 过滤未删除、可见且标题或描述包含搜索文本的产品
             .Where(x => x.Deleted == false && x.Visible == true && x.Title.ToLower().Contains(searchText.ToLower()) || x.Description.ToLower().Contains(searchText.ToLower()))
             // 加载产品的变体，只加载未删除且可见的变体
             .Include(x => x.Variants.Where(x => x.Deleted == false && x.Visible == true))
             .ToListAsync();
            return result;
        }

        /// <summary>
        /// 根据搜索文本查找产品
        /// </summary>
        /// <param name="searchText">搜索文本</param>
        /// <returns>匹配的产品列表</returns>
        public async Task<List<Domain.Entity.Product>> FindProductBySearchAsync(string searchText)
        {
            var result = await FindProductBySearchOnLinq(searchText);
            return result;
        }

        /// <summary>
        /// 查找特色产品
        /// </summary>
        /// <returns>特色产品列表，包含可见的变体信息</returns>
        public async Task<List<Domain.Entity.Product>> FindProductsByFeatureAsync()
        {
            var result = await _dbContext.Products
            // 过滤未删除且可见的产品
            .Where(x => x.Deleted == false && x.Visible == true)
            // 加载产品的变体，只加载未删除且可见的变体
            .Include(x => x.Variants.Where(x => x.Deleted == false && x.Visible == true))
            // .ThenInclude(x => x.ProductType)
            .ToListAsync();
            return result;
        }

        /// <summary>
        /// 获取产品搜索建议
        /// </summary>
        /// <param name="searchText">搜索文本</param>
        /// <returns>搜索建议列表（产品标题和描述中的关键词）</returns>
        public async Task<List<string>> GetProductSearchSuggestionsAsync(string searchText)
        {
            // 先搜索匹配的产品
            var products = await FindProductBySearchAsync(searchText);
            List<string> result = new List<string>();
            foreach (var product in products)
            {
                // 如果产品标题包含搜索文本，将标题添加到建议列表
                if (product.Title.ToLower().Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                // 处理产品描述，提取包含搜索文本的关键词
                if (product.Description != null)
                {
                    // 获取描述中的所有标点符号
                    var punctuation = product.Description.Where(char.IsPunctuation).Distinct().ToArray();
                    // 将描述按空格分割成单词，并去除标点符号
                    var words = product.Description.Split().Select(s => s.Trim(punctuation));
                    foreach (var word in words)
                    {
                        // 如果单词包含搜索文本且不在建议列表中，添加到建议列表
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }

            }
            return result;
        }
    }
}