using Condominios.Data.Interfaces;
using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.Equipos
{
    public class EquipoRepository : IEquipoRepository<Equipo>
    {
        private readonly Context _context;
        private IMtoRepository _service;
        private AlertaEstado _alertaEstado = new();
        public EquipoRepository(Context context, IMtoRepository service)
        {
            _context = context;
            _service = service;
        }

        public async Task<List<Equipo>> GetList(int id)
            => await _context.Equipo.Where(i => i.Estatus.ID == id)
                                    .Include(c => c.Estatus)          .Include(c => c.Ubicacion)
                                    .Include(c => c.Variante)         .Include(c => c.Variante.Motor)
                                    .Include(c => c.Variante.Marca)   .Include(c => c.Variante.TipoEquipo)
                                    .ToListAsync();
        
        public async Task<List<Equipo>> GetList(string cadena)
            => await _context.Equipo.Where(name => name.Variante.TipoEquipo.Nombre.Contains(cadena))
                                    .Include(c => c.Estatus)          .Include(c => c.Ubicacion)
                                    .Include(c => c.Variante)         .Include(c => c.Variante.Motor)
                                    .Include(c => c.Variante.Marca)   .Include(c => c.Variante.TipoEquipo)
                                    .ToListAsync();

        public async Task<List<Equipo>> GetList()
            => await _context.Equipo
                                    .Include(c => c.Estatus)         .Include(c => c.Ubicacion)
                                    .Include(c => c.Variante)        .Include(c => c.Variante.Motor)
                                    .Include(c => c.Variante.Marca)  .Include(c => c.Variante.TipoEquipo)
                                    .ToListAsync();

        public async Task<List<Equipo>> GetList(FiltrosDTO filtros)
        {
            IQueryable<Equipo> query = _context.Equipo.Include(c => c.Estatus)
                                                      .Include(c => c.Ubicacion)
                                                      .Include(c => c.Variante)
                                                      .Include(c => c.Variante.Motor)
                                                      .Include(c => c.Variante.Marca)
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

        public async Task<AlertaEstado> Add(CtrlEquipoViewModel viewModel)
        {
            DateTime present = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // -1 = La fecha es menor que la segunda
            // 0  = La fecha es igual que la segunda
            // 1  = La fecha es mayor que la segunda
            int comparacion = present.CompareTo(viewModel.Plantilla.UltimaAplicacion);

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            if (comparacion < 0)
            {
                _alertaEstado.Leyenda = "No se puede establecer un mes mayor al actual";
                _alertaEstado.Estado = false;

                return _alertaEstado;
            }
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

            int meses = await _context.Variante
                        .Where(c => c.ID == viewModel.Plantilla.VarianteID)
                        .Select(c => c.Periodo.Meses)
                        .FirstOrDefaultAsync();

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

        public void Delete(Equipo entity)
        {
            throw new NotImplementedException();
        }

        public Equipo GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Equipo entity)
        {
            throw new NotImplementedException();
        }

    }
}
