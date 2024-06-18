using Microsoft.AspNetCore.Mvc;
using Prova_MVC.Services;

namespace Prova_MVC.Controllers
{
    public class UtilsController : Controller
    {
        private readonly UtilsService _utilsService;

        public UtilsController(UtilsService utilsService)
        {
            _utilsService = utilsService;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            if (pageSize > 50)
            {
                pageSize = 50;
            }

            var resultadoPagina = await _utilsService.GetProdutosAsync(page, pageSize);
            resultadoPagina.Page = page;
            resultadoPagina.PageSize = pageSize;
            return View(resultadoPagina);
        }

        public IActionResult AddMany()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMany(int quantidade)
        {
            if (quantidade > 1000)
            {
                ModelState.AddModelError("", "A quantidade máxima permitida é de 1000 produtos.");
                return View();
            }

            var response = await _utilsService.AddManyProdutosAsync(quantidade);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Erro ao adicionar produtos.");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAll()
        {
            var response = await _utilsService.DeleteAllProdutosAsync();
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Erro ao deletar produtos.");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DataAtual()
        {
            var currentDateTime = await _utilsService.GetDataAtualAsync();
            ViewBag.CurrentDateTime = currentDateTime;
            return View();
        }
    }
}
