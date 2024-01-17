using Condominios.Data;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Catalogos;
using Condominios.Models.ViewModels.Perfil;

namespace Condominios.Models.Services
{
    public class PerfilService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PerfilViewModel _viewModel = new();

        public PerfilService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PerfilViewModel> GetAdmin()
        {
            _viewModel.Admin = await _unitOfWork.PerfilRepository.GetAdmin();

            return _viewModel;  
        }

        public async Task<AlertaEstado> UpdateUser(PerfilViewModel model)
        {
            var UserUpdate = await _unitOfWork.PerfilRepository.Update(model);
            await _unitOfWork.Save();
            return UserUpdate;
        }
    }
}
