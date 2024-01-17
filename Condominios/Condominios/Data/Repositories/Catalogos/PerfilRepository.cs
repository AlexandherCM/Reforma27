using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Perfil;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.Catalogos
{
    public class PerfilRepository : IPerfilRepository<Usuario>
    {
        private readonly Context _context;
        private readonly AlertaEstado _alertaEstado = new();
        private readonly HerramientaRegistro _herramientaRegistro = new();

        public PerfilRepository(Context context)
        { 
            _context = context;
        } 

        public async Task<Usuario> GetAdmin()
        {
            var Admin = await _context.Usuario.Where(u => u.Perfil.Nombre == "Administrador").FirstOrDefaultAsync();
            return Admin;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            var users = await _context.Usuario.Where(u => u.Perfil.Nombre == "General").ToListAsync();
            return users;
        }

        public async Task<AlertaEstado> Update(PerfilViewModel viewModel)
        {
            Usuario user;

            if (viewModel.ID == 0)
            {
                user = await GetAdmin();
            }
            else
            {
                user = await _context.Usuario.FindAsync(viewModel.ID);
            }

            if (!string.IsNullOrWhiteSpace(viewModel.DatosUser.Nombre))
            {
                user.Nombre = viewModel.DatosUser.Nombre;
            }

            if (!string.IsNullOrWhiteSpace(viewModel.DatosUser.Correo))
            {
                user.Correo = viewModel.DatosUser.Correo;
            }

            if (!string.IsNullOrWhiteSpace(viewModel.DatosUser.Password))
            {
                user.Clave =_herramientaRegistro.EncriptarPassword(viewModel.DatosUser.Password);
            }
            _alertaEstado.Leyenda = "Datos actualizados";
            _alertaEstado.Estado = true;
            return _alertaEstado;
        }

        public void Acceso(int id)
        {
            var usuario = _context.Find<Usuario>(id);
            usuario.Validado = !usuario.Validado;
            return;
        }

       
    }
}
