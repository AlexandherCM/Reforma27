using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Catalogos;
using Condominios.Models.ViewModels.CtrolEquipo;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface IEquipoRepository<Entidad>
    {
        public Task<Equipo?> GetById(int id);
        public Task<List<Equipo>> GetList(FiltrosDTO filtros);
        public Task<List<Equipo>> GetList(string cadena);
        public Task<List<Equipo>> GetList(int id);
        public Task<List<EquipoReplaceDTO>> GetListReplace(int varianteID, string NumeSerie);  
        public Task<List<Entidad>> GetList();
        public Task<List<Entidad>> GetListWithMtoPending(); 
        public Task<AlertaEstado> Add(CtrlEquipoViewModel viewModel);
        public AlertaEstado Update(EditEquipoViewModel model);
        public void Delete(Entidad entity);
        public Task<Equipo?> UpdateID(int id);
        public Task<string> CalculateTimes(DateTime date, int varianteID, int inputs);
    }
}
