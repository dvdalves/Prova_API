using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prova_API.Configurations;
using Prova_API.Controllers.Utils;
using Prova_API.Domain.Models;
using Prova_API.Infra.Data;

[ApiController]
[Route("api/[controller]")]
public class UtilsController : BaseController
{
    private readonly SqlLiteDbContext _context;
    private static readonly TimeZoneInfo BrasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
    public UtilsController(SqlLiteDbContext context)
    {
        _context = context;
    }

    [HttpDelete]
    [Route("DeleteAll")]
    public async Task<ActionResult> Delete()
    {
        _context.Produtos.RemoveRange(_context.Produtos);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("AddMany/{quantidade}")]
    public async Task<ActionResult<IEnumerable<Produto>>> Post(int quantidade)
    {
        if (quantidade > 1000) return BadRequest("A quantidade máxima permitida é de 1000 produtos.");

        var random = new Random();
        var produtos = new List<Produto>();

        for (int i = 0; i < quantidade; i++)
        {
            var produto = new Produto
            {
                Nome = $"Produto {i + 1}",
                Descricao = $"Descrição do produto {i + 1}",
                Valor = random.Next(1, 1000),
                Ativo = random.Next(0, 2) == 1
            };

            produtos.Add(produto);
        }

        _context.Produtos.AddRange(produtos);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<ResultadoPagina<Produto>>> Get(int page = 1, int pageSize = 15)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 15;
        if (pageSize > 50) pageSize = 50;

        var totalItems = await _context.Produtos.CountAsync();
        var produtos = await _context.Produtos
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

        var resultado = new ResultadoPagina<Produto>
        {
            TotalItems = totalItems,
            Items = produtos,
            Page = page,
            PageSize = pageSize
        };

        return Ok(resultado);
    }

    [HttpGet]
    [Route("DataAtual")]
    public IActionResult Get()
    {
        var utcNow = DateTime.UtcNow;
        var brasiliaDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, BrasiliaTimeZone);
        return Ok(new { currentDateTime = brasiliaDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffK") });
    }
}