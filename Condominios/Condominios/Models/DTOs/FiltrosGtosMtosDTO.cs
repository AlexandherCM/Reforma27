namespace Condominios.Models.DTOs
{
    public class FiltrosGtosMtosDTO : FiltrosDTO
    {
        public int ProveedorID { get; set; }
        public long Fecha1 { get; set; }
        public long Fecha2 { get; set; }
    }
}
