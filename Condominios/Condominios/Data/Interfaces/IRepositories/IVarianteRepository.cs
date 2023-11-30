using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface IVarianteRepository<Entidad>
    {
        public Task<List<Entidad>> GetNormalList();
        public Task<List<Entidad>> GetSpecialList();
        //public void Add();
        //public Entidad GetById(int id);
        //public void Update(Entidad entity);
        //public void Delete(Entidad entity);
    }
}
