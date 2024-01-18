using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;

namespace Condominios.Models.ViewModels.Perfil
{
    public class PerfilViewModel
    {
        public int ID { get; set; }
        public UsuarioDTO? DatosUser { get; set; }
        public Usuario? Admin { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public AlertaEstado? AlertaEstado { get; set; }
    }
}
