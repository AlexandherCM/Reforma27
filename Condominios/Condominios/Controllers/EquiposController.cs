using Condominios.Models.Entities;
using Condominios.Models.ViewModels.CtrolEquipo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Condominios.Models.Services;
using Condominios.Models.Services.Classes;
#pragma warning disable CS8600
#pragma warning disable CS8604

namespace Condominios.Controllers
{
    public class EquiposController : Controller
    {
        private readonly CtrlEquipoService _service;
        private AlertaEstado _alertaEstado = new();
        public EquiposController(CtrlEquipoService service)
        {
            _service = service;
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        //[Authorize(Roles = "Administrador, General")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Index()
        {
            string json = string.Empty;
            var model = await _service.GetLists();

            if (TempData["Alerta"] != null)
            {
                json = (string)TempData["Alerta"];
                // Asegúrate de acceder correctamente al atributo UltimaAplicacion
                ModelState.AddModelError(nameof(model.Plantilla.UltimaAplicacion), json);

                return View(model);
            }

            if (TempData["Equipos"] != null)
            {
                json = (string)TempData["Equipos"];
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
                _alertaEstado = await _service.InsertarEquipos(model);

                if (_alertaEstado.Estado != false)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Alerta"] = _alertaEstado.Leyenda;
                    return RedirectToAction(nameof(Index));
                }
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
