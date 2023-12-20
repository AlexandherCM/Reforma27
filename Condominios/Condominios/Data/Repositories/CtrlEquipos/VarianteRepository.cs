using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.CtrlEquipos
{
    public class VarianteRepository : IVarianteRepository<VarianteDTO>
    {
        private readonly Context _context;
        private AlertaEstado _alertaEstado = new();

        public VarianteRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<VarianteDTO>> GetNormalList()
        {
            List<Variante> variantes = await _context.Variante.Include(c => c.Marca)
                                      .Include(c => c.Motor)
                                      .Include(c => c.TipoEquipo)
                                      .Include(c => c.Periodo)
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
                        $" / Marca: {tipo.NombreMarca} / Motor: {tipo.NombreMotor} / Capacidad: {tipo.Capacidad} / Mantenimiento: {tipo.Periodo}";
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
                                           Nombre = $"Tipo: {tipo.TipoEquipo.Nombre}",
                                           NombreMarca = tipo.Marca.Nombre,
                                           NombreMotor = tipo.Motor.Nombre,
                                           Capacidad = tipo.Capacidad,
                                           Periodo = tipo.Periodo.Nombre,
                                       }).ToList();

            return tipos;
        }

        public async Task<AlertaEstado> Add(VarianteViewModel model, string medida)
        {

            if (_context.Variante.Any(e => e.MarcaID == model.VarianteEquipo.MarcaID &&
                                      e.MotorID == model.VarianteEquipo.MotorID &&
                                      e.PeriodoID == model.VarianteEquipo.PeriodoID &&
                                      e.TipoEquipoID == model.VarianteEquipo.TipoEquipoID &&
                                      e.Capacidad == model.VarianteEquipo.Capacidad(medida)))
            {
                _alertaEstado.Leyenda = "Ya existe un registro de esta clasificacion, ingrese uno diferente";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

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

            _alertaEstado.Leyenda = "Datos Registrados correctamente";
            _alertaEstado.Estado = true;
            return _alertaEstado;
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

        public async Task<Variante?> UpdateID(int id)
        {
            var variante = await _context.Variante.FirstOrDefaultAsync(c => c.ID == id);
            variante.Estado = !variante.Estado;
            return variante;
        }

        public async Task<AlertaEstado> Update(VarianteViewModel model, string medida)
        {
            var variante = _context.Find<Variante>(model.VarianteEquipo.ID);

            if (_context.Variante.Any(e => e.MarcaID == model.VarianteEquipo.MarcaID &&
                                      e.MotorID == model.VarianteEquipo.MotorID &&
                                      e.PeriodoID == model.VarianteEquipo.PeriodoID &&
                                      e.TipoEquipoID == model.VarianteEquipo.TipoEquipoID &&
                                      e.Capacidad == model.VarianteEquipo.Capacidad(medida)))
            {
                _alertaEstado.Leyenda = "Ya existe un registro de esta clasificacion, ingrese uno diferente";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            variante.MarcaID = model.VarianteEquipo.MarcaID;
            variante.MotorID = model.VarianteEquipo.MotorID;
            variante.PeriodoID = model.VarianteEquipo.PeriodoID;
            variante.TipoEquipoID = model.VarianteEquipo.TipoEquipoID;
            variante.Capacidad = model.VarianteEquipo.Capacidad(medida);

            _alertaEstado.Leyenda = "Datos actualizados correctamente";
            _alertaEstado.Estado = true;
            return _alertaEstado;

        }
    }
}
