using Condominios.Models.Entities;
using Condominios.Models.ViewModels.CtrolEquipo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Condominios.Models.Services;
using Condominios.Models.Services.Classes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Condominios.Models.DTOs;
#pragma warning disable CS8600
#pragma warning disable CS8604
//ragma warning disable CS8602

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
            string json = string.Empty;
            var model = await _service.GetLists();

            if (TempData["Equipos"] != null)
            {
                json = (string)TempData["Equipos"];
                model.Equipos = JsonConvert.DeserializeObject<List<Equipo>>(json);
            }

            if (TempData["AlertaJS"] != null)
            {
                json = (string)TempData["AlertaJS"];
                model.AlertaEstado = JsonConvert.DeserializeObject<AlertaEstado>(json);
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
                model.AlertaEstado = await _service.InsertarEquipos(model);

                if (model.AlertaEstado.Estado)
                {
                    TempData["AlertaJS"] = JsonConvert.SerializeObject(model.AlertaEstado);
                    return RedirectToAction(nameof(Index));
                }

                model = await _service.GetLists(model);
                return View("Index", model);
            }
            return RedirectToAction(nameof(Index));
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchByFilters(FiltrosDTO FiltroID)
        {
            if(FiltroID.MarcaID == 0 && FiltroID.TipoID == 0 && FiltroID.UbicacionID == 0 && FiltroID.MotorID == 0)
                return RedirectToAction(nameof(Index));

            List<Equipo> equipos = await _service.GetEquipos(FiltroID);
            string dato = JsonConvert.SerializeObject(equipos);

            TempData["Equipos"] = dato; 
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchByNames(string CadenaBusqueda)
        {
            List<Equipo> equipos = await _service.GetEquipos(CadenaBusqueda);
            string dato = JsonConvert.SerializeObject(equipos);

            TempData["Equipos"] = dato;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchByStatus(int EstatusID = 0)
        {
            List<Equipo> equipos = await _service.GetEquipos(EstatusID);
            string dato = JsonConvert.SerializeObject(equipos);

            TempData["Equipos"] = dato;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task UpdateStatus(int id)
            => await _service.ActualizarEstado(id);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CtrolMtosEquipoViewModels viewModel)
        {
            viewModel.Plantilla.ID = viewModel.EquipoID;
            viewModel.AlertaEstado = _service.AactualizarEquipo(viewModel.Plantilla);

            TempData["AlertaJS"] = JsonConvert.SerializeObject(viewModel.AlertaEstado);
            return RedirectToAction("Consultar", "Mantenimientos", new { ID = viewModel.EquipoID });
        }

    }
}
