#pragma warning disable CS8618

using Condominios.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Condominios.Models.ViewModels.CtrolEquipo
{
    public class CtrolMtosEquipoViewModels 
    {
        //public int MtoProgradoID { get; set; }  
        public Equipo Equipo { get; set; } 
        public EditEquipoViewModel Plantilla { get; set; }
        public Mantenimiento Mantenimiento { get; set; } 
        public List<MtoProgramadoViewModel> MtosProgramados { get; set; } 

        public SelectList? Proveedores { get; set; } 
        public SelectList? Estatus { get; set; }
        public SelectList? Ubicaciones { get; set; }
        public SelectList? TipoMtos { get; set; }
    }
    
    public class MtoProgramadoViewModel
    {
        public int ID { get; set; }
        public long ProxAplicEpoch { get; set; }
        public string UltimaAplicacion { get; set; }
        public string ProximaAplicacion { get; set; }
        public string Estado { get; set; } 
        public Mantenimiento Mantenimiento { get; set; }
    }
}
