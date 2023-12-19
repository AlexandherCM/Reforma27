using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace Condominios.Models.Entities
{
    [Table("MtoProgramado")]
    public class MtoProgramado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int? MantenimientoID { get; set; }
        public int EquipoID { get; set; }
        public long UltimaAplicacion { get; set; }
        public long ProximaAplicacion { get; set; }
        public bool Aplicado { get; set; }
        public bool Aplicable { get; set; }
        public bool Estado { get; set; }  

        //Relaciones - - - - - - - - - - - - - - - - - - - - - 
        [ForeignKey(nameof(EquipoID))]
        public virtual Equipo Equipo { get; set; } 
        
        [ForeignKey(nameof(MantenimientoID))]
        public virtual Mantenimiento? Mantenimiento { get; set; } 
    }
}
