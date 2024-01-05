namespace Condominios.Models.ViewModels.CtrolGastosMantenimiento
{
    public class ConjuntoViewModel
    {
        public string Variante { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public string CostoAdquisicion {  get; set; } = string.Empty;
        public string CostoMto { get; set; } = string.Empty;
        public string CostoReparacion { get; set; } = string.Empty;
    }
}
