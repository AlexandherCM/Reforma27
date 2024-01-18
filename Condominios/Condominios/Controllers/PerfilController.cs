using Condominios.Models.Entities;
using Condominios.Models.Services;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Catalogos;
using Condominios.Models.ViewModels.Perfil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
#pragma warning disable CS8600

namespace Condominios.Controllers
{
    public class PerfilController : Controller
    {
        private readonly PerfilService _Service;

        public PerfilController(PerfilService service)
        {
            _Service = service;
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> Index()
        {
            string json = string.Empty;
            PerfilViewModel model = await _Service.GetUsers();

            if (TempData["AlertaJS"] != null)
            {
                json = (string)TempData["AlertaJS"];
                model.AlertaEstado = JsonConvert.DeserializeObject<AlertaEstado>(json);
            }
            return View(model);
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> UpdateAdmin(PerfilViewModel model)
        {
            var admin = await _Service.UpdateUser(model);

            model.AlertaEstado = await _Service.UpdateUser(model);
            TempData["AlertaJS"] = JsonConvert.SerializeObject(model.AlertaEstado);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task Acceso(int id)
        {
            await _Service.Acceso(id);
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> Delete(int id)
        {
            await _Service.Delete(id);

            return Ok(new { success = true });
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> UpdateUser(PerfilViewModel model)
        {
            model.AlertaEstado = await _Service.UpdateUser(model);
            TempData["AlertaJS"] = JsonConvert.SerializeObject(model.AlertaEstado);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> ObtenerRegistro(int id)
        {
            PerfilViewModel model = await _Service.GetUsuario(id);

            var jsonResult = new JsonResult(model);
            return jsonResult;
        }
    }
}
