using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.CtrolProveedores;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface IProveedorRepository<Entidad>
    {
        public Task<Proveedor?> GetById(int id);
        public Task<Proveedor?> UpdateID(int id);
        public Task<List<Entidad>> GetList();
        public Task<List<Entidad>> GetActiveList();
        public void Add(ProveedoresViewModel model);
        public void Update(ProveedoresViewModel model);
    }
}
