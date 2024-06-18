using Prova_MVC.Models;
using Prova_MVC.Utils;

namespace Prova_MVC.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<ResultadoPagina<Produto>> GetProdutosAsync(int page, int pageSize);
        Task<Produto> GetProdutoByIdAsync(Guid id);
        Task<Produto> CreateProdutoAsync(Produto produto);
        Task<bool> UpdateProdutoAsync(Guid id, Produto produto);
        Task<bool> DeleteProdutoAsync(Guid id);
        Task<bool> AtivarProdutoAsync(Guid id);
    }
}
