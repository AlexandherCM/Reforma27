using Condominios.Models.ViewModels.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Condominios.Models.Services;

namespace Condominios.Controllers
{
    public class CatalogosController : Controller
    {
        private readonly CatalogoService _service;
        public CatalogosController(CatalogoService service)
        {
            _service = service;
        }

        //[Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> Index()
        {
            CatalogoViewModel model = await _service.ObtenerCatalogos();
            return View(model);
        }

        //[Authorize(Roles = "Administrador, General")]
        //[ValidateAntiForgeryToken]
        public async Task<CatalogoViewModel> Create([FromBody] CatalogoViewModel model)
        {
            await _service.InsertarEntidad(model);

            CatalogoViewModel json = await _service.ObtenerCatalogos();
            return json;
        }

        public async Task<IActionResult> UpdateById(CatalogoViewModel model)
        {
            await _service.ActualizarEstado(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(CatalogoViewModel model)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
