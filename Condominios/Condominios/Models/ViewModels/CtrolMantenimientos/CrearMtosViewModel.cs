using Condominios.Models.Entities;
using Condominios.Models.ViewModels.CtrolEquipo;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618

namespace Condominios.Models.ViewModels.CtrolMantenimientos
{
    public class CrearMtosViewModel
    {
        public List<EquipoMtoViewModel> equipos { get; set; } = new();
        public SelectList? Proveedores { get; set; }
        public SelectList? TipoMtos { get; set; }

        public MantenimientoViewModel Mantenimiento { get; set; }
        public string JsonEquipos { get; set; } 
    }

    public class EquipoMtoViewModel 
    {
        public string NumSerie { get; set;}
        public string Marca { get; set;}  
        public string TipoEquipo { get; set;}
        public string UltimaAplicion { get; set;}
        public string ProximaAplicion { get; set; }

        public MtoProgramado Programado { get; set; }
        public bool AplicarReparacion { get; set; }  
    }
}
