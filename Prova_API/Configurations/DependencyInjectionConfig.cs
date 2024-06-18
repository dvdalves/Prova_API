using Prova_API.Infra.Data;
using Prova_API.Infra.Repository;

namespace Prova_API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<SqlServerDbContext>();
            services.AddScoped<SqlLiteDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            return services;
        }
    }
}
