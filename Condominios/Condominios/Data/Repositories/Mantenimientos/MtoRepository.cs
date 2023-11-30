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
        public MtoRepository(Context context, IEpoch epoch)
        {
            _epoch = epoch;
        }

        public MtoProgramado CrearObjeto(DateTime UltimaAplicacion, int meses)
        {
            MtoProgramado mantenimiento = new()
            {
                UltimaAplicacion =
                            _epoch.CrearEpoch(UltimaAplicacion),
                ProximaAplicacion =
                            _epoch.CrearEpoch(UltimaAplicacion.AddMonths(meses)),

                Aplicado = false,
                Aplicable = false,
            };

            return mantenimiento;
        }
    }
}
