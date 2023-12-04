using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Condominios.Models.ViewModels.CtrolEquipo;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface IEquipoRepository<Entidad>
    {
        public Entidad GetById(int id);
        public Task<List<Equipo>> GetList(FiltrosDTO filtros);
        public Task<List<Equipo>> GetList(string cadena);
        public Task<List<Equipo>> GetList(int id);
        public Task<List<Entidad>> GetList();
        public Task Add(CtrlEquipoViewModel viewModel);
        public void Update(Entidad entity);
        public void Delete(Entidad entity);
    }
}
