using Condominios.Models.Entities;
using Condominios.Models;
using Microsoft.AspNetCore.Mvc;
using Condominios.Services;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;
using Condominios.Data.Repositories.CtrlEquipos;

namespace Condominios.Controllers
{
    public class TipoEquiposController : Controller
    {
        private readonly VarianteService _service;
        private VarianteRepository _repo = new();

        public TipoEquiposController(VarianteService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            VarianteViewModel model = await _service.Listas();

            return View(model);
        }

        public void Agregar(VarianteViewModel model)
        {

        }
    }
}
