using Condominios.Models.Entities;
using Condominios.Models.Services;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Models.ViewModels.CtrolGastosMantenimiento;
using Condominios.Models.ViewModels.CtrolMantenimientos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Newtonsoft.Json;
using System.IO;
#pragma warning disable CS8600
#pragma warning disable CS8604

namespace Condominios.Controllers
{
    public class MantenimientosController : Controller
    {
        private readonly MtoService _service;
        private EpochService _epochService = new();
        public MantenimientosController(MtoService service)
        {
            _service = service;
        }

        // CONTROLADORES-MTOS-EQUIPO- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
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
        public async Task<IActionResult> FilterByStatus(int EdoAplicacion, string JsonMtosProgramados, int EquipoID)
        {
            if (EdoAplicacion > 3)
                return RedirectToAction(nameof(Consultar), new { ID = EquipoID });

            var model = await _service.GetEquipo(EquipoID);
            await _service.GetSelects(model);

            (var ListMtos, var DictGtos) = _service.GetMtosApplicationStatus(JsonMtosProgramados, EdoAplicacion);

            model.MtosProgramados = ListMtos;
            model.TotalGtosMto = DictGtos["GtosMto"];
            model.TotalGtosRep = DictGtos["GtosRep"];

            return View(nameof(Consultar), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FilterByTime(FilterMtos filterMtos, string JsonMtosProgramados, int EquipoID)
        {
            var model = await _service.GetEquipo(EquipoID);
            await _service.GetSelects(model);

            (var ListMtos, var DictGtos) = _service.GetMtosFilters(JsonMtosProgramados, filterMtos);

            model.MtosProgramados = ListMtos;
            model.TotalGtosMto = DictGtos["GtosMto"];
            model.TotalGtosRep = DictGtos["GtosRep"];

            return View(nameof(Consultar), model);
        }

        //public async Task<IActionResult> UpdateOneMto(CtrolMtosEquipoViewModels viewModel)
        //{
        //    return RedirectToAction(nameof(Consultar), new { ID = viewModel.EquipoID });
        //}

        // CONTROLADORES CREAR-MTOS - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        public async Task<IActionResult> Pendientes()
        {
            string json = string.Empty;
            MtosPendientesViewModel model = new();

            if (TempData["AlertaJS"] != null)
            {
                json = (string)TempData["AlertaJS"];
                model.AlertaEstado = JsonConvert.DeserializeObject<AlertaEstado>(json);
            }

            model.Conjuntos = await _service.GetMtosPendientes();
            return View(model);
        }

        public async Task<IActionResult> ConfirmarMtos(string Json)
        {
            CrearMtosViewModel model = new();

            await _service.GetSelectsForConfirmarMtos(model);
            model = _service.CreateMtosViewModel(model, Json);

            return View(model);
        }

        public async Task<IActionResult> CrearMtos(MantenimientoViewModel Mantenimiento, string JsonEquipos)
        {
            var equipos = JsonConvert.DeserializeObject<List<EquipoMtoViewModel>>(JsonEquipos) ?? new();
            AlertaEstado alertaEstado = new();

            if (equipos.Count == 0)
            {
                alertaEstado.Leyenda = "No habia ningún equipo seleccionado";
                alertaEstado.Estado = false;

                TempData["AlertaJS"] = JsonConvert.SerializeObject(alertaEstado);
                return RedirectToAction(nameof(Pendientes));
            }

            alertaEstado = await _service.ConfirmarMtos(Mantenimiento, equipos);
            TempData["AlertaJS"] = JsonConvert.SerializeObject(alertaEstado);

            return RedirectToAction(nameof(Pendientes));
        }

        // Carlos CONTROLADORES-GTOS-MTOS- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public async Task<IActionResult> GastosMantenimiento()
        {
            CtrolGastosMantenimientoViewModel model = await _service.AgruparGtosMtosEquipos();
            return View(model);
        }

        public async Task<IActionResult> BusquedaFiltros(CtrolGastosMantenimientoViewModel model, string boton)
        {
            if (boton == "Todos")
                return RedirectToAction(nameof(GastosMantenimiento));

            model = await _service.GtosMtosEquiposFiltrados(model);
            return View(nameof(GastosMantenimiento), model);
        }

    }
}
