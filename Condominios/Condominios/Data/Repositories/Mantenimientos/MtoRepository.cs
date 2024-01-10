using Condominios.Data.Interfaces;
using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;
using Condominios.Models.ViewModels.CtrolMantenimientos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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
        private Dictionary<string, bool> DictNumberMtos = new Dictionary<string, bool>
        {
            { "Mtos", false },
            { "AplicarReparacion", false },
        };
        private class ResultMto
        {
            public MtoProgramado mto = new();
            public AlertaEstado Alerta = new();
        }

        public MtoRepository(Context context, IEpoch epoch)
        {
            _epoch = epoch;
            _context = context;
        }

        private AlertaEstado ValidateTimes(DateTime FechaAplicacion, long TimeLimit)
        {
            DateTime Applic = new(), MonthYear = new(), Programming = new();

            Applic = FechaAplicacion;
            MonthYear = new DateTime(Applic.Year, Applic.Month, 1, 0, 0, 0);
            Programming = _epoch.ObtenerFecha(TimeLimit);

            if (Programming.CompareTo(MonthYear) != 0)
            {
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                _alertaEstado.Leyenda = "La fecha de aplicación debe estar dentro del periodo programado.";
                _alertaEstado.Estado = false;

                return _alertaEstado;
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            }
            _alertaEstado.Estado = true;
            _alertaEstado.Leyenda = "";
            return _alertaEstado;
        }

        private ResultMto StructureNewMto(MantenimientoViewModel viewModel, MtoProgramado mtoProgramado, Dictionary<string, bool> ManyMtos)
        {
            MtoProgramado newMto = new();
            ResultMto result = new();

            // Fecha de la proxima aplicación de este mto que pasa a ser la ultima aplicación para programar un nuevo mto
            DateTime Programming = _epoch.ObtenerFecha(mtoProgramado.ProximaAplicacion);

            _alertaEstado = ValidateTimes(viewModel.FechaAplicacion, mtoProgramado.ProximaAplicacion);

            if (!mtoProgramado.Aplicable && !viewModel.TimedOut)
            {
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                result.Alerta.Leyenda = ManyMtos["Mtos"]
                    ? "Aún no esta activo el mantenimiento para estos equipos."
                    : "Aún no esta activo el periodo de aplicación para este mantenimiento.";
                result.Alerta.Estado = false;

                return result;
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            }

            if (!_alertaEstado.Estado)
            {
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                result.Alerta.Leyenda = _alertaEstado.Leyenda;
                result.Alerta.Estado = _alertaEstado.Estado;

                return result;
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            }

            mtoProgramado.Mantenimiento = new()
            {
                TipoMantenimientoID = viewModel.TipoMantenimientoID,
                ProveedorID = viewModel.ProveedorID,
                CostoMantenimiento = viewModel.CostoMantenimiento,
                CostoReparacion = ManyMtos["AplicarReparacion"] ? viewModel.CostoReparacion ?? 0 : 0,
                Observaciones = viewModel.Observaciones,
                FechaAplicacion = _epoch.CrearEpoch(viewModel.FechaAplicacion),
            };

            mtoProgramado.Aplicado = true;
            mtoProgramado.Aplicable = false;
            mtoProgramado.Estado = false;

            // Programar el proximo mantenimiento solo si este no es uno caducado - - - - - - - - - 
            if (!viewModel.TimedOut)
            {
                newMto = CreateObjectOfNewMtoProgrammed(Programming, mtoProgramado.Equipo.Variante.Periodo.Meses);
                newMto.EquipoID = mtoProgramado.EquipoID;

                _context.Add(newMto);
            }

            result.mto = newMto;
            result.Alerta.Estado = true;
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            return result;
        }

        public async Task<AlertaEstado> ConfirmarMtos(MantenimientoViewModel viewModel, List<EquipoMtoViewModel> equipos)
        {
            ResultMto result = new();

            foreach (var equipo in equipos)
            {
                //var mtoProgramado = equipo.Programados.First(); // El primer y único elemento en la lista de mtos programados
                var mtoProgramado = await _context.MtoProgramado
                                        .Include(m => m.Equipo.Variante.Periodo)
                                        .FirstOrDefaultAsync(m => m.ID == equipo.Programado.ID) ?? new();

                // Condiciones de los mantenimientos,para definir si aplicar o no la reparación - - - - - - 
                DictNumberMtos["Mtos"] = true;
                DictNumberMtos["AplicarReparacion"] = equipo.AplicarReparacion;
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                result = StructureNewMto(viewModel, mtoProgramado, DictNumberMtos);

                if (result.Alerta.Estado == false)
                    return result.Alerta;
            }
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            string leyenda = equipos.Count > 1
                ? "Mantenimiento aplicado. El proximo mantenimiento para los equipos es para"
                : "Mantenimiento aplicado. El proximo mantenimiento para el equipo es para";

            _alertaEstado.Leyenda =
                $"{leyenda} {_epoch.ObtenerMesYAnio(_epoch.ObtenerFecha(result.mto.ProximaAplicacion))}";
            _alertaEstado.Estado = true;

            return _alertaEstado;
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        }

        public async Task<AlertaEstado> ConfirmarMto(MantenimientoViewModel viewModel)
        {
            ResultMto result = new();
            var mtoProgramado = await _context.MtoProgramado
                                        .Include(m => m.Equipo.Variante.Periodo)
                                        .FirstOrDefaultAsync(m => m.ID == viewModel.MtoProgramadoID) ?? new();
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
            DictNumberMtos["AplicarReparacion"] = true;

            result = StructureNewMto(viewModel, mtoProgramado, DictNumberMtos);

            if (result.Alerta.Estado == false)
                return result.Alerta;

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            _alertaEstado.Leyenda =
                $"Mantenimiento aplicado. El proximo mantenimiento será para {_epoch.ObtenerMesYAnio(_epoch.ObtenerFecha(result.mto.ProximaAplicacion))}";
            _alertaEstado.Estado = true;

            return _alertaEstado;
            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
        }

        public async Task<AlertaEstado> UpdateMto(MantenimientoViewModel viewModel)
        {
            var mtoProgramado = await _context.MtoProgramado.Include(c => c.Mantenimiento)
                                        .FirstOrDefaultAsync(m => m.ID == viewModel.MtoProgramadoID) ?? new();

            if (mtoProgramado.Mantenimiento == null)
            {
                _alertaEstado = await ConfirmarMto(viewModel);

                _alertaEstado.Leyenda = _alertaEstado.Estado ? "Mantenimiento actualizado" : _alertaEstado.Leyenda;
                return _alertaEstado;
            }

            _alertaEstado = ValidateTimes(viewModel.FechaAplicacion, mtoProgramado.ProximaAplicacion);

            if (!_alertaEstado.Estado)
            {
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
                _alertaEstado.Leyenda = _alertaEstado.Leyenda;
                _alertaEstado.Estado = _alertaEstado.Estado;

                return _alertaEstado;
                // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            }

            Mantenimiento mto = new()
            {
                ID = mtoProgramado.Mantenimiento.ID,
                TipoMantenimientoID = viewModel.TipoMantenimientoID,
                ProveedorID = viewModel.ProveedorID,
                CostoMantenimiento = viewModel.CostoMantenimiento,
                CostoReparacion = viewModel.CostoReparacion ?? 0,
                Observaciones = viewModel.Observaciones,
                FechaAplicacion = _epoch.CrearEpoch(viewModel.FechaAplicacion)
            };
            
            _context.Entry(mtoProgramado.Mantenimiento).CurrentValues.SetValues(mto);

            _alertaEstado.Leyenda = "Mantenimiento actualizado";
            _alertaEstado.Estado = true;

            return _alertaEstado;
        }

        public MtoProgramado CreateObjectOfNewMtoProgrammed(DateTime UltimaAplicacion, int meses)
        {
            DateTime ProximaAplicacion = UltimaAplicacion.AddMonths(meses);

            bool Estado = false;

            if (ProximaAplicacion.Year > DateTime.Now.Year)
                Estado = true;
            else if (ProximaAplicacion.Year == DateTime.Now.Year)
                Estado = DateTime.Now.Month <= ProximaAplicacion.Month;

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

        public async Task CreateNewMtoProgramOfBackground()
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
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  

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

        //CARLOS - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        //PDT !!!!
        public List<Equipo> FilterGtosMto(List<Equipo> ListaEquipos, FiltrosGtosMtosDTO Filtros)
        {
            //PROVEEDOR
            if (Filtros.ProveedorID != 0)
            {
                ListaEquipos = ListaEquipos.Where(e => e.Programados.Any(mp => mp.Mantenimiento != null && mp.Mantenimiento.ProveedorID == Filtros.ProveedorID)).ToList();
            }

            if (Filtros.FechaEpoch1 > 0 && Filtros.FechaEpoch2 > 0)
            {
                long fecha1Epoch = Filtros.FechaEpoch1;
                long fecha2Epoch = Filtros.FechaEpoch2;

                ListaEquipos = ListaEquipos.Where(e => e.Programados.Any(mp => mp.Mantenimiento != null &&
                               mp.Mantenimiento.FechaAplicacion >= fecha1Epoch &&
                               mp.Mantenimiento.FechaAplicacion <= fecha2Epoch)).ToList();
            }

            return ListaEquipos;
        }
    }
}
