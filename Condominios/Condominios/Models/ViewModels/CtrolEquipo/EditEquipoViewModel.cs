using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace Condominios.Models.ViewModels.CtrolEquipo
{
    public class EditEquipoViewModel
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        public int UbicacionID { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public int EstatusID { get; set; }

        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Funcion { get; set; }

        public string? NumSerie { get; set; }
        public decimal? CostoAdquisicion { get; set; }
    }
}
