using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Condominios.Models.ViewModels.CtrolVarianteEquipo
{
    public class VarianteViewModel
    {
        public int UpdateID { get; set; }
        public VarianteEquipo VarianteEquipo { get; set; }
        public List<Variante> Variantes { get; set; }
        public SelectList? Marcas { get; set; }
        public SelectList? Motores { get; set; }
        public SelectList? Periodos { get; set; }
        public SelectList? TipoEquipo { get; set; }
        public SelectList? Capacidad { get; set; }
    }
}
