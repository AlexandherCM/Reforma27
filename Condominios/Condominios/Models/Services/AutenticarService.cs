using Condominios.Models;
using Condominios.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models.Entities;

namespace Condominios.Models.Services
{
    public interface IAutenticarService
    {
        public Task<bool> IniciarSesion(SesionViewModel sesion, HttpContext HttpContext);
        public Task CerrarSesion(HttpContext HttpContext);
    }
    public class AutenticarService : IAutenticarService
    {
        private readonly Context _context;
        public AutenticarService(Context context)
        {
            _context = context;
        }
        public async Task<bool> IniciarSesion(SesionViewModel sesion, HttpContext HttpContext)
        {
            List<Usuario> Usuarios = await _context.Usuario
            .Where(c => c.Correo == sesion.Correo && c.Clave == sesion.Clave)
            .Include(r => r.Perfil)
            .ToListAsync();

            var usuario = Usuarios.FirstOrDefault();
            if (usuario != null)
            {
                await CrearCoockie(usuario, HttpContext);
                return true;
            }
            return false;
        }

        public async Task CerrarSesion(HttpContext HttpContext)
            => await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        private static async Task CrearCoockie(Usuario usuario, HttpContext httpContext)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Perfil.Nombre)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
