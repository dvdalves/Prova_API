using Prova_API.Domain.Models;

namespace Prova_API.Data.Repository
{
    public interface IProdutoRepository
    {
        Task<(int totalItems, IEnumerable<Produto> produtos)> ObterTodos(int page, int pageSize);
        Task<Produto> ObterPorId(Guid id);
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
        Task<bool> ProdutoExiste(Guid id);
    }
}