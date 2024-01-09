using Condominios.Models.Entities;
using Condominios.Models;
using Microsoft.AspNetCore.Mvc;
using Condominios.Services;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;
using Condominios.Data.Repositories.CtrlEquipos;
using Condominios.Models.ViewModels.Catalogos;
using Newtonsoft.Json;
using Condominios.Models.Services.Classes;
using Microsoft.AspNetCore.Authorization;
using System.Data;
#pragma warning disable CS8600

namespace Condominios.Controllers
{
    public class TipoEquiposController : Controller
    {
        private readonly VarianteService _service;

        public TipoEquiposController(VarianteService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> Index()
        {
            string json = string.Empty;
            VarianteViewModel model = await _service.Listas();

            if (TempData["AlertaJS"] != null)
            {
                json = (string)TempData["AlertaJS"];
                model.AlertaEstado = JsonConvert.DeserializeObject<AlertaEstado>(json);
            }
            return View(model);
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> Agregar(VarianteViewModel model)
        {
            model.AlertaEstado = await _service.AddEquipo(model);
            TempData["AlertaJS"] = JsonConvert.SerializeObject(model.AlertaEstado);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> ObtenerRegistro(int id)
        {
            Variante model = await _service.GetEquipo(id);

            var jsonResult = new JsonResult(model);
            return jsonResult;
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> Actualizar(VarianteViewModel model)
        {
            model.AlertaEstado = await _service.Update(model);
            TempData["AlertaJS"] = JsonConvert.SerializeObject(model.AlertaEstado);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task UpdateStatus(int id)
        {
            await _service.ActualizarEstado(id);
        }
    }
}
