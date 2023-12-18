using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.AspNetCore.Mvc;

namespace Condominios.Controllers
{
    public class MantenimientosController : Controller
    {
        private readonly IMtoRepository _service;
        public MantenimientosController(IMtoRepository service)
        {
            _service = service;
        }
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
        public IActionResult Pendientes()
        {
            return View();
        }

        public async Task<IActionResult> GetMtoProgramado(int ID) 
        {
            MtoProgramado mto = await _service.GetMtoProgramado(ID);
            return new JsonResult(mto);
        }
    }
}
