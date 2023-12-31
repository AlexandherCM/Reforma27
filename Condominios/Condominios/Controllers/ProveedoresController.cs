﻿using Condominios.Models.Entities;
using Condominios.Models.Services;
using Condominios.Models.ViewModels.CtrolProveedores;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<ActionResult> Index()
        {
            ProveedoresViewModel model = await _service.Listas();
            return View(model);
        }

        public async Task<IActionResult> Agregar(ProveedoresViewModel model)
        {
            await _service.AddProveedor(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ObtenerRegistro(int id)
        {
            Proveedor model = await _service.GetEquipo(id);

            var jsonResult = new JsonResult(model);
            return jsonResult;
        }

        public async Task<IActionResult> Actualizar(ProveedoresViewModel model)
        {
            await _service.Update(model);
            return RedirectToAction("Index");
        }

        public async Task UpdateStatus(int id)
        {
            await _service.ActualizarEstado(id);
        }

    }
}
