using Condominios.Data;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Condominios.Services
{
    public class VarianteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private VarianteViewModel _model = new();

        public VarianteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VarianteViewModel> Listas()
        {
            _model.Marcas = new SelectList(await _unitOfWork.MarcaRepository.GetList(), "ID", "Nombre");
            _model.Motores = new SelectList(await _unitOfWork.MotorRepository.GetList(), "ID", "Nombre");
            _model.Periodos = new SelectList(await _unitOfWork.PeriodoRepository.GetList(), "ID", "Nombre");
            _model.TipoEquipo = new SelectList(await _unitOfWork.TipoEquipoRepository.GetList(), "ID", "Nombre");
            _model.Capacidad = new SelectList(await _unitOfWork.UnidadMedidaRepository.GetList(), "ID", "Nombre");
            //_model.Variantes = new await _unitOfWork.VarianteRepository.GetList();
            return _model;
        }
    }
}
