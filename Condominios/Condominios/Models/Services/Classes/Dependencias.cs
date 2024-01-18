using Condominios.Models;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Condominios.Models.Services.Classes
{
    public class Dependencias
    {
        private readonly Context _context;
        private readonly HerramientaRegistro _initAdmin = new(); 
        public Dependencias(Context context)
        {
            _context = context;
        }

        private readonly UsuarioDTO _suarioDTO = new() 
        { 
            Nombre = "Raúl Lara",
            Correo = "gerente@reforma27.com.mx",
            Password = "180706038176",
        };

        private readonly List<string> Perfiles = new()
        {
            "Administrador",
            "General"
        };
        private readonly List<string> Motores = new()
        {
            "Combustión interna",
            "Monofásica",
            "Trifásica",
            "N/A"
        };
        private readonly List<string> TiposMto = new()
        {
            "Preventivo",
            "Correctivo"
        };
        private readonly List<string> Estatus = new()
        {
            "Bueno",
            "Regular",
            "Malo",
            "Crítico",
            "Fuera de servicio"
        };
        public async Task AgregarCatalogos()
        {
            await AgregarTiposMto();
            await AgregarEstatus();
            await AgregarMotores();
            await AgregarPerfiles();
            await AgregarAdmin();
        }
        private async Task AgregarAdmin()
        {
            int AdminID = _context.Perfil.Where(c=>c.Nombre == "Administrador").Select(c=>c.ID).First();

            var flag =
                    await _context.Usuario.FirstOrDefaultAsync(c =>
                        c.PerfilID == AdminID);

            if (flag == null)
            {
                _context.Add(_initAdmin.CrearAdmin(_suarioDTO, AdminID));
                await _context.SaveChangesAsync();
            }
        }
        private async Task AgregarMotores()
        {
            foreach (var iteam in Motores)
            {
                var flag =
                    await _context.Motor.FirstOrDefaultAsync(c =>
                        c.Nombre == iteam);

                if (flag == null)
                {
                    Motor motor = new()
                    {
                        Nombre = iteam,
                        Estado = true
                    };
                    _context.Add(motor);
                    await _context.SaveChangesAsync();
                }
            }
        }
        private async Task AgregarPerfiles()
        {
            foreach (var iteam in Perfiles)
            {
                var flag =
                    await _context.Perfil.FirstOrDefaultAsync(c =>
                        c.Nombre == iteam);

                if (flag == null)
                {
                    Perfil perfil = new()
                    {
                        Nombre = iteam
                    };
                    _context.Add(perfil);
                    await _context.SaveChangesAsync();
                }
            }
        }
        private async Task AgregarTiposMto()
        {
            foreach (var iteam in TiposMto)
            {
                var flag =
                    await _context.TipoMantenimiento.FirstOrDefaultAsync(c =>
                        c.Nombre == iteam);

                if (flag == null)
                {
                    TipoMantenimiento tipoMantenimiento = new()
                    {
                        Nombre = iteam,
                        Estado = true
                    };
                    _context.Add(tipoMantenimiento);
                    await _context.SaveChangesAsync();
                }
            }
        }
        private async Task AgregarEstatus()
        {
            foreach (var iteam in Estatus)
            {
                var flag =
                    await _context.Estatus.FirstOrDefaultAsync(c =>
                        c.Nombre == iteam);

                if (flag == null)
                {
                    Estatus estatus = new()
                    {
                        Nombre = iteam,
                        Estado = true
                    };
                    _context.Add(estatus);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
