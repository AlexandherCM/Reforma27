using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using System.Security.Cryptography;
using System.Text;

using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace Condominios.Models.Services.Classes
{
    public class HerramientaRegistro
    {
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private string _host = "smtp.gmail.com";
        private int _puerto = 587;

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private string _nombre = "Nuevo usuario";
        private string _remitente = "pruebacorreo172003@gmail.com";
        private string _clave = "xvodomprdlhuwplg";

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public bool EnviarCorreo(CorreoDTO correo)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress(_nombre, _remitente));
                email.To.Add(MailboxAddress.Parse(correo.Para));
                email.Subject = correo.Asunto;
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = correo.Contenido
                };

                var smtp = new SmtpClient();
                smtp.Connect(_host, _puerto, SecureSocketOptions.StartTls);

                smtp.Authenticate(_remitente, _clave);
                smtp.Send(email);
                smtp.Disconnect(true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public CorreoDTO CrearPlantilla(IWebHostEnvironment hostingEnvironment, HttpContext HttpContext, Usuario usuario, string Destinatario, string plantilla, string ruta)
        {
            string folder = "Plantillas";

            // Utiliza el hostingEnvironment para resolver la ruta física
            string path = Path.Combine(hostingEnvironment.WebRootPath, folder, plantilla);

            string scheme = HttpContext.Request.Scheme;
            string host = HttpContext.Request.Host.ToString();
            string url = $"{scheme}://{host}/{ruta}";


            StreamReader reader = new StreamReader(path);
            string htmlBody = string.Format(reader.ReadToEnd(), url, usuario.Nombre);


            CorreoDTO correoDTO = new CorreoDTO()
            {
                Para = Destinatario,
                Asunto = "Confirmación de cuenta",
                Contenido = htmlBody
            };

            return correoDTO;
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
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

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public string GenerarToken()
        {
            string token = Guid.NewGuid().ToString("N");
            return token;
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        public Usuario CrearUsuario(UsuarioDTO user)
        {
            var usuarioNuevo = new Usuario()
            {
                PerfilID = 2,
                Nombre = user.Nombre,
                Correo = user.Correo,
                Clave = EncriptarPassword(user.Password),
                Restablecer = false,
                Token = GenerarToken(),
                Validado = false
            };
            return usuarioNuevo;
        }
    }
}
