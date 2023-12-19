using Condominios.Data;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Models.ViewModels.CtrolMantenimientos;
using Microsoft.AspNetCore.Mvc.Rendering;
#pragma warning disable CS8603
#pragma warning disable CS8601
#pragma warning disable CS8602

namespace Condominios.Models.Services
{
    public class MtoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private MantenimientosViewModel _viewModel = new();
        private CtrolMtosEquipoViewModels _mtosEquipoviewModel = new(); 
        public MtoService(IUnitOfWork uniOfWork)
        {
            _unitOfWork = uniOfWork;
        }

        //public async Task<MtoProgramado> GetMtoProgramado(int ID)
        //    => await _unitOfWork.MtoRepository.GetMtoProgramado(ID);

        public async Task<CtrolMtosEquipoViewModels> GetEquipo(int id)
        {
            _mtosEquipoviewModel.Equipo = await _unitOfWork.EquipoRepository.GetById(id);
            _mtosEquipoviewModel.Plantilla = new()
            {
                UbicacionID = _mtosEquipoviewModel.Equipo.UbicacionID,
                EstatusID = _mtosEquipoviewModel.Equipo.EstatusID,
                Funcion = _mtosEquipoviewModel.Equipo.Funcion,
                NumSerie = _mtosEquipoviewModel.Equipo.NumSerie,
                CostoAdquisicion = _mtosEquipoviewModel.Equipo.CostoAdquisicion
            };

            return _mtosEquipoviewModel;
        }

        public async Task<CtrolMtosEquipoViewModels> GetLists(CtrolMtosEquipoViewModels model)
        {
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            model.Ubicaciones = new SelectList(await _unitOfWork.UbicacionRepository.GetList(), "ID", "Nombre");
            model.Estatus = new SelectList(await _unitOfWork.EstatusRepository.GetList(), "ID", "Nombre");
            model.Proveedores = new SelectList(await _unitOfWork.ProveedorRepository.GetList(), "ID", "Nombre");
            model.TipoMtos = new SelectList(await _unitOfWork.TipoMtoRepository.GetList(), "ID", "Nombre");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            model.MtosProgramados = await _unitOfWork.MtoRepository.GetListMtosByID(model.Equipo.ID);

            return model;
        }

    }
}
