using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace Condominios.Models.ViewModels.CtrolEquipo
{
    public class CtrlEquipoViewModel
    {
        //EL ID SE USA COMO PLANTILLA PARA OTROS CONCEPTOS QUE TENGAN IDENTIFICADOR
        public FiltrosDTO? FiltroID { get; set; }
        public List<Equipo>? Equipos { get; set; }
        public SelectList? TipoEquipos { get; set; }
        public SelectList? Variantes { get; set; }
        public SelectList? Estados { get; set; }
        public SelectList? Estatus { get; set; }
        public SelectList? Ubicaciones { get; set; }
        public SelectList? Marcas { get; set; }
        public SelectList? Motores { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public List<string> NumerosSerie { get; set; }

        [Required]
        public EquipoViewModel Plantilla { get; set; }
    }
}
