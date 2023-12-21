using System.ComponentModel.DataAnnotations;

namespace Condominios.Models.ViewModels.CtrolEquipo
{
    public class EquipoViewModel
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int VarianteID { get; set; }
        //Otros Campos - - - - - - - - - - - - - - - - - - - - 
        [Required(ErrorMessage = "El campo es obligatorio")]
        public DateTime UltimaAplicacion { get; set; }

        [Required(ErrorMessage = "El campo es Ubicación es obligatorio")]
        public int UbicacionID { get; set; }

        [Required(ErrorMessage = "El campo estatus es obligatorio")]
        public int EstatusID { get; set; }

        [Required(ErrorMessage = "El campo es funcióne es obligatorio")]
        public string Funcion { get; set; } = string.Empty;
        public decimal? CostoAdquisicion { get; set; }
    }
}
