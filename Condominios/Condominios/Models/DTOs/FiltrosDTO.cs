using System.ComponentModel.DataAnnotations;

namespace Condominios.Models.DTOs
{
    public class FiltrosDTO
    {
        public int MarcaID { get; set; }
        public int TipoID { get; set; }
        public int UbicacionID { get; set; }
        public int MotorID { get; set; } 
    }
}
