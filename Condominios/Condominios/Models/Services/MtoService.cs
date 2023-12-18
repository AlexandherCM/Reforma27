using Condominios.Data;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Models.ViewModels.CtrolMantenimientos;
using Condominios.Models.ViewModels.CtrolProveedores;

namespace Condominios.Models.Services
{
    public class MtoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private MantenimientosViewModel _viewModel = new();
        public MtoService(IUnitOfWork uniOfWork)
        {
            _unitOfWork = uniOfWork;
        }

        public async Task<MtoProgramado> GetMtoProgramado(int ID)
            => await _unitOfWork.MtoRepository.GetMtoProgramado(ID);

        public async Task<MantenimientosViewModel> Listas()
        {
            _viewModel.Marcas = new List<Marca>(await _unitOfWork.MarcaRepository.GetList());
            return _viewModel;
        }
    }
}
