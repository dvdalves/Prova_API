using API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Data
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
    }
}
