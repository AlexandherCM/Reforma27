using Condominios.Data.Interfaces;
using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Models.ViewModels.CtrolMantenimientos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
#pragma warning disable CS8602
#pragma warning disable CS8603

namespace Condominios.Data.Repositories.Mantenimientos
{
    public class MtoRepository : IMtoRepository
    {
        private readonly IEpoch _epoch;
        private AlertaEstado _alertaEstado = new();
        private Context _context;
        private Dictionary<int, string> Status { get; } = new Dictionary<int, string>
        {
            { 1, "Pendiente" },
            { 2, "Aplicado" },
            { 3, "No aplicado" }
        };
        private Dictionary<string, string> DictGtos = new Dictionary<string, string>
        {
            { "GtosMto", "" },
            { "GtosRep", "" },
        };

        public MtoRepository(Context context, IEpoch epoch)
        {
            _epoch = epoch;
            _context = context;
        }

        public async Task<AlertaEstado> ConfirmMto(MantenimientoViewModel viewModel)
        {
            var mtoProgramado = await _context.MtoProgramado
                                        .Include(m => m.Equipo.Variante.Periodo)
                                        .FirstOrDefaultAsync(m => m.ID == viewModel.MtoProgramadoID);

            DateTime Applic = viewModel.FechaAplicacion;
            DateTime MonthYear = new DateTime(Applic.Year, Applic.Month, 1, 0, 0, 0);
            DateTime Programming = _epoch.ObtenerFecha(mtoProgramado.ProximaAplicacion);

            if (!mtoProgramado.Aplicable)
            {
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                _alertaEstado.Leyenda = "Aún no esta activo el periodo de aplicación para este mantenimiento.";
                _alertaEstado.Estado = false;

                return _alertaEstado;
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            }

            if (Programming.CompareTo(MonthYear) != 0)
            {
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                _alertaEstado.Leyenda = "La fecha de aplicación debe estar dentro del periodo programado.";
                _alertaEstado.Estado = false;

                return _alertaEstado;
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            }

            mtoProgramado.Mantenimiento = new()
            {
                TipoMantenimientoID = viewModel.TipoMantenimientoID,
                ProveedorID = viewModel.ProveedorID,
                CostoMantenimiento = viewModel.CostoMantenimiento,
                CostoReparacion = viewModel.CostoReparacion ?? 0,
                Observaciones = viewModel.Observaciones,
                FechaAplicacion = _epoch.CrearEpoch(viewModel.FechaAplicacion)
            };

            mtoProgramado.Aplicado = true;
            mtoProgramado.Aplicable = false;
            mtoProgramado.Estado = false;

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            var newMto = CrearObjeto(Programming, mtoProgramado.Equipo.Variante.Periodo.Meses);
            newMto.EquipoID = mtoProgramado.EquipoID;

            _context.Add(newMto);
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            _alertaEstado.Leyenda =
                $"Mantenimiento aplicado. El proximo mantenimiento sera para {_epoch.ObtenerMesYAnio(_epoch.ObtenerFecha(newMto.ProximaAplicacion))}";
            _alertaEstado.Estado = true;

            return _alertaEstado;
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        }

        public MtoProgramado CrearObjeto(DateTime UltimaAplicacion, int meses)
        {
            DateTime ProximaAplicacion = UltimaAplicacion.AddMonths(meses);

            bool Estado = false;

            if (ProximaAplicacion.Year > DateTime.Now.Year)
                Estado = true;
            else if (ProximaAplicacion.Year == DateTime.Now.Year)
                Estado = ProximaAplicacion.Month <= DateTime.Now.Month;

            MtoProgramado mantenimiento = new()
            {
                UltimaAplicacion =
                    _epoch.CrearEpoch(UltimaAplicacion),
                ProximaAplicacion = _epoch.CrearEpoch(ProximaAplicacion),
                Aplicado = false,
                Aplicable = ProximaAplicacion.Year == DateTime.Now.Year && ProximaAplicacion.Month == DateTime.Now.Month,
                Estado = Estado
            };

            return mantenimiento;
        }

