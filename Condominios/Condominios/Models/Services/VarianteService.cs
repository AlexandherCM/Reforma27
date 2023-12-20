using Condominios.Data;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;
using Microsoft.AspNetCore.Mvc.Rendering;
#pragma warning disable CS8600 

namespace Condominios.Services
{
    public class VarianteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private VarianteViewModel _model = new();
        private AlertaEstado _alertaEstado = new();

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
            _model.Variantes = new List<Variante>(await _unitOfWork.VarianteRepository.GetList()); 
            return _model;
        }

        public async Task<AlertaEstado> AddEquipo(VarianteViewModel model)
        {
            UnidadMedida capacidadString = await _unitOfWork.UnidadMedidaRepository.GetById(model.VarianteEquipo.CapacidadSelect);
            _alertaEstado = await _unitOfWork.VarianteRepository.Add(model, capacidadString?.Nombre ?? string.Empty);

            if (_alertaEstado.Estado)
            {
                await _unitOfWork.Save();
            }
            return _alertaEstado;
        }

        public async Task<Variante> GetEquipo(int id)
        {
            Variante variante = await _unitOfWork.VarianteRepository.GetById(id);
            return variante;
        }

        public async Task<AlertaEstado> Update(VarianteViewModel model)
        {
            UnidadMedida capacidadString = await _unitOfWork.UnidadMedidaRepository.GetById(model.VarianteEquipo.CapacidadSelect);
            _alertaEstado = await _unitOfWork.VarianteRepository.Update(model, capacidadString?.Nombre ?? string.Empty);
            if (_alertaEstado.Estado)
            {
                await _unitOfWork.Save();
            }
            return _alertaEstado;
        }

        public async Task<Variante> ActualizarEstado(int id)
        {
            Variante variante = await _unitOfWork.VarianteRepository.UpdateID(id);
            await _unitOfWork.Save();   
            return variante;
        }
    }
}
    