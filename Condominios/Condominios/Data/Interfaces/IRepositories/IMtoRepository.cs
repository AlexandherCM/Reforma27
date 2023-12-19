using Condominios.Models.Entities;

namespace Condominios.Data.Interfaces.IRepositories
{
    public interface IMtoRepository
    {
        public MtoProgramado CrearObjeto(DateTime UltimaAplicacion, int meses);
        public Task CreateNewMtoProgram();
        //public Task<MtoProgramado> GetMtoProgramado(int ID);
        public Task<List<MtoProgramado>> GetListMtosByID(int ID);  
    }
}