        public async Task CreateNewMtoProgram()
        {
            DateTime flagTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            long flagTimeEpoch = _epoch.CrearEpoch(flagTime);

            var Mtos = await _context.MtoProgramado
                .Include(r => r.Equipo)
                .Include(r => r.Equipo.Variante.Periodo)
                .Where(c => !c.Aplicado && c.Estado)
                .ToListAsync();

            if (!Mtos.Any()) return;

            foreach (var mto in Mtos.Where(m => m.ProximaAplicacion <= flagTimeEpoch))
            {
                if (mto.ProximaAplicacion == flagTimeEpoch)
                {
                    mto.Aplicable = true;
                    continue;
                }

                DateTime UltimaAplicacion = _epoch.ObtenerFecha(mto.ProximaAplicacion);
                DateTime ProximaAplicacion = UltimaAplicacion.AddMonths(mto.Equipo.Variante.Periodo.Meses);

                long EpochProxAplic = _epoch.CrearEpoch(ProximaAplicacion);

                MtoProgramado newMto = new()
                {
                    EquipoID = mto.Equipo.ID,
                    UltimaAplicacion = mto.ProximaAplicacion,
                    ProximaAplicacion = EpochProxAplic,
                    Aplicado = false,
                    Aplicable = ProximaAplicacion.Year == DateTime.Now.Year && ProximaAplicacion.Month == DateTime.Now.Month,
                    Estado = true
                };

                //Agregar a la base de datos
                _context.Add(newMto);
                mto.Aplicable = false;
                mto.Estado = false;
            }

            // Verificar si hay cambios antes de guardar
            if (_context.ChangeTracker.HasChanges())
                await _context.SaveChangesAsync();
        }


        public async Task<List<MtoProgramado>> GetListMtosProgramByID(int ID)
            => await _context.MtoProgramado.Include(c => c.Mantenimiento)
                                           .Include(c => c.Mantenimiento.Proveedor)
                                           .Include(c => c.Equipo)
                                           .Where(c => c.EquipoID == ID).ToListAsync();

        public (List<MtoProgramadoViewModel>, Dictionary<string, string>) Filter(string json, int filter)
        {
            var listMtos = JsonConvert.DeserializeObject<List<MtoProgramadoViewModel>>(json) ?? new List<MtoProgramadoViewModel>();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            var filteredMtos = listMtos
                .Where(c => c.EstadoAplicacion == Status[filter])
                .OrderByDescending(c => c.ProxAplicEpoch)
                .ToList();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            return CaculateGtos(filteredMtos);
        }

        public (List<MtoProgramadoViewModel>, Dictionary<string, string>) Filter(string json, FilterMtos filters)
        {
            var listMtos = JsonConvert.DeserializeObject<List<MtoProgramadoViewModel>>(json) ?? new List<MtoProgramadoViewModel>();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            var filteredMtos = listMtos
                .Where(c => c.ProveedorID != 0 && c.ProveedorID == filters.ProveedorID)
                .Where(mto => mto.DiaDeAplicacionEpoch >= _epoch.CrearEpoch(filters.FechaUno) &&
                              mto.DiaDeAplicacionEpoch <= _epoch.CrearEpoch(filters.FechaDos))
                .OrderByDescending(c => c.ProxAplicEpoch)
                .ToList();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            return CaculateGtos(filteredMtos);
        }

        private (List<MtoProgramadoViewModel>, Dictionary<string, string>) CaculateGtos(List<MtoProgramadoViewModel> filteredMtos)
        {
            CultureInfo cultureInfo = new CultureInfo("es-MX");

            decimal GtosMto = filteredMtos.Sum(g => decimal.Parse(g.CostoMantenimiento.Substring(1)));
            decimal GtosRep = filteredMtos.Sum(g => decimal.Parse(g.CostoReparacion.Substring(1)));

            this.DictGtos["GtosMto"] = GtosMto.ToString("C", cultureInfo);
            this.DictGtos["GtosRep"] = GtosRep.ToString("C", cultureInfo);

            return (filteredMtos, this.DictGtos);
        }

        public List<ConjuntoMtosViewModel> GetMtosPendientes(List<Equipo> equipos)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var pendientes = equipos.GroupBy(equipo => new
            {
                TipoEquipo = equipo.Variante.TipoEquipo.Nombre,
                PeriodoNombre = equipo.Variante.Periodo.Nombre,
                ProxAplic = equipo.Programados.First(c => c.Estado == true).ProximaAplicacion
            })
            .Select(group => new ConjuntoMtosViewModel
            {
                TipoEquipo = group.Key.TipoEquipo,
                Periodo = group.Key.PeriodoNombre,
                FormatDateAplic = 
                    _epoch.ObtenerMesYAnio(_epoch.ObtenerFecha(group.Key.ProxAplic)),
                Cantidad = group.Count(),
                JsonEquipos = JsonConvert.SerializeObject(group.ToList(), jsonSettings)
            }).ToList();

            return pendientes;
        }

    }
}
