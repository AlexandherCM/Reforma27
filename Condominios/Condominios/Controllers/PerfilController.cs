using Condominios.Models.Services;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Perfil;
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

        public async Task<IActionResult> Index()
        {
            string json = string.Empty;
            PerfilViewModel model = await _Service.GetAdmin();

            if (TempData["AlertaJS"] != null)
            {
                json = (string)TempData["AlertaJS"];
                model.AlertaEstado = JsonConvert.DeserializeObject<AlertaEstado>(json);
            }
            return View(model);
        }

        public async Task<IActionResult> UpdateAdmin(PerfilViewModel model)
        {
            var admin = await _Service.UpdateUser(model);

            model.AlertaEstado = await _Service.UpdateUser(model);
            TempData["AlertaJS"] = JsonConvert.SerializeObject(model.AlertaEstado);
            return RedirectToAction(nameof(Index));

        }
    }
}
