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

            MtoProgramado mantenimiento = new()
            {
                UltimaAplicacion =
                    _epoch.CrearEpoch(UltimaAplicacion),
                ProximaAplicacion = _epoch.CrearEpoch(ProximaAplicacion),
                Aplicable =
                    ProximaAplicacion.Month == DateTime.Now.Month && ProximaAplicacion.Year == DateTime.Now.Year,
                Aplicado = false
            };

            return mantenimiento;
        }

        public async Task CreateNewMtoProgram()
        {
            DateTime now = DateTime.Now;
            DateTime flagTime = new DateTime(now.Year, now.Month, 1, 0, 0, 0);

            var Mtos = await _context.MtoProgramado
                .Include(r => r.Equipo)
                .Include(r => r.Equipo.Variante.Periodo)
                .Where(c => !c.Aplicado)
                .ToListAsync();

            if (!Mtos.Any()) return;

            foreach (var mto in Mtos.Where(m => m.ProximaAplicacion <= _epoch.CrearEpoch(flagTime)))
            {
                if (mto.ProximaAplicacion == _epoch.CrearEpoch(flagTime))
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
                    Aplicable =
                        ProximaAplicacion.Month == DateTime.Now.Month && ProximaAplicacion.Year == DateTime.Now.Year,
                    Aplicado = false
                };

                //Agregar a la base de datos
                _context.Add(newMto);
                mto.Aplicable = false;
            }

            // Verificar si hay cambios antes de guardar
            if (_context.ChangeTracker.HasChanges())
                await _context.SaveChangesAsync();

        }

        public async Task<MtoProgramado> GetMtoProgramado(int ID)
            => await _context.MtoProgramado
                             .Where(c => c.ID == ID)
                             .OrderByDescending(c => c.ProximaAplicacion)
                             .FirstOrDefaultAsync();
    }
}
