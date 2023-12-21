using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface IMtoRepository
    {
        public MtoProgramado CrearObjeto(DateTime UltimaAplicacion, int meses);
        public Task CreateNewMtoProgram();
        //public Task<MtoProgramado> GetMtoProgramado(int ID);
        public Task<List<MtoProgramado>> GetListMtosByID(int ID);  
        public Task<AlertaEstado> ConfirmMto(MantenimientoViewModel model);    
    }
} 
