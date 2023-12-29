using Condominios.Models.Entities;

namespace Condominios.Models.ViewModels.CtrolMantenimientos
{
    public class ConjuntoMtosViewModel
    {
        public string FormatDateAplic {  get; set; } = string.Empty;  
        public DateTime DateAplic {  get; set; }

        public int Cantidad { get; set; }   
        public string TipoEquipo { get; set; } = string.Empty;
        public string Periodo { get; set; } = string.Empty;

        public string JsonEquipos { get; set; } = string.Empty;
        public MtoProgramado MtoProgramado { get; set; } = new();
    } 

    public class MtosPendientesViewModel
    {
        public string Json { get; set; } = string.Empty;
        public List<ConjuntoMtosViewModel> Conjuntos { get; set; } = new();
    }
}
