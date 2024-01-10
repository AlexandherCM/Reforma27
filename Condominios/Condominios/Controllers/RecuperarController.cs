using Microsoft.AspNetCore.Mvc;

namespace Condominios.Controllers
{
    public class RecuperarController : Controller
    {
        public IActionResult Correo()
        {
            return View();
        }
        public IActionResult Contraseña()
        {
            return View();
        }
        public IActionResult NuevosUsuarios()
        {
            return View();
        }
    }
}
