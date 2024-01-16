using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Catalogos;
using Condominios.Models.ViewModels.Perfil;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface IPerfilRepository<Entidad>
    {
        Task<Usuario> GetAdmin();
        Task<AlertaEstado> Update(PerfilViewModel viewModel);
    }
}
