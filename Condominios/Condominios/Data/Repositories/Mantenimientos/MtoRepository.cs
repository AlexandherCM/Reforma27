using Condominios.Data.Interfaces;
using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602
#pragma warning disable CS8603

namespace Condominios.Data.Repositories.Mantenimientos
{
    public class MtoRepository : IMtoRepository
    {
        private readonly IEpoch _epoch;
        private Context _context;
        public MtoRepository(Context context, IEpoch epoch)
        {
            _epoch = epoch;
            _context = context;
        }

        public MtoProgramado CrearObjeto(DateTime UltimaAplicacion, int meses)
        {
            DateTime ProximaAplicacion = UltimaAplicacion.AddMonths(meses);

            bool Estado = false;

            if (ProximaAplicacion.Year > DateTime.Now.Year)
                Estado = true;
            else if(ProximaAplicacion.Year == DateTime.Now.Year)
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

        //public async Task<MtoProgramado> GetMtoProgramado(int ID)
        //    => await _context.MtoProgramado
        //                     .Where(c => c.ID == ID)
        //                     .OrderByDescending(c => c.ProximaAplicacion)
        //                     .FirstOrDefaultAsync();

        public async Task<List<MtoProgramado>> GetListMtosByID(int ID)
            => await _context.MtoProgramado.Include(c => c.Mantenimiento)
                                           .Include(c => c.Mantenimiento.Proveedor)
                                           .Include(c => c.Equipo)
                                           .Where(c => c.EquipoID == ID).ToListAsync();

    }
}
