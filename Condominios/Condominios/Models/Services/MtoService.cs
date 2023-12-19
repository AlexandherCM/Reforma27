using Condominios.Data;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Models.ViewModels.CtrolMantenimientos;
using Microsoft.AspNetCore.Mvc.Rendering;
#pragma warning disable CS8603

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

        public async Task<MtoProgramado> GetMtoProgramado(int ID)
            => await _unitOfWork.MtoRepository.GetMtoProgramado(ID);

        public async Task<Equipo> GetEquipo(int id)
            => await _unitOfWork.EquipoRepository.GetById(id);

        public async Task<CtrolMtosEquipoViewModels> GetLists()
        {
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            _mtosEquipoviewModel.Ubicaciones = new SelectList(await _unitOfWork.UbicacionRepository.GetList(), "ID", "Nombre");
            _mtosEquipoviewModel.Estatus = new SelectList(await _unitOfWork.EstatusRepository.GetList(), "ID", "Nombre");
            _mtosEquipoviewModel.Proveedores = new SelectList(await _unitOfWork.ProveedorRepository.GetList(), "ID", "Nombre");
            _mtosEquipoviewModel.TipoMtos = new SelectList(await _unitOfWork.TipoMtoRepository.GetList(), "ID", "Nombre");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            return _mtosEquipoviewModel;
        }


        public async Task<MantenimientosViewModel> Listas()
        {
            _viewModel.Marcas = new List<Marca>(await _unitOfWork.MarcaRepository.GetList());
            return _viewModel;
        }
    }
}
