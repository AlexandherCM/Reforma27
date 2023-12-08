using Condominios.Models.Entities;
using Condominios.Models;
using Microsoft.AspNetCore.Mvc;
using Condominios.Services;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;
using Condominios.Data.Repositories.CtrlEquipos;
using Condominios.Models.ViewModels.Catalogos;

namespace Condominios.Controllers
{
    public class TipoEquiposController : Controller
    {
        private readonly VarianteService _service;

        public TipoEquiposController(VarianteService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            VarianteViewModel model = await _service.Listas();

            return View(model);
        }

        public async Task<IActionResult> Agregar(VarianteViewModel model)
        {
            await _service.AddEquipo(model);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ObtenerRegistro(int id)
        {
            Variante model = await _service.GetEquipo(id);

            var jsonResult = new JsonResult(model);
            return jsonResult;
        }

    }
}
