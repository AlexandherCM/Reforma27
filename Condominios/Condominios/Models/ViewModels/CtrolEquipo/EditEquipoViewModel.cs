using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace Condominios.Models.ViewModels.CtrolEquipo
{
    public class EditEquipoViewModel
    {
        public int? ID { get; set; }

        [Required(ErrorMessage = "El campo es Ubicación es obligatorio")]
        public int UbicacionID { get; set; }

        [Required(ErrorMessage = "El campo estatus es obligatorio")]
        public int EstatusID { get; set; }

        [Required(ErrorMessage = "El campo es funcióne es obligatorio")]
        public string Funcion { get; set; }

        [Required(ErrorMessage = "El campo es número de serie es obligatorio")]
        public string NumSerie { get; set; }

        public decimal? CostoAdquisicion { get; set; }
    }
}
