using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.CtrolProveedores;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface IProveedoreRepository<Entidad>
    {
        public void Add(ProveedoresViewModel model);
        public Task<List<Entidad>> GetList();
        public Task<Proveedor?> GetById(int id);
        public void Update(ProveedoresViewModel model);
        public Task<Proveedor?> UpdateID(int id);
    }
}
