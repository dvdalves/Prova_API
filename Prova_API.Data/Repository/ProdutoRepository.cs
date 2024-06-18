using Microsoft.EntityFrameworkCore;
using Prova_API.Domain.Models;
using Prova_API.Infra.Data;

namespace Prova_API.Infra.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly SqlServerDbContext _context;

        public ProdutoRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<(int totalItems, IEnumerable<Produto> produtos)> ObterTodos(int page, int pageSize)
        {
            var totalItems = await _context.Produtos.CountAsync();
            var produtos = await _context.Produtos
                                         .OrderBy(p => p.DataCadastro)
                                         .Skip((page - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToListAsync();

            return (totalItems, produtos);
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task Adicionar(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Remover(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ProdutoExiste(Guid id)
        {
            return await _context.Produtos.AnyAsync(e => e.Id == id);
        }
    }
}
