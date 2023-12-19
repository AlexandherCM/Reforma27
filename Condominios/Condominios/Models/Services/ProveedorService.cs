using Condominios.Data;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.CtrolProveedores;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Condominios.Models.Services
{
    public class ProveedorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ProveedoresViewModel _viewModel = new();

        public ProveedorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProveedoresViewModel> Listas()
        {
            _viewModel.Proveedor = new List<Proveedor>(await _unitOfWork.ProveedorRepository.GetList());
            return _viewModel;
        }

        public async Task AddProveedor(ProveedoresViewModel model)
        {
            _unitOfWork.ProveedorRepository.Add(model);
            await _unitOfWork.Save();
        }

        public async Task<Proveedor> GetEquipo(int id)
        {
            Proveedor variante = await _unitOfWork.ProveedorRepository.GetById(id);
            return variante;
        }

        public async Task Update(ProveedoresViewModel model)
        {
            _unitOfWork.ProveedorRepository.Update(model);
            await _unitOfWork.Save();
        }

        public async Task<Proveedor> ActualizarEstado(int id)
        {
            Proveedor proveedor = await _unitOfWork.ProveedorRepository.UpdateID(id);
            await _unitOfWork.Save();
            return proveedor;
        }
    }
}
