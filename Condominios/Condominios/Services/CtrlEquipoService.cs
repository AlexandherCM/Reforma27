using Condominios.Data;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.CtrolEquipo;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Condominios.Services
{
    public class CtrlEquipoService
    {
        private readonly IUnitOfWork _uniOfWork;
        private CtrlEquipoViewModel _viewModel = new();
        public CtrlEquipoService(IUnitOfWork uniOfWork)
        {
            _uniOfWork = uniOfWork;
        }

        public async Task InsertarEquipos(CtrlEquipoViewModel model)
        {
            await _uniOfWork.EquipoRepository.Add(model);
            await _uniOfWork.Save();
        }

        public async Task<List<Equipo>> GetEquipos()
            => await _uniOfWork.EquipoRepository.GetList();
        
        public async Task<List<Equipo>> GetEquipos(FiltrosDTO filtros)
            => await _uniOfWork.EquipoRepository.GetList(filtros);

        public async Task<CtrlEquipoViewModel> GetLists() 
        {
            _viewModel.Estados = new SelectList(await _uniOfWork.EstatusRepository.GetList(), "ID", "Nombre");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            _viewModel.TipoEquipos = new SelectList(await _uniOfWork.TipoEquipoRepository.GetList(), "ID", "Nombre");
            _viewModel.Variantes = new SelectList(await _uniOfWork.VarianteRepository.GetSpecialList(), "ID", "Nombre");
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            _viewModel.Ubicaciones = new SelectList(await _uniOfWork.UbicacionRepository.GetList(), "ID", "Nombre");
            _viewModel.Marcas = new SelectList(await _uniOfWork.MarcaRepository.GetList(), "ID", "Nombre");
            _viewModel.Motores = new SelectList(await _uniOfWork.MotorRepository.GetList(), "ID", "Nombre");

            _viewModel.Estatus = new SelectList(await _uniOfWork.EstatusRepository.GetList(), "ID", "Nombre");

            _viewModel.Equipos = await GetEquipos();
            return _viewModel;
        }

    }
}
