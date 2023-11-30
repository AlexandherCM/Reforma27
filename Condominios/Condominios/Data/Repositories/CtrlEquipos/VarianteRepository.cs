using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
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
                                      .Include(c => c.Funcion)
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
                        $" - {tipo.NombreMarca} - {tipo.NombreMotor} - {tipo.Capacidad} - {tipo.Funcion}";
                }
            });

            return tipos;
        }

        private List<VarianteDTO> Clone(List<Variante> datos)
        {
            List<VarianteDTO> tipos = (from tipo in datos
                                         select new VarianteDTO
                                         {
                                             ID = tipo.ID,
                                             Nombre = tipo.TipoEquipo.Nombre,
                                             NombreMarca = tipo.Marca.Nombre,
                                             NombreMotor = tipo.Motor.Nombre,
                                             Capacidad = tipo.Capacidad,
                                             Funcion = tipo.Funcion.Nombre
                                         }).ToList();

            return tipos;
        }
    }
}
