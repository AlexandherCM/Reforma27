using Condominios.Models.Entities;

namespace Condominios.Models.ViewModels.CtrolProveedores
{
    public class ProveedoresViewModel
    {
        public int UpdateID { get; set; }
        public int ID { get; set; }

        public string Empresa { get; set; } = string.Empty;
        public string Servicio { get; set; } = string.Empty;
        public string Contacto { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Direccion {  get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public List<Proveedor> Proveedor { get; set; } = new();
    }
}
