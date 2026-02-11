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
        //services.AddTransient<IProductRepository, ProductRepository>();
        // If ProductRepository implements IProductRepository
        services.AddScoped<IProductRepository, ProductRepository>();

        // If no interface, direct registration


    }
}
