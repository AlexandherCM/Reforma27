using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace Condominios.Models.Entities
{
    [Table("Mantenimiento")]
    public class Mantenimiento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int MtoProgramadoID { get; set; }
        public int TipoMantenimientoID { get; set; }
        public int ProveedorID { get; set; }
        public decimal CostoMantenimiento { get; set; }
        public decimal CostoReparacion { get; set; }
        public string Observaciones { get; set; }
        public long FechaAplicacion { get; set; }

        //Relaciones - - - - - - - - - - - - - - - - - - - - - 
        [ForeignKey(nameof(MtoProgramadoID))]
        public virtual MtoProgramado MtoProgramado { get; set; }

        [ForeignKey(nameof(TipoMantenimientoID))]
        public virtual TipoMantenimiento TipoMantenimiento { get; set; }

        [ForeignKey(nameof(ProveedorID))]
        public virtual Proveedor Proveedor { get; set; }
    }
}
