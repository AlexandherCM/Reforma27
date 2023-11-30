using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace Condominios.Models.ViewModels.Catalogos
{
    public class CatalogoGralViewModel
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }
        public bool Estado { get; set; }
    }
}
