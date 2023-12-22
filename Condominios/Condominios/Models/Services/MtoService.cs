using Condominios.Data;
using Condominios.Data.Interfaces;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Models.ViewModels.CtrolMantenimientos;
using Humanizer;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
#pragma warning disable CS8603
#pragma warning disable CS8601
#pragma warning disable CS8602
#pragma warning disable CS8619

namespace Condominios.Models.Services
{
    public class MtoService
    {
        private readonly IUnitOfWork _unitOfWork;

        private CtrolMtosEquipoViewModels _viewModelMtos = new();
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

        public (List<MtoProgramadoViewModel>, Dictionary<string, string>) GetMtosApplicationStatus(string listMtos, int filter)
            => _unitOfWork.MtoRepository.Filter(listMtos, filter);
        
        public (List<MtoProgramadoViewModel>, Dictionary<string, string>) GetMtosFilters(string listMtos, FilterMtos filterMtos)
            => _unitOfWork.MtoRepository.Filter(listMtos, filterMtos);
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public async Task<AlertaEstado> ConfirmarMto(MantenimientoViewModel viewModel)  
        {
            _alertaEstado = await _unitOfWork.MtoRepository.ConfirmMto(viewModel);

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

                if(mto.Mantenimiento != null)
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
    }
}
