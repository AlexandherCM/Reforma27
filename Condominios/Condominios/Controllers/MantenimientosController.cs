using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Condominios.Models.ViewModels.CtrolMantenimientos;
using Microsoft.AspNetCore.Mvc;

namespace Condominios.Controllers
{
    public class MantenimientosController : Controller
    {
        private readonly IMtoRepository _service;


        private MantenimientosViewModel _model = new();

        public MantenimientosController(IMtoRepository service)
        {
            _service = service;
        }
        public IActionResult Consultar(int id)
        {
            return View();
        }
        public async IActionResult Crear()
        {
            MantenimientosViewModel model = await _service.Listas();
            return View(model);
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
