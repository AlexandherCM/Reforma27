using Condominios.Models.Entities;
using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace Condominios.Models.ViewModels
{
    public class VarianteViewModel : Variante
    {
        public List<Variante> variantes { get; set; }
    }
}
