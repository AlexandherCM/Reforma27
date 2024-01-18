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
        
        private readonly List<string> Unidades = new() 
        {
            "HP",
            "rpm",
            "L/H",
            "KCAL/H",
            "Kg",
            "lts",
            "m3",
        };

        private readonly Dictionary<string, int> Periodos = new()
        {
            { "Mensual", 1 },
            { "Bimestral", 2 },
            { "Trimestral", 3 },
            { "Semestral", 6 },
            { "Anual", 12 },
            { "C/5años", 60 }
        };

        private readonly List<string> Tipos = new List<string>
        {
            "Bomba horizontal",
            "Bomba Jockey",
            "Bomba sumergible",
            "Bomba vertical",
            "Caldera de agua",
            "Cisterna A",
            "Cisterna B",
            "Cisterna C",
            "Cisterna D",
            "CO2",
            "Compactador",
            "Elevador",
            "Elevauto",
            "Gabinete",
            "Góndola",
            "Montacargas",
            "Planta emergencia",
            "Pluvial",
            "PQS",
            "Sanitario",
            "Tinaco"
        };

        private readonly List<string> Marcas = new List<string>
        {
            "Baldor Reliancer",
            "Bipark 26",
            "Cárcamo",
            "Cisterna agua potable",
            "Espa",
            "Extintores",
            "Góndola",
            "Hayward",
            "Hidrantes",
            "Hydromatic Pumps",
            "John Deree",
            "Kone",
            "Leflam",
            "Marathon Electric",
            "Mass-Ter Cal",
            "Pedrollo",
            "Rotoplas",
            "Siemens",
            "Stamford",
            "Wilkinson",
            "WEG"
        };

        private readonly List<string> Ubicaciones = new List<string>
        {
            "Amenidades",
            "Amenidades 7",
            "Amenidades 16",
            "Amenidades 26",
            "Azotea",
            "Azotea 11",
            "Azotea 12",
            "Azotea 20",
            "Azotea 21",
            "Cárcamo perimetral Sótano 4",
            "Cárcamo Sanitario Sótano 5",
            "Cuarto de basura Sótano 3",
            "Cuarto de bombas Sótano 4",
            "Cuarto de maquinas azotea",
            "Cuarto maquinas alberca",
            "Estacionamiento comercial",
            "Jacuzzi",
            "Nivel 17",
            "Nivel 24",
            "Nivel 25",
            "Nivel 8",
            "Sótano 4",
            "Tanque de tormentas"
        };
        public async Task AgregarCatalogos()
        {
            await AgregarTiposMto();
            await AgregarEstatus();
            await AgregarMotores();
            await AgregarPerfiles();
            await AgregarAdmin();
            await AgregarTipos();
            await AgregarMarcas();
            await AgregarUbicaciones();
            await AgregarUnidades();
            await AgregarPeriodos();
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

        private async Task AgregarTipos()
        {
            foreach (var iteam in Tipos)
            {
                var flag =
                    await _context.TipoEquipo.FirstOrDefaultAsync(c =>
                        c.Nombre == iteam);

                if (flag == null)
                {
                    TipoEquipo tipo = new()
                    {
                        Nombre = iteam,
                        Estado = true
                    };
                    _context.Add(tipo);
                    await _context.SaveChangesAsync();
                }
            }
        }
        private async Task AgregarMarcas()
        {
            foreach (var iteam in Marcas)
            {
                var flag =
                    await _context.Marca.FirstOrDefaultAsync(c =>
                        c.Nombre == iteam);

                if (flag == null)
                {
                    Marca marca = new()
                    {
                        Nombre = iteam,
                        Estado = true
                    };
                    _context.Add(marca);
                    await _context.SaveChangesAsync();
                }
            }
        }
        private async Task AgregarUbicaciones()
        {
            foreach (var iteam in Ubicaciones)
            {
                var flag =
                    await _context.Ubicacion.FirstOrDefaultAsync(c =>
                        c.Nombre == iteam);

                if (flag == null)
                {
                    Ubicacion ubicacion = new()
                    {
                        Nombre = iteam,
                        Estado = true
                    };
                    _context.Add(ubicacion);
                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task AgregarUnidades()
        {
            foreach (var iteam in Unidades)
            {
                var flag =
                    await _context.UnidadMedida.FirstOrDefaultAsync(c =>
                        c.Nombre == iteam);

                if (flag == null)
                {
                    UnidadMedida unidad = new()
                    {
                        Nombre = iteam,
                        Estado = true
                    };
                    _context.Add(unidad);
                    await _context.SaveChangesAsync();
                }
            }
        }
         
        private async Task AgregarPeriodos()
        {
            foreach (var periodo in Periodos)
            {
                var flag = await _context.Periodo.FirstOrDefaultAsync(p => p.Nombre == periodo.Key || p.Meses == periodo.Value);

                if (flag == null)
                {
                    Periodo nuevoPeriodo = new()
                    {
                        Nombre = periodo.Key,
                        Meses = periodo.Value,
                        Estado = true
                    };

                    _context.Add(nuevoPeriodo);
                    await _context.SaveChangesAsync();
                }
            }
        }

    }
}
