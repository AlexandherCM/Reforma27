using Condominios.Models;
using Condominios.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.DTOs;
using Microsoft.Win32;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace Condominios.Models.Services
{
    public interface IAutenticarService
    {
        public Task<string> IniciarSesion(SesionViewModel sesion, HttpContext HttpContext);
        public Task CerrarSesion(HttpContext HttpContext);
        public Task<string> UsuarioExistente(UsuarioDTO user, HttpContext httpContext);
    }
    public class AutenticarService : IAutenticarService
    {
        private readonly Context _context;
        private HerramientaRegistro _herramientaRegistro = new HerramientaRegistro();
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AutenticarService(Context context, IWebHostEnvironment webHostEnvironment )
        {
            _context = context;
            _hostingEnvironment = webHostEnvironment;
        }
        public async Task<string> IniciarSesion(SesionViewModel sesion, HttpContext HttpContext)
        {
            var Mensaje = string.Empty;
            var Clave = _herramientaRegistro.EncriptarPassword(sesion.Clave);
            List<Usuario> Usuarios = await _context.Usuario
            .Where(c => c.Correo == sesion.Correo && c.Clave == Clave)
            .Include(r => r.Perfil)
            .ToListAsync();

            var usuario = Usuarios.FirstOrDefault();
            if (usuario != null)
            {
                if (usuario.Validado != false)
                {
                    if (usuario.Restablecer != true)
                    {
                        await CrearCoockie(usuario, HttpContext);
                        return Mensaje = "ok";
                    }
                    return Mensaje = "Ha solicitado la recuperacion de la cuenta, favor de revisar su correo";
                }
                return Mensaje = "La cuenta esta en espera de validacion";
            }
            return Mensaje = "Las credenciales son incorrectas";
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

        public async Task<string> UsuarioExistente(UsuarioDTO user, HttpContext httpContext)
        {
            var Mensaje = string.Empty;
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Correo == user.Correo);

            if(usuario == null)
            {
                int GralID = _context.Perfil.Where(c => c.Nombre == "General").Select(c => c.ID).First();

                var NuevoUsuario = _herramientaRegistro.CrearUsuario(user, GralID);
                var Destinatario = "carlosivan12.ci2@gmail.com";
                var Plantilla = "Confirmar.html";
                var Ruta = $"Acceso/Confirmar?token={NuevoUsuario.Token}";

                var Correo = _herramientaRegistro.CrearPlantilla(_hostingEnvironment, httpContext, NuevoUsuario, Destinatario, Plantilla, Ruta);
                bool Enviado = _herramientaRegistro.EnviarCorreo(Correo);

                if(Enviado)
                {
                    _context.Usuario.Add(NuevoUsuario);
                    await _context.SaveChangesAsync();
                    return Mensaje = "Su cuenta esta en espera de aceptacion, favor de esperar el correo de validacion";
                }
                return Mensaje = "No se pudo completar el registro, intentelo de nuevo";
            }
            return Mensaje = "El correo ya se encuentra registrado";
        }

        public async Task<string> ConfirmarUsuario(string token, HttpContext httpContext)
        {
            var Mensaje = string.Empty;
            var usuario = _context.Usuario.FirstOrDefault(u => u.Token == token);
            if (usuario != null && usuario.Validado == false)
            {
                var Destinatario = usuario.Correo;
                var Plantilla = "Respuesta.html";
                var Ruta = "Acceso/Login";
                var Correo = _herramientaRegistro.CrearPlantilla(_hostingEnvironment, httpContext, usuario, Destinatario, Plantilla, Ruta);
                bool Enviado = _herramientaRegistro.EnviarCorreo(Correo);

                usuario.Validado = true;

                if (Enviado)
                {
                    await _context.SaveChangesAsync();
                    return Mensaje = "El usuario ha sido validado";
                }
                return Mensaje = "No se pudo validar el usuario, intentelo de nuevo";
            }
            return Mensaje = "El correo del usuario ya esta dado de alta";
        }

        public async Task<string> RestablecerCuenta(UsuarioDTO user, HttpContext httpContext)
        {
            var Mensaje = string.Empty;
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Correo == user.Correo);
            if (usuario != null && usuario.Validado == true)
            {
                if (usuario.Restablecer == false)
                {
                    var Destinatario = usuario.Correo;
                    var Plantilla = "Recuperar.html";
                    var Ruta = $"Acceso/CambiarPassword?token={usuario.Token}";
                    var Correo = _herramientaRegistro.CrearPlantilla(_hostingEnvironment, httpContext, usuario, Destinatario, Plantilla, Ruta);
                    bool Enviado = _herramientaRegistro.EnviarCorreo(Correo);

                    usuario.Restablecer = true;

                    if (Enviado)
                    {
                        await _context.SaveChangesAsync();

                        return Mensaje = "Restablecer cuenta, favor de revisar la bandeja de entrada de su correo para seguir con el proceso";
                    }
                    return Mensaje = "No se pudo restablecer la cuenta, intentelo de nuevo";
                }
                return Mensaje = "Ya ha solicitado el restablecimiento de su cuenta, favor de revisar la bandeja de entrada de su correo";
            }
            return Mensaje = "No hay ningun usuario asociado a esa cuenta";
        }

        public async Task<string> CambiarPass(string token, UsuarioDTO user)
        {
            var Mensaje = string.Empty;
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Token == token);
            if (usuario != null)
            {
                if (usuario.Restablecer == false)
                {
                    return Mensaje = "La cuenta ya sido restablecida";
                }

                usuario.Clave = _herramientaRegistro.EncriptarPassword(user.Password); 
                usuario.Restablecer = false;

                await _context.SaveChangesAsync();
                return Mensaje = "La cuenta ha sido restablecida correctamente";
            }
            return Mensaje = "Usuario no encontrado";
        }
    }
}
