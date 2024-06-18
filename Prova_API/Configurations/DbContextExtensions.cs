using Microsoft.EntityFrameworkCore;
using Prova_API.Infra.Data;

namespace Prova_API.Configurations
{
    public static class DbContextExtensions
    {
        public static void AddCustomDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqlServerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection")));

            services.AddDbContext<SqlLiteDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("SqlLiteConnection")));
        }
    }
}
