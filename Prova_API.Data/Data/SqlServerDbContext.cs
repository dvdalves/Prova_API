using Microsoft.EntityFrameworkCore;
using Prova_API.Domain.Models;

namespace Prova_API.Data.Data
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
    }
}
