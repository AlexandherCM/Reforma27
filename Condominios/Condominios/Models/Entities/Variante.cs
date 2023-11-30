using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace Condominios.Models.Entities
{
    [Table("Variante")]
    public class Variante 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int MarcaID { get; set; }
        public int MotorID { get; set; }
        public int PeriodoID { get; set; }
        public int FuncionID { get; set; }
        public int TipoEquipoID { get; set; }
        public string Capacidad { get; set; }
        public bool Estado { get; set; }

        //Relaciones - - - - - - - - - - - - - - - - - - - - -

        [ForeignKey(nameof(MarcaID))]
        public virtual Marca? Marca { get; set; }

        [ForeignKey(nameof(MotorID))]
        public virtual Motor? Motor { get; set; }

        [ForeignKey(nameof(PeriodoID))]
        public virtual Periodo? Periodo { get; set; }
        
        [ForeignKey(nameof(FuncionID))]
        public virtual Funcion? Funcion { get; set; }
        
        [ForeignKey(nameof(TipoEquipoID))] 
        public virtual TipoEquipo? TipoEquipo { get; set; } 
    }
}
