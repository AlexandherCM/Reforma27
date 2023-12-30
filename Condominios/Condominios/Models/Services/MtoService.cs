using Condominios.Data;
using Condominios.Data.Interfaces;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Models.ViewModels.CtrolGastosMantenimiento;
using Condominios.Models.ViewModels.CtrolMantenimientos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using System.Linq;
#pragma warning disable CS8603
#pragma warning disable CS8601
#pragma warning disable CS8602

namespace Condominios.Models.Services
{
    public class MtoService
    {
        private readonly IUnitOfWork _unitOfWork;

        private CtrolMtosEquipoViewModels _viewModelMtos = new();
        private CtrolGastosMantenimientoViewModel _viewModelGastosMants = new();
        private AlertaEstado _alertaEstado = new();
        private IEpoch _epoch;
        public MtoService(IUnitOfWork uniOfWork, IEpoch epoch)
        {
            _unitOfWork = uniOfWork;
            _epoch = epoch;
        }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        public async Task GetSelects(CtrolMtosEquipoViewModels model)
        {
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            model.Ubicaciones = new SelectList(await _unitOfWork.UbicacionRepository.GetList(), "ID", "Nombre");
            model.Estatus = new SelectList(await _unitOfWork.EstatusRepository.GetList(), "ID", "Nombre");
            model.Proveedores = new SelectList(await _unitOfWork.ProveedorRepository.GetList(), "ID", "Nombre");
            model.TipoMtos = new SelectList(await _unitOfWork.TipoMtoRepository.GetList(), "ID", "Nombre");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        }

        public async Task GetSelectsForConfirmarMtos(CrearMtosViewModel model)
        {
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            model.Proveedores = new SelectList(await _unitOfWork.ProveedorRepository.GetList(), "ID", "Nombre");
            model.TipoMtos = new SelectList(await _unitOfWork.TipoMtoRepository.GetList(), "ID", "Nombre");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        } 

        public (List<MtoProgramadoViewModel>, Dictionary<string, string>) GetMtosApplicationStatus(string listMtos, int filter)
            => _unitOfWork.MtoRepository.Filter(listMtos, filter);

        public (List<MtoProgramadoViewModel>, Dictionary<string, string>) GetMtosFilters(string listMtos, FilterMtos filterMtos)
            => _unitOfWork.MtoRepository.Filter(listMtos, filterMtos);
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
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
        public async Task<CtrolMtosEquipoViewModels> GetLists(CtrolMtosEquipoViewModels model)
        {
            CultureInfo cultureInfo = new CultureInfo("es-MX");
            await GetSelects(model);

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
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                    EstadoAplicacion = estado,
                    Pendiente = mto.Estado && !mto.Aplicado,
                    //Aplicable = mto.Aplicado, 
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                    Proveedor = mto.Mantenimiento != null
                              ? mto.Mantenimiento.Proveedor.Nombre : "-",
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
            model.MtoPendienteID = model.GetMtoPendienteID();
            model.EquipoID = model.GetEquipoID();

            return model;
        }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        public async Task<List<ConjuntoMtosViewModel>> GetMtosPendientes()
        {
            List<Equipo> equipos = await _unitOfWork.EquipoRepository.GetListWithMtoPending();
            return _unitOfWork.MtoRepository.GetMtosPendientes(equipos);
        }
        public async Task<List<Equipo>> GetEquipos(FiltrosDTO filtros)
            => await _unitOfWork.EquipoRepository.GetList(filtros);



        //Carlos - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        public async Task<CtrolGastosMantenimientoViewModel> Equipos()
        {
            _viewModelGastosMants.Equipos = await _unitOfWork.EquipoRepository.GetList();
            await Listas();
            await Agrupacion();
            return _viewModelGastosMants;
        }

        public async Task<CtrolGastosMantenimientoViewModel> EquiposFiltrados(CtrolGastosMantenimientoViewModel model)
        {
            await Listas();
            _viewModelGastosMants.Equipos = model.Equipos;
            await Agrupacion();
            return _viewModelGastosMants;
        }

        private async Task Listas()
        {
            _viewModelGastosMants.Mantenimientos = await _unitOfWork.MtoRepository.GetList();
            _viewModelGastosMants.Marcas = new SelectList(await _unitOfWork.MarcaRepository.GetList(), "ID", "Nombre");
            _viewModelGastosMants.Ubicaciones = new SelectList(await _unitOfWork.UbicacionRepository.GetList(), "ID", "Nombre");
            _viewModelGastosMants.TipoEquipos = new SelectList(await _unitOfWork.TipoEquipoRepository.GetList(), "ID", "Nombre");
            _viewModelGastosMants.Motores = new SelectList(await _unitOfWork.MotorRepository.GetList(), "ID", "Nombre");
            _viewModelGastosMants.Proveedores = new SelectList(await _unitOfWork.ProveedorRepository.GetList(), "ID", "Nombre");
        }

        private async Task Agrupacion()
        {
            var equiposAgrupados = _viewModelGastosMants.Equipos
                .GroupBy(equipo => equipo.Variante.TipoEquipo.Nombre)
                .Select(group => new ConteoViewModel
                {
                    Variante = group.Key,
                    Cantidad = group.Count(),
                    CostoAd = group.Sum(e => e.CostoAdquisicion),
                    CostoM = 0,
                    CostoR = 0,
                })
                .ToList();

            //POSIBLE ERROR !!!

            var equiposMantenimientosAgrupados = _viewModelGastosMants.Equipos
                .Join(
                    _viewModelGastosMants.Mantenimientos,
                    equipo => equipo.ID,
                    mantenimiento => mantenimiento.ID,
                    (equipo, mantenimiento) => new
                    {
                        Equipo = equipo,
                        Mantenimiento = mantenimiento
                    })
                .GroupBy(
                    joined => joined.Equipo.Variante.TipoEquipo.Nombre,
                    (key, group) => new ConteoViewModel
                    {
                        Variante = key,
                        Cantidad = group.Count(),
                        CostoAd = group.Sum(e => e.Equipo.CostoAdquisicion),
                        CostoM = group.Sum(e => e.Mantenimiento.CostoMantenimiento),
                        CostoR = group.Sum(e => e.Mantenimiento.CostoReparacion)
                    })
                .ToList();

            _viewModelGastosMants.Conteo = equiposAgrupados
                .Concat(equiposMantenimientosAgrupados)
                .GroupBy(c => c.Variante)
                .Select(g => new ConteoViewModel
                {
                    Variante = g.Key,
                    Cantidad = g.First().Cantidad,
                    CostoAd = g.First().CostoAd,
                    CostoM = g.Sum(c => c.CostoM),
                    CostoR = g.Sum(c => c.CostoR),
                })
                .ToList();
            _viewModelGastosMants.CostoMTotal = _viewModelGastosMants.Conteo.Sum(e => e.CostoM);
            _viewModelGastosMants.CostoRTotal = _viewModelGastosMants.Conteo.Sum(e => e.CostoR);
            _viewModelGastosMants.CostoAdTotal = _viewModelGastosMants.Equipos.Sum(e => e.CostoAdquisicion);
        }

    }
}
