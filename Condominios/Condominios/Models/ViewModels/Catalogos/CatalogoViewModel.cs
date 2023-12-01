using Condominios.Models.Entities;
using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace Condominios.Models.ViewModels.Catalogos
{
    public class CatalogoViewModel
    {
        public CatalogoGralViewModel? CatalogoGralViewModel { get; set; }
        public PeriodoViewModel? PeriodoViewModel { get; set; }
        public List<Marca>? Marcas { get; set; }
        public List<Motor>? Motores { get; set; }
        public List<Ubicacion>? Ubicaciones { get; set; }
        public List<Periodo>? Periodos { get; set; }
        public List<Estatus>? Estatus { get; set; }
        public List<TipoMantenimiento>? TipoMantenimientos { get; set; }
        public List<TipoEquipo>? TipoEquipos { get; set; }
        public List<UnidadMedida>? unidadMedidas { get; set; }

        [Required]
        public string Entidad { get; set; }
        public int ID { get; set; }
    }
}
