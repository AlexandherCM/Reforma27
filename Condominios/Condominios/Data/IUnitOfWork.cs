using Condominios.Data.Interfaces.IRepositories;
using Condominios.Data.Repositories.Catalogos;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;

namespace Condominios.Data
{
    public interface IUnitOfWork : IDisposable
    {
        public ICatalogoRepository<Marca> MarcaRepository { get; }
        public ICatalogoRepository<Motor> MotorRepository { get; }
        public ICatalogoRepository<Periodo> PeriodoRepository { get; }
        public ICatalogoRepository<Ubicacion> UbicacionRepository { get; }
        public ICatalogoRepository<Estatus> EstatusRepository { get; }
        public IEquipoRepository<Equipo> EquipoRepository { get; }
        public ICatalogoRepository<TipoEquipo> TipoEquipoRepository { get; }
        public ICatalogoRepository<TipoMantenimiento> TipoMtoRepository { get; }
        public IVarianteRepository<VarianteDTO> VarianteRepository { get; }
        public Task Save();
    }
}
