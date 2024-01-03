using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Models.ViewModels.CtrolMantenimientos;
using System.Linq;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface IMtoRepository
    {
        public MtoProgramado CrearObjeto(DateTime UltimaAplicacion, int meses);
        public Task CreateNewMtoProgram();

        public Task<AlertaEstado> ConfirmarMto(MantenimientoViewModel model);
        public Task<AlertaEstado> ConfirmarMtos(MantenimientoViewModel viewModel, List<EquipoMtoViewModel> equipos);

        public Task<List<Mantenimiento>> GetList();
        public Task<List<MtoProgramado>> GetListMtosProgramByID(int ID);      

        public (List<MtoProgramadoViewModel>, Dictionary<string, string>) Filter(string json, int filter);       
        public (List<MtoProgramadoViewModel>, Dictionary<string, string>) Filter(string json, FilterMtos filters);
        public List<ConjuntoMtosViewModel> GetMtosPendientes(List<Equipo> equipos);
        public List<Equipo> FilterGtosMto(List<Equipo> ListaEquipos, FiltrosGtosMtosDTO Filtros); 
    }
} 
 