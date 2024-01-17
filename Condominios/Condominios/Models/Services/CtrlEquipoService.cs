using Condominios.Data;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Microsoft.AspNetCore.Mvc.Rendering;
#pragma warning disable CS8600
#pragma warning disable CS8603

namespace Condominios.Models.Services
{
    public class CtrlEquipoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private CtrlEquipoViewModel _viewModel = new();
        private AlertaEstado _alertaEstado = new();
        public CtrlEquipoService(IUnitOfWork uniOfWork)
        {
            _unitOfWork = uniOfWork;
        }

        public async Task<AlertaEstado> InsertarEquipos(CtrlEquipoViewModel model)
        {
            _alertaEstado = await _unitOfWork.EquipoRepository.Add(model);

            if (_alertaEstado.Estado)
                await _unitOfWork.Save();

            return _alertaEstado;
        }

        public async Task<AlertaEstado> ActualizarEquipo(EditEquipoViewModel model)
        {
            _alertaEstado = _unitOfWork.EquipoRepository.Update(model);

            if (_alertaEstado.Estado)
                 await _unitOfWork.Save();

            return _alertaEstado;
        }

        public async Task<List<Equipo>> GetEquipos()
            => await _unitOfWork.EquipoRepository.GetList();

        public async Task<List<Equipo>> GetEquipos(int id)
            => await _unitOfWork.EquipoRepository.GetList(id);

        public async Task<List<Equipo>> GetEquipos(string cadena)
            => await _unitOfWork.EquipoRepository.GetList(cadena);


        public async Task<List<Equipo>> GetEquipos(FiltrosDTO filtros)
            => await _unitOfWork.EquipoRepository.GetList(filtros);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public async Task<CtrlEquipoViewModel> GetLists()
            => await GetListsOnModel(_viewModel);
        public async Task<CtrlEquipoViewModel> GetLists(CtrlEquipoViewModel viewModel)
            => await GetListsOnModel(viewModel);
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        private async Task<CtrlEquipoViewModel> GetListsOnModel(CtrlEquipoViewModel viewModel)
        {
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            viewModel.TipoEquipos = new SelectList(await _unitOfWork.TipoEquipoRepository.GetActiveList(), "ID", "Nombre");
            viewModel.Variantes = new SelectList(await _unitOfWork.VarianteRepository.GetSpecialList(), "ID", "Nombre");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            viewModel.Ubicaciones = new SelectList(await _unitOfWork.UbicacionRepository.GetActiveList(), "ID", "Nombre");
            viewModel.Marcas = new SelectList(await _unitOfWork.MarcaRepository.GetActiveList(), "ID", "Nombre");
            viewModel.Motores = new SelectList(await _unitOfWork.MotorRepository.GetActiveList(), "ID", "Nombre");

            viewModel.AllEstatus = new SelectList(await _unitOfWork.EstatusRepository.GetActiveList(), "ID", "Nombre");

            var estatusList = await _unitOfWork.EstatusRepository.GetActiveList();
            estatusList = estatusList.Where(e => e.Nombre != "Fuera de servicio").ToList();

            // Crear el SelectList con la lista filtrada
            viewModel.Estatus = new SelectList(estatusList, "ID", "Nombre");

            viewModel.Equipos = await GetEquipos();
            return viewModel;
        }

        public async Task<Equipo> ActualizarEstado(int id)
        {
            Equipo equipo = await _unitOfWork.EquipoRepository.UpdateID(id);
            await _unitOfWork.Save();
            return equipo;
        }

        public async Task<Equipo> GetEquipo(int id)
            => await _unitOfWork.EquipoRepository.GetById(id);

    }
}
