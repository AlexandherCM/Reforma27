using Condominios.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace Condominios.Models.ViewModels
{
    public class SesionViewModel
    {
        [Required(ErrorMessage = "El campo Correo es obligatorio")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo contaseña es obligatorio")]
        public string Clave { get; set; }
    }
}
