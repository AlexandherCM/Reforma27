using Microsoft.AspNetCore.Mvc;
using Condominios.Models.ViewModels;
using Condominios.Models.Services;

namespace Condominios.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AutenticarService _service;
        public AccesoController(AutenticarService service)
        {
            _service = service;
        }
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> ValidarAcceso(SesionViewModel sesion)
        {
            bool estado = await _service.IniciarSesion(sesion, HttpContext);
            if (estado != false)
            {
                return RedirectToAction("Equipos", "Home");
            }
            return RedirectToAction(nameof(Login));
        }
        public async Task<IActionResult> Logout()
        {
            await _service.CerrarSesion(HttpContext);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Usuario()
        {
            return View();
        }

    }
}
