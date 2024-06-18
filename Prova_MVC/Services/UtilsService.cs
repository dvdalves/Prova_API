using Prova_MVC.Models;
using Prova_MVC.Utils;
using System.Text.Json;

namespace Prova_MVC.Services
{
    public class UtilsService
    {
        private readonly HttpClient _httpClient;

        public UtilsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResultadoPagina<Produto>> GetProdutosAsync(int page, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<ResultadoPagina<Produto>>($"api/utils?page={page}&pageSize={pageSize}");
            return response;
        }

        public async Task<HttpResponseMessage> AddManyProdutosAsync(int quantidade)
        {
            return await _httpClient.PostAsync($"api/utils/AddMany/{quantidade}", null);
        }

        public async Task<HttpResponseMessage> DeleteAllProdutosAsync()
        {
            return await _httpClient.DeleteAsync("api/utils/DeleteAll");
        }

        public async Task<string> GetDataAtualAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<JsonElement>("api/utils/DataAtual");
            return response.GetProperty("currentDateTime").GetString();
        }
    }
}
