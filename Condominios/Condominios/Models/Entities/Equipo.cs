using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
#pragma warning disable CS8618

namespace Condominios.Models.Entities
{
    [Table("Equipo")]
    [Index(nameof(NumSerie), IsUnique = true)]
    public class Equipo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int VarianteID { get; set; }
        public int UbicacionID { get; set; }
        public int EstatusID { get; set; }
        public string NumSerie { get; set; }
        public string Funcion { get; set; } 
        public decimal CostoAdquisicion { get; set; }
        public bool Estado { get; set; }

        //Relaciones - - - - - - - - - - - - - - - - - - - - - 
        [ForeignKey(nameof(VarianteID))]
        public virtual Variante Variante { get; set; }

        [ForeignKey(nameof(UbicacionID))]
        public virtual Ubicacion Ubicacion { get; set; }

        [ForeignKey(nameof(EstatusID))]
        public virtual Estatus Estatus { get; set; }

        public virtual ICollection<MtoProgramado> Programados{ get;set; }
    }
}
