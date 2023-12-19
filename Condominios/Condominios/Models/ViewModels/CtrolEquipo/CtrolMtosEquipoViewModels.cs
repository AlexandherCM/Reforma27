#pragma warning disable CS8618

using Condominios.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Condominios.Models.ViewModels.CtrolEquipo
{
    public class CtrolMtosEquiposViewModels
    { 
        public Equipo Equipo { get; set; } 
        public EditEquipoViewModel Plantilla { get; set; }
        public Mantenimiento Mantenimiento { get; set; } 
        public List<Mantenimiento> Mantenimientos { get; set; }

        public SelectList? Proveedores { get; set; } 
        public SelectList? Estatus { get; set; }
        public SelectList? Ubicaciones { get; set; }
        public SelectList? TipoMtos { get; set; }
    } 
}
