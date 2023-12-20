#pragma warning disable CS8618

using Condominios.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Condominios.Models.ViewModels.CtrolEquipo
{
    public class CtrolMtosEquipoViewModels
    { 
        public int EquipoID { get; set; }  
        public Equipo Equipo { get; set; } 
        public EditEquipoViewModel Plantilla { get; set; }
        public List<MtoProgramadoViewModel> MtosProgramados { get; set; } = new();
        public MantenimientoViewModel Mantenimiento { get; set; }          

        public SelectList? Proveedores { get; set; } 
        public SelectList? Estatus { get; set; }
        public SelectList? Ubicaciones { get; set; }
        public SelectList? TipoMtos { get; set; }

        public string TotasGtosMto { get; set; }    
        public string TotasGtosRep { get; set; }    
    } 

    public class MantenimientoViewModel
    {
        public int? ID { get; set; }
        public int TipoMantenimientoID { get; set; }
        public int ProveedorID { get; set; }
        public decimal CostoMantenimiento { get; set; }
        public decimal CostoReparacion { get; set; }
        public string Observaciones { get; set; }
        public long FechaAplicacion { get; set; }
    }
    
    public class MtoProgramadoViewModel
    {
        public int MantenimientoID { get; set; }
        public string NumSerieEquipo { get; set; } 

        public long ProxAplicEpoch { get; set; }
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
