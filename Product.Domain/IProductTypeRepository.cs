using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain
{
    public interface IProductTypeRepository
    {
        /// <summary>
        /// 查找所有商品类型
        /// </summary>
        /// <returns>商品类型列表</returns>
        Task<List<Product.Domain.Entity.ProductType>> FindAllProductTypeAsync();

        /// <summary>
        /// 查找商品类型By商品Id
        /// </summary>
        /// <param name="productId">商品Id</param>  
        /// <returns>商品类型</returns>
        Task<List<Product.Domain.Entity.ProductType>> GetProductTypeByProductIdAsync(Guid productId);
    }
}
