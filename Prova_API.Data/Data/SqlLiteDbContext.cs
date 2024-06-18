using Microsoft.EntityFrameworkCore;
using Prova_API.Domain.Models;

namespace Prova_API.Data.Data
{
    public class SqlLiteDbContext : DbContext
    {
        public SqlLiteDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
    }
}
