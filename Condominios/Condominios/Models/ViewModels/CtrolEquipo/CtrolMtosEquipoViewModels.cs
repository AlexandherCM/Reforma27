
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618
#pragma warning disable CS8602
#pragma warning disable CS8604

namespace Condominios.Models.ViewModels.CtrolEquipo
{
    public class CtrolMtosEquipoViewModels
    {
        // ID's requeridos
        [Required]
        public int MantenimientoID { get; set; }

        [Required]
        public int EquipoID { get; set; } 
         
        public Equipo? Equipo { get; set; }

        [Required]
        public EditEquipoViewModel Plantilla { get; set; }

        // Lista de Mto Programado con su Mto confirmado- - - - - - - - - - - - - - - - - - -  
        public List<MtoProgramadoViewModel> MtosProgramados { get; set; } = new();
        public string JsonMtosProgramados { get; set; }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        public MantenimientoViewModel Mantenimiento { get; set; }


        // Selects - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public SelectList? Proveedores { get; set; } 
        public SelectList? Estatus { get; set; }
        public SelectList? Ubicaciones { get; set; }
        public SelectList? TipoMtos { get; set; }
        // Selects - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        public string? TotalGtosMto { get; set; }    
        public string? TotalGtosRep { get; set; }

        public AlertaEstado? AlertaEstado { get; set; }

        public Dictionary<int, string> estados { get; } =  new Dictionary<int, string>
        {
            { 1, "Pendiente" },
            { 2, "Aplicado" },
            { 3, "No aplicado" }
        };

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public int EdoAplicacion { get; set; }
        public string controller { get; } = "Status";
        public FilterMtos filterMtos { get; set; }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        public int GetMtoPendienteID() 
            => MtosProgramados.Where(c=>c.Pendiente).Select(c => c.MantenimientoID).FirstOrDefault();

        public int GetEquipoID()
            => Equipo.ID;

        public string CreateListMtosJson()
            => JsonConvert.SerializeObject(MtosProgramados);
    } 

    //Para confirma un mantenimiento programado
    public class MantenimientoViewModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public int MtoProgramadoID { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public int TipoMantenimientoID { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public int ProveedorID { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public decimal CostoMantenimiento { get; set; }

        public decimal? CostoReparacion { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime FechaAplicacion { get; set; }

        public bool TimedOut { get; set; } = false;
    }
    
    //Para obtener la lista de todos los mantenimientos del equipo
    public class MtoProgramadoViewModel
    {
        public int MantenimientoID { get; set; }
        public string NumSerieEquipo { get; set; } 

        public long ProxAplicEpoch { get; set; }

        public string DiaDeAplicacion { get; set; }
        public long DiaDeAplicacionEpoch { get; set; }  

        public string UltimaAplicacion { get; set; }
        public string ProximaAplicacion { get; set; }

        public string EstadoAplicacion { get; set; } 
        public bool Pendiente { get; set; } 
        //public bool Aplicable { get; set; } // PDT su uso!!
         
        public string CostoMantenimiento { get; set; }
        public string CostoReparacion { get; set; }
        public string Proveedor { get; set; }
        public string Observaciones { get; set; }
        public int ProveedorID { get; set; }

        public int TipoMantenimientoID { get; set; }
        public string TipoMantenimiento { get; set; }
    }

    public class FilterMtos
    {
        public int ProveedorID { get; set; }
        public DateTime FechaUno { get; set; }
        public DateTime FechaDos { get; set; }
    } 
} 
