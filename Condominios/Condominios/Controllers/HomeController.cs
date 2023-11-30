using Condominios.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Condominios.Controllers
{
    public class HomeController : Controller
    {
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        //[Authorize(Roles = "Administrador")]
        public IActionResult Equipos()
        {
            return RedirectToAction("Index", "Equipos");
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        //[Authorize(Roles = "Administrador")]
        public IActionResult Usuarios()
        {
            return View();
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        //[Authorize(Roles = "Administrador, General")]
        public IActionResult Catalogos()
        {
            return RedirectToAction("Index", "Catalogos");
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        //[Authorize(Roles = "Administrador, General")]
        public IActionResult Privacy()
        {
            return View();
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    }
}