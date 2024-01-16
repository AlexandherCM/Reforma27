using Condominios.Data;
using Condominios.Data.Interfaces;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Models.ViewModels.CtrolGastosMantenimiento;
using Condominios.Models.ViewModels.CtrolMantenimientos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Globalization;
#pragma warning disable CS8602

namespace Condominios.Models.Services
{
    public class MtoService
    {
        private readonly IUnitOfWork _unitOfWork;

        private CtrolMtosEquipoViewModels _viewModelMtos = new();
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private CtrolGastosMantenimientoViewModel _viewModelGastosMants = new();
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private AlertaEstado _alertaEstado = new();
        private IEpoch _epoch;
        public MtoService(IUnitOfWork uniOfWork, IEpoch epoch)
        {
            _unitOfWork = uniOfWork;
            _epoch = epoch;
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public async Task GetSelectsForConsultarMto(CtrolMtosEquipoViewModels model)
        {
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            model.Ubicaciones = new SelectList(await _unitOfWork.UbicacionRepository.GetActiveList(), "ID", "Nombre");
            model.Estatus = new SelectList(await _unitOfWork.EstatusRepository.GetActiveList(), "ID", "Nombre");
            model.Proveedores = new SelectList(await _unitOfWork.ProveedorRepository.GetFormatActiveList(), "ID", "Nombre");
            model.TipoMtos = new SelectList(await _unitOfWork.TipoMtoRepository.GetActiveList(), "ID", "Nombre");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        }

        public async Task GetSelectsForConfirmarMtos(CrearMtosViewModel model)
        {
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            model.Proveedores = new SelectList(await _unitOfWork.ProveedorRepository.GetFormatActiveList(), "ID", "Nombre");
            model.TipoMtos = new SelectList(await _unitOfWork.TipoMtoRepository.GetActiveList(), "ID", "Nombre");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        }


        public (List<MtoProgramadoViewModel>, Dictionary<string, string>) GetMtosApplicationStatus(string listMtos, int filter)
            => _unitOfWork.MtoRepository.Filter(listMtos, filter);

        public (List<MtoProgramadoViewModel>, Dictionary<string, string>) GetMtosFilters(string listMtos, FilterMtos filterMtos)
            => _unitOfWork.MtoRepository.Filter(listMtos, filterMtos);
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public async Task<AlertaEstado> UpdateMtoProgrammed(MantenimientoViewModel viewModel, string rol) 
        {
            if (!rol.Equals("Administrador"))
            {
                _alertaEstado = new() 
                {
                    Estado = false,
                    Leyenda = $"Solo el administrador puede editar los mantenimientos pasados."
                };

                return _alertaEstado;
            }

            _alertaEstado = await _unitOfWork.MtoRepository.UpdateMto(viewModel);

            if (_alertaEstado.Estado)
                await _unitOfWork.Save();

            return _alertaEstado;
        }
        public async Task<AlertaEstado> ConfirmarMto(MantenimientoViewModel viewModel)
        {
            _alertaEstado = await _unitOfWork.MtoRepository.ConfirmarMto(viewModel);

            if (_alertaEstado.Estado)
                await _unitOfWork.Save();

            return _alertaEstado;
        }

        public async Task<AlertaEstado> ConfirmarMtos(MantenimientoViewModel viewModel, List<EquipoMtoViewModel> Equipos)
        {
            _alertaEstado = await _unitOfWork.MtoRepository.ConfirmarMtos(viewModel, Equipos);

            if (_alertaEstado.Estado)
                await _unitOfWork.Save();

            return _alertaEstado;
        }

        public CrearMtosViewModel CreateMtosViewModel(CrearMtosViewModel model, string Json)
        {
            List<Equipo> equipos = JsonConvert.DeserializeObject<List<Equipo>>(Json) ?? new();

            equipos.ForEach(equipo =>
            {
                DateTime DateUltima = _epoch.ObtenerFecha(equipo.Programados.FirstOrDefault(c => c.Estado == true)?.UltimaAplicacion ?? new());
                DateTime DateProxima = _epoch.ObtenerFecha(equipo.Programados.FirstOrDefault(c => c.Estado == true)?.ProximaAplicacion ?? new());

                EquipoMtoViewModel Viewodel = new()
                {
                    NumSerie = equipo.NumSerie,
                    Marca = equipo.Variante.Marca?.Nombre ?? "",
                    TipoEquipo = equipo.Variante.TipoEquipo?.Nombre ?? "",
                    UltimaAplicion = _epoch.ObtenerMesYAnio(DateUltima),
                    ProximaAplicion = _epoch.ObtenerMesYAnio(DateProxima),
                    Programado = equipo.Programados.First(c => c.Estado == true)
                };

                model.equipos.Add(Viewodel);
            });

            return model;
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public async Task<CtrolMtosEquipoViewModels> GetEquipo(int id)
        {
            _viewModelMtos.Equipo = await _unitOfWork.EquipoRepository.GetById(id);
            _viewModelMtos.Plantilla = new()
            {
                UbicacionID = _viewModelMtos.Equipo.UbicacionID,
                EstatusID = _viewModelMtos.Equipo.EstatusID,
                Funcion = _viewModelMtos.Equipo.Funcion,
                NumSerie = _viewModelMtos.Equipo.NumSerie,
                CostoAdquisicion = _viewModelMtos.Equipo.CostoAdquisicion
            };

            return _viewModelMtos;
        }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public async Task<CtrolMtosEquipoViewModels> GetListsForConsultarMtos(CtrolMtosEquipoViewModels model)
        {
            CultureInfo cultureInfo = new CultureInfo("es-MX");
            await GetSelectsForConsultarMto(model);

            List<MtoProgramado> Mtos = await _unitOfWork.MtoRepository.GetListMtosProgramByID(model.Equipo.ID);

            Mtos.ForEach(mto =>
            {
                string estado = string.Empty;
                string fechaTexto = string.Empty;

                if (mto.Estado && !mto.Aplicado)
                    estado = model.estados[1];
                else if (!mto.Estado && mto.Aplicado)
                    estado = model.estados[2];
                else if (!mto.Estado && !mto.Aplicado)
                    estado = model.estados[3];

                if (mto.Mantenimiento != null)
                    fechaTexto = _epoch.ObtenerFecha(mto.Mantenimiento.FechaAplicacion).ToLongDateString();

                MtoProgramadoViewModel viewModel = new()
                {
                    MantenimientoID = mto.ID,
                    NumSerieEquipo = mto.Equipo.NumSerie,
                    ProxAplicEpoch = mto.ProximaAplicacion,
                    UltimaAplicacion =
                        _epoch.ObtenerMesYAnio(_epoch.ObtenerFecha(mto.UltimaAplicacion)),
                    ProximaAplicacion =
                        _epoch.ObtenerMesYAnio(_epoch.ObtenerFecha(mto.ProximaAplicacion)),
                    DiaDeAplicacion = mto.Mantenimiento != null
                                      ? char.ToUpper(fechaTexto[0]) + fechaTexto.Substring(1) : "-",
                    DiaDeAplicacionEpoch = mto.Mantenimiento != null
                                         ? mto.Mantenimiento.FechaAplicacion : 0,

                    TipoMantenimiento = mto.Mantenimiento != null ? mto.Mantenimiento.TipoMantenimiento.Nombre : "-",
                    TipoMantenimientoID = mto.Mantenimiento != null ? mto.Mantenimiento.TipoMantenimientoID : 0,
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                    EstadoAplicacion = estado,
                    Pendiente = mto.Estado && !mto.Aplicado,
                    //Aplicable = mto.Aplicado, 
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                    DatosProveedor = new()
                    {
                        Empresa = mto.Mantenimiento != null ? mto.Mantenimiento.Proveedor.Empresa : "",
                        Servicio = mto.Mantenimiento != null ? mto.Mantenimiento.Proveedor.Servicio : "",
                        Contacto = mto.Mantenimiento != null ? mto.Mantenimiento.Proveedor.Contacto : ""
                    },

                    Observaciones = mto.Mantenimiento != null
                              ? mto.Mantenimiento.Observaciones : "-",
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                    ProveedorID = mto.Mantenimiento != null
                              ? mto.Mantenimiento.Proveedor.ID : 0,
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                    CostoReparacion = mto.Mantenimiento != null
                              ? mto.Mantenimiento.CostoReparacion.ToString("C", cultureInfo) : 0.ToString("C", cultureInfo),
                    CostoMantenimiento = mto.Mantenimiento != null
                              ? mto.Mantenimiento.CostoMantenimiento.ToString("C", cultureInfo) : 0.ToString("C", cultureInfo),
                };

                model.MtosProgramados.Add(viewModel);
            });

            model.MtosProgramados = model.MtosProgramados.OrderByDescending(c => c.ProxAplicEpoch).ToList();

            model.TotalGtosMto = Mtos.Sum(m => m.Mantenimiento != null ? m.Mantenimiento.CostoMantenimiento : 0).ToString("C", cultureInfo);
            model.TotalGtosRep = Mtos.Sum(m => m.Mantenimiento != null ? m.Mantenimiento.CostoReparacion : 0).ToString("C", cultureInfo);

            // Obtener el ID del mantenimiento programado pendiente
            model.JsonMtosProgramados = model.CreateListMtosJson();
            //model.MantenimientoID = model.GetMtoPendienteID();
            model.EquipoID = model.GetEquipoID();

            return model;
        }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        public async Task<List<ConjuntoMtosViewModel>> GetMtosPendientes()
        {
            List<Equipo> equipos = await _unitOfWork.EquipoRepository.GetListWithMtoPending();
            return _unitOfWork.MtoRepository.GetMtosPendientes(equipos);
        }


        //GTOS-MTOS - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private async Task GetSelectsForGtosMtos()
        {
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            _viewModelGastosMants.Marcas = new SelectList(await _unitOfWork.MarcaRepository.GetList(), "ID", "Nombre");
            _viewModelGastosMants.Ubicaciones = new SelectList(await _unitOfWork.UbicacionRepository.GetList(), "ID", "Nombre");
            _viewModelGastosMants.TipoEquipos = new SelectList(await _unitOfWork.TipoEquipoRepository.GetList(), "ID", "Nombre");
            _viewModelGastosMants.Motores = new SelectList(await _unitOfWork.MotorRepository.GetList(), "ID", "Nombre");
            _viewModelGastosMants.Proveedores = new SelectList(await _unitOfWork.ProveedorRepository.GetFormatList(), "ID", "Nombre");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        }

        public async Task<CtrolGastosMantenimientoViewModel> AgruparGtosMtosEquipos()
        {
            await GetSelectsForGtosMtos();
            AgruparEquiposPorTipo(await _unitOfWork.EquipoRepository.GetList());

            return _viewModelGastosMants;
        }

        public async Task<CtrolGastosMantenimientoViewModel> GtosMtosEquiposFiltrados(CtrolGastosMantenimientoViewModel model)
        {
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            List<Equipo> Equipos = await _unitOfWork.EquipoRepository.GetList(model.Filtros.ConverDateToEpoch(_epoch)); // Obtengo el primer cnjunto de filtros
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            Equipos = _unitOfWork.MtoRepository.FilterGtosMto(Equipos, model.Filtros); // Obtengo el segundo conjunto de filtros
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

            await GetSelectsForGtosMtos();
            AgruparEquiposPorTipo(Equipos);

            return _viewModelGastosMants;
        }

        private void AgruparEquiposPorTipo(List<Equipo> Equipos)
        {
            CultureInfo cultureInfo = new CultureInfo("es-MX");

            _viewModelGastosMants.ConjuntoViewModel = Equipos
                                 .GroupBy(equipo => equipo.Variante.TipoEquipo.Nombre)
                                 .Select(group => new ConjuntoViewModel
                                 {
                                     Variante = group.Key,
                                     Cantidad = group.Count(),
                                     CostoAdquisicion = 
                                        group.Sum(e => e.CostoAdquisicion).ToString("C", cultureInfo),
                                     CostoMto = 
                                        group.Sum(e => e.Programados.Sum(mto => mto.Mantenimiento?.CostoMantenimiento?? 0)).ToString("C", cultureInfo),
                                     CostoReparacion = 
                                        group.Sum(e => e.Programados.Sum(mto => mto.Mantenimiento?.CostoReparacion?? 0)).ToString("C", cultureInfo),
                                 })
                                 .ToList();

            _viewModelGastosMants.TotalCostoAdquisicion = 
                _viewModelGastosMants.ConjuntoViewModel.Sum(e => decimal.Parse(e.CostoAdquisicion.Substring(1))).ToString("C", cultureInfo);

            _viewModelGastosMants.TotalCostoMtos = 
                _viewModelGastosMants.ConjuntoViewModel.Sum(e => decimal.Parse(e.CostoMto.Substring(1))).ToString("C", cultureInfo);

            _viewModelGastosMants.TotalCostoReparacion = 
                _viewModelGastosMants.ConjuntoViewModel.Sum(e => decimal.Parse(e.CostoReparacion.Substring(1))).ToString("C", cultureInfo);
        }

    }
}
