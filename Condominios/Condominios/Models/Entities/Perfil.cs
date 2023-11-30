using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace Condominios.Models.Entities
{
    [Table("Perfil")]
    public class Perfil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Nombre { get; set; }
    }
}
