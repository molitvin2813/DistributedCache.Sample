using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Interfaces;

namespace ProductService.PostgreSQL
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddPostgresDB(this IServiceCollection services, IConfiguration configuration)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddDbContext<ProductServiceContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("CatalogServiceContext")));

            services.AddScoped<IProductServiceContext, ProductServiceContext>();
            return services;
        }
    }
}