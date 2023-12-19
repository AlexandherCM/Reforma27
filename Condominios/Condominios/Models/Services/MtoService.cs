using Condominios.Data;
using Condominios.Data.Interfaces;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Models.ViewModels.CtrolMantenimientos;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private List<CtrolMtosEquipoViewModels> _listViewModelMtos = new(); 
        private IEpoch _epoch; 
        public MtoService(IUnitOfWork uniOfWork, IEpoch epoch)
        {
            _unitOfWork = uniOfWork;
            _epoch = epoch;
        }

        //public async Task<MtoProgramado> GetMtoProgramado(int ID)
        //    => await _unitOfWork.MtoRepository.GetMtoProgramado(ID);

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
                MtoProgramadoViewModel viewModel = new()
                {
                    ID = mto.ID,
                    ProxAplicEpoch = mto.ProximaAplicacion,
                    UltimaAplicacion = 
                        _epoch.ObtenerMesYAnio(_epoch.ObtenerFecha(mto.UltimaAplicacion)),
                    ProximaAplicacion = 
                        _epoch.ObtenerMesYAnio(_epoch.ObtenerFecha(mto.ProximaAplicacion)),
                };
            });

            return model;
        }

    }
}
