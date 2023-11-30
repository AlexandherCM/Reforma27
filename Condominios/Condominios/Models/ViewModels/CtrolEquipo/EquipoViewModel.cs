﻿using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "El campo es obligatorio")]
        public decimal CostoAdquisicion { get; set; }

        //Otros Campos - - - - - - - - - - - - - - - - - - - - 
        [Required(ErrorMessage = "El campo es obligatorio")]
        public DateTime UltimaAplicacion { get; set; }
    }
}
