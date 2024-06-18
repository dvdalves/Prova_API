using Microsoft.AspNetCore.Mvc;
using Prova_MVC.Models;
using Prova_MVC.Services.Interfaces;

public class ProdutosController : Controller
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        if (pageSize > 50)
        {
            pageSize = 50;
        }

        var resultadoPagina = await _produtoService.GetProdutosAsync(page, pageSize);
        resultadoPagina.Page = page;
        resultadoPagina.PageSize = pageSize;
        return View(resultadoPagina);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var produto = await _produtoService.GetProdutoByIdAsync(id);
        return View(produto);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Produto produto)
    {
        if (ModelState.IsValid)
        {
            await _produtoService.CreateProdutoAsync(produto);
            return RedirectToAction(nameof(Index));
        }
        return View(produto);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var produto = await _produtoService.GetProdutoByIdAsync(id);
        return View(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, Produto produto)
    {
        if (ModelState.IsValid)
        {
            await _produtoService.UpdateProdutoAsync(id, produto);
            return RedirectToAction(nameof(Index));
        }
        return View(produto);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var produto = await _produtoService.GetProdutoByIdAsync(id);
        return View(produto);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _produtoService.DeleteProdutoAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Ativar(Guid id)
    {
        await _produtoService.AtivarProdutoAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
