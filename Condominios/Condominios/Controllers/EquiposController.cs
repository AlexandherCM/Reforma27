using Condominios.Models.Entities;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
#pragma warning disable CS8600
#pragma warning disable CS8604

namespace Condominios.Controllers
{
    public class EquiposController : Controller
    {
        private readonly CtrlEquipoService _service;
        public EquiposController(CtrlEquipoService service)
        {
            _service = service;
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        //[Authorize(Roles = "Administrador, General")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Index()
        {
            var model = await _service.GetLists();

            if (TempData["Equipos"] != null)
            {
                string json = (string)TempData["Equipos"];
                model.Equipos = JsonConvert.DeserializeObject<List<Equipo>>(json);
            }

            return View(model);
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> Create(CtrlEquipoViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _service.InsertarEquipos(model);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchByFilters(CtrlEquipoViewModel model)
        {
            List<Equipo> equipos = await _service.GetEquipos(model.FiltroID);
            string dato = JsonConvert.SerializeObject(equipos);

            TempData["Equipos"] = dato;
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchByNames(CtrlEquipoViewModel model)
        {
            List<Equipo> equipos = await _service.GetEquipos(model.CadenaBusqueda);
            string dato = JsonConvert.SerializeObject(equipos);

            TempData["Equipos"] = dato;
            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchByStatus(CtrlEquipoViewModel model)
        {
            List<Equipo> equipos = await _service.GetEquipos(model.EstatusID ?? 0);
            string dato = JsonConvert.SerializeObject(equipos);

            TempData["Equipos"] = dato;
            return RedirectToAction(nameof(Index));
        }

    }
}
