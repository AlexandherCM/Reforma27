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

        public async Task<AlertaEstado> ConfirmarMto(MantenimientoViewModel viewModel)  
        {
            _alertaEstado = await _unitOfWork.MtoRepository.ConfirmMto(viewModel);

            if (_alertaEstado.Estado)
                await _unitOfWork.Save();

            return _alertaEstado;
        }

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

        public async Task<CtrolMtosEquipoViewModels> GetLists(CtrolMtosEquipoViewModels model)
        {
            // Configuración de la cultura para México (es-MX)
            CultureInfo cultureInfo = new CultureInfo("es-MX");

            long NowTimeEpoch = _epoch.CrearEpoch(DateTime.Now);

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            model.Ubicaciones = new SelectList(await _unitOfWork.UbicacionRepository.GetList(), "ID", "Nombre");
            model.Estatus = new SelectList(await _unitOfWork.EstatusRepository.GetList(), "ID", "Nombre");
            model.Proveedores = new SelectList(await _unitOfWork.ProveedorRepository.GetList(), "ID", "Nombre");
            model.TipoMtos = new SelectList(await _unitOfWork.TipoMtoRepository.GetList(), "ID", "Nombre");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            List<MtoProgramado> Mtos = await _unitOfWork.MtoRepository.GetListMtosByID(model.Equipo.ID);

            Mtos.ForEach(mto =>
            {
                string estado = string.Empty;
                string fechaTexto = string.Empty;

                if (mto.Estado && !mto.Aplicado)
                    estado = "Pendiente";
                else if (!mto.Estado && mto.Aplicado)
                    estado = "Aplicado";
                else if (!mto.Estado && !mto.Aplicado)
                    estado = "No Aplicado";

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
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                    Estado = estado,
                    Pendiente = mto.Estado && !mto.Aplicado,
                    Aplicable = mto.Aplicado,
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                    Proveedor = mto.Mantenimiento != null 
                              ? mto.Mantenimiento.Proveedor.Nombre : "No asignado",
                    CostoReparacion = mto.Mantenimiento != null 
                              ? mto.Mantenimiento.CostoReparacion.ToString("C", cultureInfo) : 0.ToString("C", cultureInfo),
                    CostoMantenimiento = mto.Mantenimiento != null 
                              ? mto.Mantenimiento.CostoMantenimiento.ToString("C", cultureInfo) : 0.ToString("C", cultureInfo),
                };

                model.MtosProgramados.Add(viewModel);
            });

            model.MtosProgramados = model.MtosProgramados.OrderByDescending(c => c.ProxAplicEpoch).ToList();

            model.TotasGtosMto = Mtos.Sum(m => m.Mantenimiento != null ? m.Mantenimiento.CostoMantenimiento : 0).ToString("C", cultureInfo);
            model.TotasGtosRep = Mtos.Sum(m => m.Mantenimiento != null ? m.Mantenimiento.CostoReparacion : 0).ToString("C", cultureInfo);

            // Obtener el ID del mantenimiento programado pendiente
            model.MtoPendienteID = model.GetMtoPendienteID();
            model.EquipoID = model.GetEquipoID();

            return model;
        }

    }
}
