using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Models.ViewModels.CtrolMantenimientos;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface IMtoRepository
    {
        public MtoProgramado CrearObjeto(DateTime UltimaAplicacion, int meses);
        public Task CreateNewMtoProgram();
        public Task<List<MtoProgramado>> GetListMtosProgramByID(int ID);  
        public Task<AlertaEstado> ConfirmMto(MantenimientoViewModel model);    

        public (List<MtoProgramadoViewModel>, Dictionary<string, string>) Filter(string json, int filter);       
        public (List<MtoProgramadoViewModel>, Dictionary<string, string>) Filter(string json, FilterMtos filters);
        public List<ConjuntoMtosViewModel> GetMtosPendientes(List<Equipo> equipos);
    }
} 
 