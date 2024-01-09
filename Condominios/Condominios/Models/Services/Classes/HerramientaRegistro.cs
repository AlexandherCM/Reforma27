using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Condominios.Models.Services.Classes
{
    public class HerramientaRegistro
    {
        public string EncriptarPassword(string password)
        {
            string hash = string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] valor = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                foreach (byte b in valor)
                {
                    hash += $"{b:x2}";
                }
            }
            return hash;
        }




        public Usuario CrearUsuario(UsuarioDTO user)
        {
            var usuarioNuevo = new Usuario()
            {
                Nombre = user.Nombre,
                Correo = user.Correo,
                Clave = EncriptarPassword(user.Password),
                Restablecer = false,
                Validado = false,
                Token = "g"
            };

            return usuarioNuevo;
        }
    }
}
