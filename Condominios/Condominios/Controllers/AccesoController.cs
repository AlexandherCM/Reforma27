using Microsoft.AspNetCore.Mvc;
using Condominios.Models.ViewModels;
using Condominios.Models.Services;
using Condominios.Models.Entities;
using Condominios.Models.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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
            ViewBag.Sesion = TempData["Sesion"];
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        public IActionResult Recuperar()
        {
            return View();
        }

        public IActionResult CambiarPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(UsuarioDTO user)
        {
            if (user.Password == user.ConfPassword)
            {
                var Registrado = await _service.UsuarioExistente(user, HttpContext);
                ViewBag.Mensaje = Registrado;
                return View();
            }
            ViewBag.MensajeClave = "Las contraseñas no coinciden";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Recuperar(UsuarioDTO user)
        {
            var Recuperado = await _service.RestablecerCuenta(user, HttpContext);
            ViewBag.Recuperado = Recuperado;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarPassword(string token, UsuarioDTO user)
        {
            if (user.Password == user.ConfPassword)
            {
                var Cambio = await _service.CambiarPass(token, user);
                ViewBag.Cambiopas = Cambio;
                return View();
            }
            ViewBag.Cambio = "Las contraseñas no coinciden";
            return View();
        }

        public async Task<IActionResult> Confirmar(string token)
        {
            var Confirmado = await _service.ConfirmarUsuario(token, HttpContext);
            ViewBag.Confirmado = Confirmado;
            return View();
        }


        public async Task<IActionResult> ValidarAcceso(SesionViewModel sesion)
        {
            var Acceso = await _service.IniciarSesion(sesion, HttpContext);
            if (Acceso == "ok")
            {
                return RedirectToAction("Pendientes", "Home");
            }
            TempData["Sesion"] = Acceso;
            return RedirectToAction(nameof(Login));
        }

        [Authorize(Roles = "Administrador, General")]
        public async Task<IActionResult> Logout()
        {
            await _service.CerrarSesion(HttpContext);
            return RedirectToAction(nameof(Login));
        }

    }
}
