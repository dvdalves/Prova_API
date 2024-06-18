using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prova_API.Configurations;
using Prova_API.Controllers.Utils;
using Prova_API.Domain.Models;
using Prova_API.Infra.Repository;

namespace Prova_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet("{page:int}/{pageSize:int}")]
        public async Task<ActionResult<ResultadoPagina<Produto>>> Get(int page = 1, int pageSize = 50)
        {
            var (totalProdutos, produtos) = await _produtoRepository.ObterTodos(page, pageSize);

            var resultado = new ResultadoPagina<Produto>
            {
                TotalItems = totalProdutos,
                Items = produtos,
                Page = page,
                PageSize = pageSize
            };

            return Ok(resultado);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Produto>> Get(Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post(Produto produto)
        {
            await _produtoRepository.Adicionar(produto);
            return CreatedAtAction("Get", new { id = produto.Id }, produto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _produtoRepository.Atualizar(produto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _produtoRepository.ProdutoExiste(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _produtoRepository.Remover(id);
            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch(Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            if (produto == null)
            {
                return NotFound();
            }

            produto.Ativar();
            await _produtoRepository.Atualizar(produto);

            return NoContent();
        }

    }
}
