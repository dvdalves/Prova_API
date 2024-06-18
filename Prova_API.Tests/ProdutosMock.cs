using Prova_API.Domain.Models;

namespace Prova_API.Tests
{
    public static class ProdutosMock
    {
        private static Produto GetProdutoMock()
        {
            return new Produto
            {
                Id = Guid.NewGuid(),
                Nome = "Produto 1",
                Descricao = "Descrição do Produto 1",
                Valor = 10,
                Ativo = true
            };
        }

        public static List<Produto> GetProdutosMock()
        {
            var list = new List<Produto>
            {
                GetProdutoMock()
            };
            return list;
        }
    }
}
