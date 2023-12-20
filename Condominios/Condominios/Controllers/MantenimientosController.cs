using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models.Entities;
using Condominios.Models.Services;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Models.ViewModels.CtrolMantenimientos;
using Microsoft.AspNetCore.Mvc;

namespace Condominios.Controllers
{
    public class MantenimientosController : Controller
    {
        private readonly MtoService _service;
        public MantenimientosController(MtoService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Consultar(int ID)
        {
            var model = await _service.GetEquipo(ID);
            model = await _service.GetLists(model);

            return View(model);
        }
        public async Task<IActionResult> CreateOneMto(CtrolMtosEquipoViewModels model)
        {
            // Hacer lo que necesitas con el modelo

            // Redirigir a la acción Consultar manteniendo el ID
            //PDT!!!!
            return RedirectToAction(nameof(Consultar), new { ID = model.EquipoID });
        }

        public async Task<IActionResult> UpdateOneMto(CtrolMtosEquipoViewModels model)
        {
            return RedirectToAction(nameof(Consultar));
        }
        
        public async Task<IActionResult> Crear()
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

        //public async Task<IActionResult> GetMtoProgramado(int ID) 
        //{
        //    MtoProgramado mto = await _service.GetMtoProgramado(ID);
        //    return new JsonResult(mto);
        //}
    }
}
