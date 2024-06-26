﻿using Microsoft.EntityFrameworkCore;
using Prova_API.Domain.Models;

namespace Prova_API.Infra.Data
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
    }
}
