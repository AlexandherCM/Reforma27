using Condominios.Models.Entities;

namespace Condominios.Models.ViewModels.CtrolProveedores
{
    public class ProveedoresViewModel
    {
        public int UpdateID { get; set; }
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public string Direccion {  get; set; }
        public string Correo { get; set; }
        public List<Proveedor> Proveedor { get; set; }
    }
}
