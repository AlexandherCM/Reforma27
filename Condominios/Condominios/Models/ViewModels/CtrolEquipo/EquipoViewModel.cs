using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace Condominios.Models.ViewModels.CtrolEquipo
{
    public class EquipoViewModel
    {
        //EL ID SE USA COMO PLANTILLA PARA OTROS CONCEPTOS QUE TENGAN IDENTIFICADOR
        public int? ID { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public int VarianteID { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public int UbicacionID { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public int EstatusID { get; set; }
        public decimal? CostoAdquisicion { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Funcion { get; set; }  

        //Otros Campos - - - - - - - - - - - - - - - - - - - - 
        [Required(ErrorMessage = "El campo es obligatorio")]
        public DateTime UltimaAplicacion { get; set; }
    }
}
