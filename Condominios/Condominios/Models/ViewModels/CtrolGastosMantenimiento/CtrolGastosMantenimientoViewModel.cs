using Condominios.Models.DTOs;
using Condominios.Models.Services.Classes;
using Microsoft.AspNetCore.Mvc.Rendering;
#pragma warning disable CS8618

namespace Condominios.Models.ViewModels.CtrolGastosMantenimiento
{
    public class CtrolGastosMantenimientoViewModel
    {
        public List<ConjuntoViewModel> ConjuntoViewModel { get; set; } = new();

        //Filtros de busqueda - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public FiltrosGtosMtosDTO? Filtros { get; set; }

        //Calculos totales - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public string TotalCostoAdquisicion { get; set; } = string.Empty;
        public string TotalCostoMtos { get; set; } = string.Empty;
        public string TotalCostoReparacion { get; set; } = string.Empty;

        //Selects Lists - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public SelectList? Proveedores { get; set; }
        public SelectList? Marcas { get; set; }
        public SelectList? Ubicaciones { get; set; }
        public SelectList? TipoEquipos { get; set; }
        public SelectList? Motores { get; set; }

        //Alerta estado - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public AlertaEstado AlertaEstado { get; set; }
    }
}
