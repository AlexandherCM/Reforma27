
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618
#pragma warning disable CS8602
#pragma warning disable CS8604

namespace Condominios.Models.ViewModels.CtrolEquipo
{
    public class CtrolMtosEquipoViewModels
    {
        [Required]
        public int MtoPendienteID { get; set; }

        [Required]
        public int EquipoID { get; set; } 
         
        public Equipo? Equipo { get; set; }

        [Required]
        public EditEquipoViewModel Plantilla { get; set; }

        public List<MtoProgramadoViewModel> MtosProgramados { get; set; } = new();
        public MantenimientoViewModel Mantenimiento { get; set; }          

        public SelectList? Proveedores { get; set; } 
        public SelectList? Estatus { get; set; }
        public SelectList? Ubicaciones { get; set; }
        public SelectList? TipoMtos { get; set; }

        public string? TotasGtosMto { get; set; }    
        public string? TotasGtosRep { get; set; }

        public AlertaEstado? AlertaEstado { get; set; }

        public Dictionary<int, string> estados { get; } =  new Dictionary<int, string>
        {
            { 1, "Pendiente" },
            { 2, "Aplicados" },
            { 3, "No aplicados" }
        };

        public int EdoAplicacion { get; set; } 

        public int GetMtoPendienteID() 
            => MtosProgramados.Where(c=>c.Pendiente).Select(c => c.MantenimientoID).FirstOrDefault();

        public int GetEquipoID()
            => Equipo.ID;
    } 

    public class MantenimientoViewModel
    {
        public int MtoProgramadoID { get; set; }  
        public int TipoMantenimientoID { get; set; }
        public int ProveedorID { get; set; }
        public decimal CostoMantenimiento { get; set; }
        public decimal? CostoReparacion { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaAplicacion { get; set; }
    }
    
    public class MtoProgramadoViewModel
    {
        public int MantenimientoID { get; set; }
        public string NumSerieEquipo { get; set; } 

        public long ProxAplicEpoch { get; set; }
        public string DiaDeAplicacion { get; set; }
        public string UltimaAplicacion { get; set; }
        public string ProximaAplicacion { get; set; }

        public string Estado { get; set; } 
        public bool Pendiente { get; set; } 
        public bool Aplicable { get; set; } 
         
        public string CostoMantenimiento { get; set; }
        public string CostoReparacion { get; set; }
        public string Proveedor { get; set; }
    }
} 
