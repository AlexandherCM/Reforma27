using Microsoft.AspNetCore.Mvc;

namespace Condominios.Controllers
{
    public class MantenimientosController : Controller
    {
        public IActionResult Consultar(int id)
        {
            return View();
        }
        public IActionResult Crear()
        {
            return View();
        }
    }
}
