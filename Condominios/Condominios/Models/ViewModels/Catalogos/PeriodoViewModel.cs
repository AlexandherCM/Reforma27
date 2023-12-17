using System.ComponentModel.DataAnnotations;

namespace Condominios.Models.ViewModels.Catalogos
{
    public class PeriodoViewModel : CatalogoGralViewModel
    {
        [Required(ErrorMessage = "El campo cantidad es obligatorio")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Selecciona el plazo de tiempo")]
        public bool Mes { get; set; }

        //public int Meses()
        //    => Mes ? Cantidad : Cantidad * 12;
        
        public int Meses()
            => Cantidad;
    }
}
