using Condominios.Data.Interfaces;
using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602
#pragma warning disable CS8600

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
            MtoProgramado mantenimiento = new()
            {
                UltimaAplicacion =
                            _epoch.CrearEpoch(UltimaAplicacion),
                ProximaAplicacion =
                            _epoch.CrearEpoch(UltimaAplicacion.AddSeconds(meses)),

                Aplicable = true,
                Aplicado = false
            };

            return mantenimiento;
        }

        public async Task<List<MtoProgramado>> GetMtosProgramados() 
            => await _context.MtoProgramado.Include(r => r.Equipo)
                                           .Include(r => r.Equipo.Variante.Periodo)
                                           .Where(c=>
                                                  c.Aplicable == true && 
                                                  c.Aplicado == false).ToListAsync();
        // Save para la tarea en segundo plano
        public void AddEntity(MtoProgramado mto)
            => _context.Add(mto);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public DateTime ObtenerFecha(double epoch)
            => _epoch.ObtenerFecha(epoch);

        public long CrearEpoch(DateTime fecha)
            => _epoch.CrearEpoch(fecha);

        public string ObtenerMesYAnio(DateTime fecha)
        {
            throw new NotImplementedException();
        }

    }
}
