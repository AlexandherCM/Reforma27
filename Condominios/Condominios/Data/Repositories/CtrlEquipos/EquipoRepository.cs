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
#pragma warning disable CS8600

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
                                    .Include(c => c.Estatus).Include(c => c.Ubicacion)
                                    .Include(c => c.Variante).Include(c => c.Variante.Motor)
                                    .Include(c => c.Variante.Marca).Include(c => c.Variante.TipoEquipo)
                                    .Include(c => c.Variante.Periodo)
                                    .ToListAsync();

        public async Task<List<Equipo>> GetList(string cadena)
            => await _context.Equipo.Where(name => name.Variante.TipoEquipo.Nombre.Contains(cadena))
                                    .Include(c => c.Estatus).Include(c => c.Ubicacion)
                                    .Include(c => c.Variante).Include(c => c.Variante.Motor)
                                    .Include(c => c.Variante.Marca).Include(c => c.Variante.TipoEquipo)
                                    .Include(c => c.Variante.Periodo)
                                    .ToListAsync();

        public async Task<List<Equipo>> GetList()
            => await _context.Equipo
                                    .Include(c => c.Estatus).Include(c => c.Ubicacion)
                                    .Include(c => c.Variante).Include(c => c.Variante.Motor)
                                    .Include(c => c.Variante.Marca).Include(c => c.Variante.TipoEquipo)
                                    .Include(c => c.Variante.Periodo).Include(c => c.Programados).ThenInclude(m => m.Mantenimiento)
                                    .ToListAsync();

        public async Task<List<EquipoReplaceDTO>> GetListReplace(int varianteID, string NumSerieEpoActual)
        {
            var equipos = await _context.Equipo
                                        .Include(c => c.Estatus).Include(c => c.Ubicacion)
                                        .Include(c => c.Variante).Include(c => c.Variante.Motor)
                                        .Include(c => c.Variante.Marca).Include(c => c.Variante.TipoEquipo)
                                        .Include(c => c.Variante.Periodo).Include(c => c.Programados).ThenInclude(m => m.Mantenimiento)
                                        .Where(c => c.Estado && c.VarianteID == varianteID && c.NumSerie != NumSerieEpoActual)
                                        .ToListAsync();

            var equiposConRemplazo = await _context.Equipo
                                                   .Where(c => !c.Estado && c.CadenaRemplazado != null)
                                                   .ToListAsync();

            var equiposDisponibles = equipos.Where(c => !equiposConRemplazo.Any(r => r.CadenaRemplazado.Contains(c.NumSerie))).ToList();

            var equiposDTO = equiposDisponibles.Select(equipo => new EquipoReplaceDTO
            {
                ID = equipo.ID,
                Nombre = $"{equipo.Variante.TipoEquipo.Nombre} - Serie: {equipo.NumSerie} - Marca: {equipo.Variante.Marca.Nombre}" +
                         $" - Motor: {equipo.Variante.Motor.Nombre} - Capacidad: {equipo.Variante.Capacidad}"
            }).ToList();

            return equiposDTO;
        }

        public async Task<List<Equipo>> GetListWithMtoPending()
            => await _context.Equipo.Include(c => c.Estatus).Include(c => c.Ubicacion)
                                    .Include(c => c.Variante).Include(c => c.Variante.Motor)
                                    .Include(c => c.Variante.Marca).Include(c => c.Variante.TipoEquipo)
                                    .Include(c => c.Variante.Periodo).Include(c => c.Programados.Where(c => c.Estado == true))
                                    .Where(c => c.Estado)
                                    .ToListAsync();

        public async Task<List<Equipo>> GetList(FiltrosDTO filtros)
        {
            IQueryable<Equipo> query = _context.Equipo.Include(c => c.Estatus)
                                                      .Include(c => c.Ubicacion)
                                                      .Include(c => c.Variante)
                                                      .Include(c => c.Variante.Motor)
                                                      .Include(c => c.Variante.Marca)
                                                      .Include(c => c.Variante.Periodo)
                                                      .Include(c => c.Variante.TipoEquipo)
                                                      .Include(c => c.Programados).ThenInclude(m => m.Mantenimiento);
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
                _alertaEstado.Leyenda = 
                    "¡No se puede establecer una fecha superior a la actual como fecha de última aplicación!";
                _alertaEstado.Estado = false;

                return _alertaEstado;
            }
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            DateTime ProximoMto =
                viewModel.Plantilla.UltimaAplicacion.AddMonths(GetMonths(viewModel.Plantilla.VarianteID).GetAwaiter().GetResult());

            var MonthAndYear = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);

            if (ProximoMto.CompareTo(MonthAndYear) == -1)
            {
                string fechaInicio = _epoch.ObtenerMesYAnio(viewModel.Plantilla.UltimaAplicacion);
                string fechaProximoMantenimiento = _epoch.ObtenerMesYAnio(ProximoMto);

                _alertaEstado.Leyenda = $"¡No es posible establecer \"{fechaInicio}\" como fecha de inicio, " +
                    $"ya que la programación para el próximo mantenimiento sería en \"{fechaProximoMantenimiento}\", " +
                    $"y esta fecha es menor a la actual¡";

                _alertaEstado.Estado = false;

                return _alertaEstado;
            }
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            var numerosSerieProcesados = new HashSet<string>();

            foreach (var SerieEquipo in viewModel.NumerosSerie)
            {
                // Verificar si el número de serie ya fue procesado
                if (numerosSerieProcesados.Contains(SerieEquipo))
                {
                    _alertaEstado.Leyenda = "¡Los números de serie no pueden ser iguales!";
                    _alertaEstado.Estado = false;
                    return _alertaEstado;
                }

                // Verificar si el número de serie está asignado a otro equipo
                if (_context.Equipo.Any(c => c.NumSerie.Equals(SerieEquipo)))
                {
                    _alertaEstado.Leyenda = $"¡El número de serie \"{SerieEquipo}\" ya está asignado a otro equipo!";
                    _alertaEstado.Estado = false;
                    return _alertaEstado;
                }

                // Agregar el número de serie a la lista de procesados
                numerosSerieProcesados.Add(SerieEquipo);
            }

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

            int meses = await GetMonths(viewModel.Plantilla.VarianteID);

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
                    _service.CreateObjectOfNewMtoProgrammed(viewModel.Plantilla.UltimaAplicacion, meses)
                }
            }).ToList();

            await _context.Equipo.AddRangeAsync(equipos);

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            _alertaEstado.Leyenda = "Datos Registrados correctamente.";
            _alertaEstado.Estado = true;

            return _alertaEstado;
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        }
        public AlertaEstado Update(EditEquipoViewModel model)
        {
            var equipo = _context.Equipo.Include(e => e.Programados).FirstOrDefault(e => e.ID == model.ID);
            var equipoEncontrado = _context.Equipo.Any(c => c.NumSerie.Equals(model.NumSerie) && c.ID != model.ID);

            string GetEstatusForModel = _context.Estatus.Where(c => c.ID == model.EstatusID).Select(c => c.Nombre).First();
            string GetEstatusForEquipo = _context.Estatus.Where(c => c.ID == equipo.EstatusID).Select(c => c.Nombre).First();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

            if (!equipoEncontrado)
                equipo.NumSerie = model.NumSerie.Trim();
            else
            {
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                _alertaEstado.Leyenda = "¡El número de serie ingresado ya esta asignado a otro equipo!";
                _alertaEstado.Estado = false;

                return _alertaEstado;
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            }
            // Dejar fuera de servicio al equipo - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            if (GetEstatusForModel.Equals("Fuera de servicio") && !GetEstatusForEquipo.Equals("Fuera de servicio"))
            {
                //Mtp programado activo del equipo
                var mtoProgramado = _context.MtoProgramado.FirstOrDefault(c => c.Estado && c.EquipoID == equipo.ID);

                equipo.Estado = false;
                mtoProgramado.Estado = false;
            }
            // Reincorporar equipo a los mtos - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            else if (!GetEstatusForModel.Equals("Fuera de servicio") && GetEstatusForEquipo.Equals("Fuera de servicio"))
            {
                if (model.RetomarFecha == null)
                {
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                    _alertaEstado.Leyenda = "¡Ingrese la fecha en que será programado el mantenimiento!";
                    _alertaEstado.Estado = false;

                    return _alertaEstado;
                }

                // Obtener fecha que asigna el mes de mto
                DateTime RetomarMto = new DateTime(model.RetomarFecha?.Year ?? 0, model.RetomarFecha?.Month ?? 0, 1, 0, 0, 0);
                int meses = GetMonths(equipo.VarianteID).GetAwaiter().GetResult();

                if (RetomarMto < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0))
                {
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                    _alertaEstado.Leyenda = "¡No se puede programar un mantenimiento en una fecha inferior a la actual!";
                    _alertaEstado.Estado = false;

                    return _alertaEstado;
                }

                long fechaEpoch = _epoch.CrearEpoch(RetomarMto);
                MtoProgramado programado = equipo.Programados.Where(c => c.ProximaAplicacion == fechaEpoch).FirstOrDefault();

                if (programado != null && programado.Aplicado)
                {
                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                    _alertaEstado.Leyenda = "¡Ya existe un mantenimiento aplicado en esta fecha!";
                    _alertaEstado.Estado = false;

                    return _alertaEstado;
                }

                //Volver activar el mto pasado
                if (programado != null)
                {
                    programado.Estado = true;
                    //Remover algún mto programado superior a la nueva calendarización
                    foreach (var item in equipo.Programados)
                    {
                        if (item.ProximaAplicacion > fechaEpoch)
                            _context.MtoProgramado.Remove(item);
                    }
                }
                //Crear un nuevo mtoProgramado
                else
                {
                    programado = equipo.Programados.OrderByDescending(c => c.ProximaAplicacion).FirstOrDefault();
                    equipo.Programados
                          .Add(_service.CreateObjectOfNewMtoProgrammed(RetomarMto = RetomarMto.AddMonths(-meses), meses, _epoch.ObtenerFecha(programado.ProximaAplicacion)));

                    //Remover algún mto programado superior a la nueva calendarización
                    foreach (var item in equipo.Programados)
                    {
                        if (item.ProximaAplicacion > fechaEpoch)
                            _context.MtoProgramado.Remove(item);
                    }
                }
                // Activar Equipo
                equipo.Estado = true;
            }

            equipo.CostoAdquisicion = model.CostoAdquisicion ?? 0;
            equipo.UbicacionID = model.UbicacionID;
            equipo.EstatusID = model.EstatusID;
            equipo.Funcion = model.Funcion;
            equipo.CadenaRemplazado = !string.IsNullOrEmpty(model.CadenaRemplazado) ? model.CadenaRemplazado : null;

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            _alertaEstado.Leyenda = "Datos Actualizados correctamente.";
            _alertaEstado.Estado = true;

            return _alertaEstado;
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        }

        public void Delete(Equipo entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Equipo?> GetById(int ID)
            => await _context.Equipo.Include(c => c.Variante)
                                    .Include(c => c.Variante.Marca)
                                    .Include(c => c.Variante.Motor)
                                    .Include(c => c.Variante.TipoEquipo)
                                    .Include(c => c.Variante.Periodo)
                                    .Include(c => c.Ubicacion)
                                    .Include(c => c.Estatus)
                                    .FirstOrDefaultAsync(c => c.ID == ID);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        private async Task<int> GetMonths(int varianteID)
            => await _context.Variante
                        .Where(c => c.ID == varianteID)
                        .Select(c => c.Periodo.Meses)
                        .FirstOrDefaultAsync();

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 

        public async Task<Equipo?> UpdateID(int id)
        {
            var equipo = await _context.Equipo.FirstOrDefaultAsync(c => c.ID == id);
            equipo.Estado = !equipo.Estado;
            return equipo;
        }

        public async Task<string> CalculateTimes(DateTime date, int varianteID, int inputs)
        {
            int meses = await GetMonths(varianteID);
            DateTime ProximaAplicacion = date.AddMonths(meses);

            string cadena = inputs > 1
                ? "El proximo mantenimiento para los equipos será en"
                : "El proximo mantenimiento para el equipo será en";

            return $"{cadena} {_epoch.ObtenerMesYAnio(ProximaAplicacion)}.";
        }
    }
}
