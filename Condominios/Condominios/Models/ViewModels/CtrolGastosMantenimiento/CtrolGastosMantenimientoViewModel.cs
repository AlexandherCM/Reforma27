using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Condominios.Models.ViewModels.CtrolGastosMantenimiento
{
    public class CtrolGastosMantenimientoViewModel
    {
        public List<Equipo> Equipos { get; set; } = new();
        public List<Mantenimiento> Mantenimientos { get; set; } = new();
        public List<ConteoViewModel> Conteo { get; set; } = new();
        public SelectList? Proveedores { get; set; }
        public SelectList? Marcas { get; set; }
        public SelectList? Ubicaciones { get; set; }
        public SelectList? TipoEquipos { get; set; }
        public SelectList? Motores { get; set; }

        [DisplayFormat(DataFormatString = "Fecha 1", ApplyFormatInEditMode = true)]
        public DateTime Fecha1 { get; set; }

        [DisplayFormat(DataFormatString = "Fecha 2", ApplyFormatInEditMode = true)]
        public DateTime Fecha2 { get; set; }

        public FiltrosGtosMtosDTO? Filtros { get; set; }

        public decimal CostoAdTotal { get; set; }
        public decimal CostoMTotal { get; set; }
        public decimal CostoRTotal { get; set; }
    }
}
