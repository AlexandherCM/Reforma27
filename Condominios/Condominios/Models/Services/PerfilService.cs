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

        public async Task<PerfilViewModel> GetUsers()
        {
            _viewModel.Admin = await _unitOfWork.PerfilRepository.GetAdmin();
            _viewModel.Usuarios = await _unitOfWork.PerfilRepository.GetUsuarios();

            return _viewModel;  
        }

        public async Task<AlertaEstado> UpdateUser(PerfilViewModel model)
        {
            var UserUpdate = await _unitOfWork.PerfilRepository.Update(model);
            await _unitOfWork.Save();
            return UserUpdate;
        }

        public async Task Acceso(int id)
        {
            _unitOfWork.PerfilRepository.Acceso(id);
            await _unitOfWork.Save();
            return;
        }

        public async Task<bool> Delete(int id)
        {
            var borrado = await _unitOfWork.PerfilRepository.Delete(id);
            if (borrado)
            {
                await _unitOfWork.Save();
            }
            return borrado;
        }

        public async Task<PerfilViewModel> GetUsuario(int id)
        {
            _viewModel.User = await _unitOfWork.PerfilRepository.GetUser(id);
            return _viewModel;
        }

    }
}
