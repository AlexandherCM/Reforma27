using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.CtrlEquipos
{
    public class VarianteRepository : IVarianteRepository<VarianteDTO>
    {
        private readonly Context _context;
        public VarianteRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<VarianteDTO>> GetNormalList()
        {
            List<Variante> variantes = await _context.Variante.Include(c => c.Marca)
                                      .Include(c => c.Motor)
                                      .Include(c => c.TipoEquipo)
                                      .ToListAsync();
            return Clone(variantes);
        }
        public async Task<List<VarianteDTO>> GetSpecialList()
        {
            List<VarianteDTO> tipos = await GetNormalList();

            tipos.ForEach(tipo =>
            {
                if (tipo != null)
                {
                    tipo.Nombre += 
                        $" - {tipo.NombreMarca} - {tipo.NombreMotor} - {tipo.Capacidad}";
                }
            });

            return tipos;
        }

        private static List<VarianteDTO> Clone(List<Variante> datos)
        {
            List<VarianteDTO> tipos = (from tipo in datos
                                         select new VarianteDTO
                                         {
                                             ID = tipo.ID,
                                             Nombre = tipo.TipoEquipo.Nombre,
                                             NombreMarca = tipo.Marca.Nombre,
                                             NombreMotor = tipo.Motor.Nombre,
                                             Capacidad = tipo.Capacidad
                                         }).ToList();

            return tipos;
        }

        public void Add(VarianteViewModel model, string medida)
        {
            Variante variante = new Variante()
            {
                MarcaID = model.VarianteEquipo.MarcaID,
                MotorID = model.VarianteEquipo.MotorID,
                PeriodoID = model.VarianteEquipo.PeriodoID,
                TipoEquipoID = model.VarianteEquipo.TipoEquipoID,
                Capacidad = model.VarianteEquipo.Capacidad(medida),
                Estado = true
            };

            _context.Add(variante);
        }

        public async Task<List<Variante>> GetList()
        {
            var variantes = await _context.Variante.ToListAsync();
            return variantes;
        }

        public async Task<Variante?> GetById(int id)
        {
            var variante = await _context.Variante.FirstOrDefaultAsync(c => c.ID == id);
            return variante;
        }
    }
}
