using Condominios.Data.Interfaces;
using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Microsoft.EntityFrameworkCore;
using System.Linq;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.Equipos
{
    public class EquipoRepository : IEquipoRepository<Equipo>
    {
        private readonly Context _context;
        private IMtoRepository _service;
        private AlertaEstado _alertaEstado = new();
        private IEpoch _epoch;
        public EquipoRepository(Context context, IMtoRepository service, IEpoch epoch)
        {
            _context = context;
            _service = service;
            _epoch = epoch;
        }

        public async Task<List<Equipo>> GetList(int id)
            => await _context.Equipo.Where(i => i.Estatus.ID == id)
                                    .Include(c => c.Estatus)          .Include(c => c.Ubicacion)
                                    .Include(c => c.Variante)         .Include(c => c.Variante.Motor)
                                    .Include(c => c.Variante.Marca)   .Include(c => c.Variante.TipoEquipo)
                                    .Include(c => c.Variante.Periodo)
                                    .ToListAsync();
        
        public async Task<List<Equipo>> GetList(string cadena)
            => await _context.Equipo.Where(name => name.Variante.TipoEquipo.Nombre.Contains(cadena))
                                    .Include(c => c.Estatus)          .Include(c => c.Ubicacion)
                                    .Include(c => c.Variante)         .Include(c => c.Variante.Motor)
                                    .Include(c => c.Variante.Marca)   .Include(c => c.Variante.TipoEquipo)
                                    .Include(c => c.Variante.Periodo)
                                    .ToListAsync();

        public async Task<List<Equipo>> GetList()
            => await _context.Equipo
                                    .Include(c => c.Estatus)          .Include(c => c.Ubicacion)
                                    .Include(c => c.Variante)         .Include(c => c.Variante.Motor)
                                    .Include(c => c.Variante.Marca)   .Include(c => c.Variante.TipoEquipo)
                                    .Include(c => c.Variante.Periodo) .Include(c => c.Programados).ThenInclude(m => m.Mantenimiento)
                                    .ToListAsync();

        public async Task<List<Equipo>> GetList(FiltrosDTO filtros)
        {
            IQueryable<Equipo> query = _context.Equipo.Include(c => c.Estatus)
                                                      .Include(c => c.Ubicacion)
                                                      .Include(c => c.Variante)
                                                      .Include(c => c.Variante.Motor)
                                                      .Include(c => c.Variante.Marca)
                                                      .Include(c => c.Variante.Periodo)
                                                      .Include(c => c.Variante.TipoEquipo);
            //TIPO
            if (filtros.TipoID != 0)
            {
                query = query.Where(e => e.Variante.TipoEquipoID == filtros.TipoID);
            }
            //MARCA
            if (filtros.MarcaID != 0)
            {
                query = query.Where(e => e.Variante.MarcaID == filtros.MarcaID);
            }
            //MOTOR
            if (filtros.MotorID != 0)
            {
                query = query.Where(e => e.Variante.MotorID == filtros.MotorID);
            }
            //UBICACIÓN
            if (filtros.UbicacionID != 0)
            {
                query = query.Where(e => e.UbicacionID == filtros.UbicacionID);
            }

            return await query.ToListAsync();
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private AlertaEstado DetectErrors(CtrlEquipoViewModel viewModel)
        {
            DateTime present = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            int comparacion = present.CompareTo(viewModel.Plantilla.UltimaAplicacion);

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            if (comparacion < 0)
            {
                _alertaEstado.Leyenda = "No se puede establecer una fecha superior a la actual";
                _alertaEstado.Estado = false;

                return _alertaEstado;
            }
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            DateTime ProximoMto =
                viewModel.Plantilla.UltimaAplicacion.AddMonths(GetMonths(viewModel).GetAwaiter().GetResult());

            var MonthAndYear = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0); 

            if (ProximoMto.CompareTo(MonthAndYear) == -1)
            {
                string fechaInicio = _epoch.ObtenerMesYAnio(viewModel.Plantilla.UltimaAplicacion);
                string fechaProximoMantenimiento = _epoch.ObtenerMesYAnio(ProximoMto);

                _alertaEstado.Leyenda = $"No es posible establecer \"{fechaInicio}\" como fecha de inicio, " +
                    $"ya que la programación para el próximo mantenimiento sería en \"{fechaProximoMantenimiento}\", " +
                    $"y esta fecha es menor a la actual.";

                _alertaEstado.Estado = false;

                return _alertaEstado;
            }
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

            _alertaEstado.Estado = true;
            return _alertaEstado;
        }

        public async Task<AlertaEstado> Add(CtrlEquipoViewModel viewModel)
        {
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            _alertaEstado = DetectErrors(viewModel);
            if (!_alertaEstado.Estado)
                return _alertaEstado;
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

            int meses = await GetMonths(viewModel);

            var equipos = viewModel.NumerosSerie.Select(serie => new Equipo
            {
                NumSerie = serie,
                VarianteID = viewModel.Plantilla.VarianteID,
                UbicacionID = viewModel.Plantilla.UbicacionID,
                EstatusID = viewModel.Plantilla.EstatusID,
                CostoAdquisicion = viewModel.Plantilla.CostoAdquisicion ?? 0,
                Estado = true,
                Funcion = viewModel.Plantilla.Funcion,
                Programados = new List<MtoProgramado>
                {
                    _service.CrearObjeto(viewModel.Plantilla.UltimaAplicacion, meses)
                }
            }).ToList();

            await _context.Equipo.AddRangeAsync(equipos);

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            _alertaEstado.Leyenda = "Datos Registrados correctamente";
            _alertaEstado.Estado = true;

            return _alertaEstado;
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        }
        public AlertaEstado Update(EditEquipoViewModel model)
        {
            var equipo = _context.Find<Equipo>(model.ID);
            var nombre = _context.Equipo.Any(c => c.NumSerie.Contains(model.NumSerie) && c.ID != model.ID);

            if (!nombre)
                equipo.NumSerie = model.NumSerie.Trim();
            else
            {
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                _alertaEstado.Leyenda = "El número de serie ingresado ya esta asignado a otro equipo.";
                _alertaEstado.Estado = false;

                return _alertaEstado;
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            }

            if (model.CostoAdquisicion.HasValue && model.CostoAdquisicion.Value != 0)
                equipo.CostoAdquisicion = model.CostoAdquisicion.Value;

            equipo.UbicacionID = model.UbicacionID;
            equipo.EstatusID = model.EstatusID;
            equipo.Funcion = model.Funcion;

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            _alertaEstado.Leyenda = "Datos Actualizados correctamente";
            _alertaEstado.Estado = true;

            return _alertaEstado;
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        }

        public void Delete(Equipo entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Equipo?> GetById(int ID)
            => await _context.Equipo.Include(c=>c.Variante)
                                    .Include(c => c.Variante.Marca)
                                    .Include(c => c.Variante.Motor)
                                    .Include(c => c.Variante.TipoEquipo)
                                    .Include(c => c.Variante.Periodo)
                                    .Include(c => c.Ubicacion)
                                    .Include(c => c.Estatus)
                                    .FirstOrDefaultAsync(c => c.ID == ID);



        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private async Task<int> GetMonths(CtrlEquipoViewModel viewModel)
            => await _context.Variante
                        .Where(c => c.ID == viewModel.Plantilla.VarianteID)
                        .Select(c => c.Periodo.Meses)
                        .FirstOrDefaultAsync();

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        public async Task<Equipo?> UpdateID(int id)
        {
            var equipo = await _context.Equipo.FirstOrDefaultAsync(c => c.ID == id);
            equipo.Estado = !equipo.Estado;
            return equipo;
        }
    }
}
