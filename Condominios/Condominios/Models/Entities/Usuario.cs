using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace Condominios.Models.Entities
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int PerfilID { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        //public string token { get; set; }
        //public bool Restablecer { get; set; }
        //public bool Validado { get; set; }

        [ForeignKey(nameof(PerfilID))]
        public virtual Perfil Perfil { get; set; }
    }
}
