using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prova_API.Configurations;
using Prova_API.Controllers;
using Prova_API.Domain.Models;
using Prova_API.Infra.Repository;

namespace Prova_API.Tests
{
    public class ProdutosTests : BaseTests
    {
        private ProdutosController _controller;
        private IProdutoRepository _produtoRepository;

        [SetUp]
        public void Setup()
        {
            InicializarContexto();
            _produtoRepository = new ProdutoRepository(_context);
            _controller = new ProdutosController(_produtoRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Criar_Produto_Sucesso()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Teste",
                Descricao = "Teste",
                Valor = 10,
                Ativo = true
            };

            // Act
            var result = await _controller.Post(produto);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<CreatedAtActionResult>());
        }

        [Test]
        public void Criar_Produto_Falha()
        {
            // Arrange
            var produto = new Produto
            {
                Nome = "Teste",
                Descricao = "Teste",
                Valor = 10,
                Ativo = true
            };

            produto.Nome = null;

            // Act and Assert
            Assert.ThrowsAsync<DbUpdateException>(async () => await _controller.Post(produto));
        }


        [Test]
        public async Task Editar_Produto_Sucesso()
        {
            // Arrange
            PopularBancoDeDados(ProdutosMock.GetProdutosMock());

            // Act
            var produto = _context.Produtos.First();
            produto.Nome = "Test Edite";
            var result = await _controller.Put(produto.Id, produto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<NoContentResult>());
                Assert.That(result, Is.Not.Null);
            });
        }

        [Test]
        public async Task Editar_Produto_Nao_Existente_Falha()
        {
            // Arrange
            var produto = new Produto
            {
                Id = Guid.NewGuid(),
                Nome = "Test Edit",
                Descricao = "Test Edit",
                Valor = 10,
                Ativo = true
            };

            // Act
            var result = await _controller.Put(produto.Id, produto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<NotFoundResult>());
                Assert.That(result, Is.Not.Null);
            });
        }

        [Test]
        public async Task Deletar_Produto_Sucesso()
        {
            // Arrange
            PopularBancoDeDados(ProdutosMock.GetProdutosMock());
            var produto = _context.Produtos.First();

            // Act
            var result = await _controller.Delete(produto.Id);
            var produtoDeletado = await _context.Produtos.FindAsync(produto.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<NoContentResult>());
                Assert.That(produtoDeletado, Is.Null);
            });
        }

        [Test]
        public async Task Deletar_Produto_Falha()
        {
            // Arrange
            var produto = new Produto
            {
                Id = Guid.NewGuid(),
                Nome = "Delete Fail",
                Descricao = "Delete Fail",
                Valor = 10,
                Ativo = true
            };

            // Act
            var result = await _controller.Delete(produto.Id);
            var produtoDeletado = await _context.Produtos.FindAsync(produto.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<NoContentResult>());
                Assert.That(produtoDeletado, Is.Null);
            });
        }

        [Test]
        public async Task Obter_Produto_Sucesso()
        {
            // Arrange
            PopularBancoDeDados(ProdutosMock.GetProdutosMock());
            var produto = _context.Produtos.First();

            // Act
            var result = await _controller.Get(produto.Id);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.Value, Is.EqualTo(produto));
        }


        [Test]
        public async Task Obter_Produto_Falha()
        {
            // Act
            var result = await _controller.Get(Guid.NewGuid());

            // Assert
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Obter_Produtos_Sucesso()
        {
            // Arrange
            PopularBancoDeDados(ProdutosMock.GetProdutosMock());

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result.Result as OkObjectResult;
            var resultado = okResult.Value as ResultadoPagina<Produto>;
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            Assert.That(okResult.Value, Is.InstanceOf<ResultadoPagina<Produto>>());
            Assert.That(resultado.Items.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task Ativar_Produto_Sucesso()
        {
            // Arrange
            PopularBancoDeDados(ProdutosMock.GetProdutosMock());
            var produto = _context.Produtos.First();
            produto.Ativo = false;

            // Act
            var result = await _controller.Patch(produto.Id);
            var produtoAtivado = await _context.Produtos.FindAsync(produto.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.InstanceOf<NoContentResult>());
                Assert.That(produtoAtivado.Ativo, Is.True);
            });
        }

        [Test]
        public async Task Ativar_Produto_Falha()
        {
            // Act
            var result = await _controller.Patch(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
