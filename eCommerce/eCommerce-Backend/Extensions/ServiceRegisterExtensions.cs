using eCommerce_Backend.Application.Common;
using eCommerce_Backend.Application.Interfaces;
using eCommerce_Backend.Application.IServices;
using eCommerce_Backend.Application.Services;
using eCommerce_Backend.Application.UnitOfWork;

namespace eCommerce_Backend.Extensions
{
    public static class ServiceRegisterExtensions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IFileStorage, FileStorage>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
