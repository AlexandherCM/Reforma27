using System.ComponentModel.DataAnnotations;

namespace Condominios.Models.ViewModels.CtrolEquipo
{
    public class EquipoViewModel : EditEquipoViewModel
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int VarianteID { get; set; }
        //Otros Campos - - - - - - - - - - - - - - - - - - - - 
        [Required(ErrorMessage = "El campo es obligatorio")]
        public DateTime UltimaAplicacion { get; set; }
    }
}
