using System.ComponentModel.DataAnnotations;

namespace Condominios.Models.DTOs
{
    public class FiltrosDTO
    {
        public int MarcaID { get; set; }
        public int TipoID { get; set; }
        public int UbicacionID { get; set; }
        public int MotorID { get; set; } 
        public int ProveedorID { get; set; }

        public long Fecha1 { get; set; }
        public long Fecha2 { get; set; }
    }
}
