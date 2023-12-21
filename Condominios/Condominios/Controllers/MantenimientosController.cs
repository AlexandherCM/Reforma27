using Condominios.Models.Entities;
using Condominios.Models.Services;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
#pragma warning disable CS8600
#pragma warning disable CS8604

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
            string json = string.Empty;

            var model = await _service.GetEquipo(ID);
            model = await _service.GetLists(model);

            if (TempData["AlertaJS"] != null)
            {
                json = (string)TempData["AlertaJS"];
                model.AlertaEstado = JsonConvert.DeserializeObject<AlertaEstado>(json);
            }

            return View(model);
        }
        public async Task<IActionResult> CreateOneMto(CtrolMtosEquipoViewModels viewModel)
        {
            viewModel.Mantenimiento.MtoProgramadoID = viewModel.MtoPendienteID;
            viewModel.AlertaEstado = await _service.ConfirmarMto(viewModel.Mantenimiento);

            TempData["AlertaJS"] = JsonConvert.SerializeObject(viewModel.AlertaEstado);

            //return View(nameof(Consultar), new { ID = viewModel.EquipoID });
            return RedirectToAction(nameof(Consultar), new { ID = viewModel.EquipoID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchByEstateMto(int EdoAplicacion)
        {
            return RedirectToAction("Index", "Equipos");
        }

        public async Task<IActionResult> UpdateOneMto(CtrolMtosEquipoViewModels viewModel)
        {
            
            return RedirectToAction(nameof(Consultar), new { ID = viewModel.EquipoID });
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
