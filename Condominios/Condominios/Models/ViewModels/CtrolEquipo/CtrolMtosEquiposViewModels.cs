#pragma warning disable CS8618

using Condominios.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Condominios.Models.ViewModels.CtrolEquipo
{
    public class CtrolMtosEquiposViewModels
    {
        public Equipo Equipo { get; set; }
        public EquipoViewModel Plantilla { get; set; }
        public List<Mantenimiento> Mantenimientos { get; set; }

        public SelectList? TipoEquipos { get; set; }
        public SelectList? Variantes { get; set; }
        public SelectList? Estados { get; set; }
        public SelectList? Estatus { get; set; }
        public SelectList? Ubicaciones { get; set; }
        public SelectList? Marcas { get; set; }
        public SelectList? Motores { get; set; }
    }
}
