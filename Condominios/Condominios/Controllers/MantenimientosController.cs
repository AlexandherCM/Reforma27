using Condominios.Models.ViewModels.Catalogos;
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
        public IActionResult GastosMantenimiento()
        {
            return View();
        }

        public async Task<IActionResult> GetMtoProgramado() 
        {
            //await _service.Update(model);
            var jsonResult = new JsonResult("Hola");
            return jsonResult;
        }
    }
}
