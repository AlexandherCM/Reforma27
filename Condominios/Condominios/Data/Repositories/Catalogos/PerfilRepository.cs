﻿using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Perfil;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8603
#pragma warning disable CS8602
#pragma warning disable CS8600

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

        public async Task<string> GetAdminEmail()
        {
            var admin = await _context.Usuario
                .Where(u => u.Perfil.Nombre == "Administrador")
                .Select(u => u.Correo)
                .FirstOrDefaultAsync();

            return admin;
        }

        public async Task<Usuario> GetUser(int id)
        {
            var user = await _context.Usuario.FindAsync(id);
            return user;
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
            _alertaEstado.Leyenda = "Datos Actualizados correctamente.";
            _alertaEstado.Estado = true;
            return _alertaEstado;
        }

        public void Acceso(int id)
        {
            var usuario = _context.Find<Usuario>(id);
            usuario.Validado = !usuario.Validado;
            return;
        }

        public async Task<bool> Delete(int id)
        {
            var usuario = await _context.FindAsync<Usuario>(id);

            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
                return true; 
            }

            return false; 
        }
    }
}
