using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Domain.Entity;
using Product.Infrastructure;
using Product.Domain;
using Product.WebAPI.Controllers.Response;

namespace Product.WebAPI.Controllers
{
    /// <summary>
    /// 分类控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="categoryRepository">分类仓储</param>
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <returns>分类列表</returns>
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetCategories()
        {
            var resp = new ServiceResponse<List<Category>>();
            var result = await _categoryRepository.FindAllCategoryAsync();
            if (result == null || result.Count == 0)
            {
                resp.Success = false;
                resp.Message = "没有更多数据";
            }
            else
            {
                resp.Data = result;
            }
            return Ok(resp);
        }
    }
}
