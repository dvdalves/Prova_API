using API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Data
{
    public class SqlLiteDbContext : DbContext
    {
        public SqlLiteDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
    }
}
