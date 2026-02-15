using AMS.Mapper;
using AMS.Repository;
using AMS.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

public static class ServicesConfiguration
{
    public static void AddProviderServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IDashboardRepository, DashboardRepository>();
        services.AddTransient<IAccountServices, AccountServices>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();

        // If no interface, direct registration


    }
}
