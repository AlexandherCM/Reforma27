﻿using Condominios.Models.ViewModels.Catalogos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Condominios.Models.Services;
using Condominios.Models.Entities;
using Newtonsoft.Json;
using Condominios.Models.Services.Classes;

namespace Condominios.Controllers
{
    public class CatalogosController : Controller
    {
        private readonly CatalogoService _service;
        public CatalogosController(CatalogoService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> Index()
        {
            CatalogoViewModel model = await _service.ObtenerCatalogos();
            return View(model);
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task UpdateById([FromBody] CatalogoViewModel model)
        {
            await _service.ActualizarEstado(model);
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<CatalogoViewModel> Create([FromBody] CatalogoViewModel model)
        {
            var alerta = await _service.InsertarEntidad(model);
            CatalogoViewModel json = await _service.ObtenerCatalogos();
            json.AlertaEstado = alerta;
            return json;
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> ObtenerRegistro()
        {
            CatalogoViewModel model = await _service.ObtenerCatalogos();

            var jsonResult = new JsonResult(model);
            return jsonResult;
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> Update([FromBody] CatalogoViewModel model)
        {
            var alerta = await _service.Update(model);
            model.AlertaEstado = alerta;
            var jsonResult = new JsonResult(model);
            return jsonResult;
        }
    }
}
