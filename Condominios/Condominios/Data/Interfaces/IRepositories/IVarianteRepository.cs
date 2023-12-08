using Condominios.Models.Entities;
using Condominios.Models.ViewModels;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface IVarianteRepository<Entidad>
    {
        public Task<List<Entidad>> GetNormalList();
        public Task<List<Entidad>> GetSpecialList();
        public void Add(VarianteViewModel model, string medida);
        public Task<List<Variante>> GetList();
        public Task<Variante?> GetById(int id);
        //public Entidad GetById(int id);
        //public void Update(Entidad entity);
        //public void Delete(Entidad entity);
    }
}
