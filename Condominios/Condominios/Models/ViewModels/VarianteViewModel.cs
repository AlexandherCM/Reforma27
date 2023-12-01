using Condominios.Models.Entities;
using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace Condominios.Models.ViewModels
{
    public class VarianteViewModel : Variante
    {
        public List<Variante> variantes { get; set; }

        [Required(ErrorMessage = "Escriba la función de este tipo de equipos")]
        public string LaFuncion { get; set; }
    }
}
