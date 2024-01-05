using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Catalogos;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface ICatalogoRepository<Entidad>
    {
        public Task<Entidad?> GetById(int id);
        public void UpdateEstateById(int id);
        public Task<List<Entidad>> GetList();
        public Task<List<Entidad>> GetActiveList(); 
        public Task<AlertaEstado> add(CatalogoViewModel viewModel);
        public Task<AlertaEstado> Update(CatalogoViewModel viewModel);
        public void Delete(Entidad entity);
    }
}
