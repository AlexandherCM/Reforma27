using Condominios.Models.Entities;
using Condominios.Models.ViewModels.CtrolEquipo;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618

namespace Condominios.Models.ViewModels.CtrolMantenimientos
{
    public class CrearMtosViewModel
    {
        public List<Equipo> equipos { get; set; } = new();
        public SelectList? Proveedores { get; set; }
        public SelectList? TipoMtos { get; set; }
        public MantenimientoViewModel Mantenimiento { get; set; }
    }
}
