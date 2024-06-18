using Prova_MVC.Models;
using Prova_MVC.Services.Interfaces;
using Prova_MVC.Utils;

namespace Prova_MVC.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly HttpClient _httpClient;

        public ProdutoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResultadoPagina<Produto>> GetProdutosAsync(int page, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<ResultadoPagina<Produto>>($"api/produtos/{page}/{pageSize}");
            return response;
        }

        public async Task<Produto> GetProdutoByIdAsync(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<Produto>($"api/produtos/{id}");
            return response;
        }

        public async Task<Produto> CreateProdutoAsync(Produto produto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/produtos", produto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Produto>();
        }

        public async Task<bool> UpdateProdutoAsync(Guid id, Produto produto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/produtos/{id}", produto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProdutoAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/produtos/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AtivarProdutoAsync(Guid id)
        {
            var response = await _httpClient.PatchAsync($"api/produtos/{id}", null);
            return response.IsSuccessStatusCode;
        }
    }
}
