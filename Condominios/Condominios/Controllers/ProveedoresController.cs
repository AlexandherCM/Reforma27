using Condominios.Models.Entities;
using Condominios.Models.Services;
using Condominios.Models.ViewModels.CtrolProveedores;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Condominios.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly ProveedorService _service;
        private ProveedoresViewModel _viewModel = new();

        public ProveedoresController(ProveedorService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<ActionResult> Index()
        {
            ProveedoresViewModel model = await _service.Listas();
            return View(model);
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> Agregar(ProveedoresViewModel model)
        {
            await _service.AddProveedor(model);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> ObtenerRegistro(int id)
        {
            Proveedor model = await _service.GetEquipo(id);

            var jsonResult = new JsonResult(model);
            return jsonResult;
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> Actualizar(ProveedoresViewModel model)
        {
            await _service.Update(model);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task UpdateStatus(int id)
        {
            await _service.ActualizarEstado(id);
        }

    }
}
