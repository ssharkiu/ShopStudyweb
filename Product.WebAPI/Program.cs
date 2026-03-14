/*
 * Program.cs
 * Product.WebAPI 应用程序入口点
 * 
 * 作用：
 * - 配置和启动 ASP.NET Core Web API 应用
 * - 注册依赖服务，如控制器、Swagger、数据库上下文等
 * - 配置 HTTP 请求管道，包括中间件和路由
 * 
 * 设计说明：
 * - 使用 WebApplication.CreateBuilder 创建应用构建器
 * - 通过依赖注入注册各种服务
 * - 配置数据库连接和仓储服务
 * - 构建并运行应用
 */
using Microsoft.EntityFrameworkCore;
using Product.Infrastructure.dbContexts;
using Product.Infrastructure;
using Product.Domain;


// 创建 Web 应用构建器
var builder = WebApplication.CreateBuilder(args);

// 添加服务到容器

// 添加控制器服务
builder.Services.AddControllers();

// 添加 Swagger/OpenAPI 服务，用于 API 文档生成
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 配置数据库上下文
// 使用 PostgreSQL 数据库，连接字符串从配置中获取
builder.Services.AddDbContext<ProductDbContext>(opt =>
{
    // 从配置的 ConnStr 部分获取连接字符串
    opt.UseNpgsql(builder.Configuration.GetSection("ConnStr").Value);
});

// 注册仓储服务（使用 Scoped 生命周期）
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// 构建应用
var app = builder.Build();

// 配置 HTTP 请求管道
if (app.Environment.IsDevelopment())
{
    // 在开发环境中启用 Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 启用 HTTPS 重定向
app.UseHttpsRedirection();

// 启用授权中间件
app.UseAuthorization();

// 配置控制器路由
app.MapControllers();

// 运行应用
app.Run();