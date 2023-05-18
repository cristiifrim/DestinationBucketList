using DBLApi.Data;
using DBLApi.Middleware;
using DBLApi.Repositories;
using DBLApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DBLApi.Extensions
{

public static class StartupExtensions
{
    public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DataContext>(options =>
        {
            var connectionString = config.GetConnectionString("PostgresConnection");
            options.UseNpgsql(connectionString);
        });
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }

    public static IServiceCollection AddMiddlewareServices(this IServiceCollection services)
    {
        services.AddSingleton<ErrorHandlerMiddleware>();
        return services;
    }
}
}