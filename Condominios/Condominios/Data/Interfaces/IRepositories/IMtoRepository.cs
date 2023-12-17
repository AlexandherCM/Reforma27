using Condominios.Models.Entities;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface IMtoRepository : IEpoch
    {
        public MtoProgramado CrearObjeto(DateTime UltimaAplicacion, int meses);
        public Task<List<MtoProgramado>> GetMtosProgramados();
        public Task Save();
        public void AddEntity(MtoProgramado mto);
    }
}
