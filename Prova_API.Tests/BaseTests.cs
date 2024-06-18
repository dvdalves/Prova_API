using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prova_API.Infra.Data;

namespace Prova_API.Tests
{
    public class BaseTests
    {
        protected SqlServerDbContext _context = default!;
        protected IServiceProvider _serviceProvider = default!;
        private readonly string DataBaseName = "DataBaseTest" + Guid.NewGuid();

        public void InicializarContexto()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<SqlServerDbContext>(options => options.UseInMemoryDatabase(databaseName: DataBaseName));
            _serviceProvider = serviceCollection.BuildServiceProvider();
            _context = _serviceProvider.GetRequiredService<SqlServerDbContext>();
        }

        protected void PopularBancoDeDados<T>(ICollection<T> collection) where T : class
        {
            if (collection != null && collection.Count != 0)
            {
                _context.AddRange(collection);
                _context.SaveChanges();
            }
        }
    }
}
