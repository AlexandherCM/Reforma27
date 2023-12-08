using Condominios.Models.ViewModels.Catalogos;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface ICatalogoRepository<Entidad>
    {
        public Task<Entidad?> GetById(int id);
        public void UpdateEstateById(int id);
        public Task<List<Entidad>> GetList();
        public void Add(CatalogoViewModel viewModel);
        public void Update(Entidad entity);
        public void Delete(Entidad entity);
    }
}
