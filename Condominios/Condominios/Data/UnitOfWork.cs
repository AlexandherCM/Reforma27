using Condominios.Data.Interfaces.IRepositories;
using Condominios.Data.Repositories.Catalogos;
using Condominios.Models;
using Condominios.Models.DTOs;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;

namespace Condominios.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        public ICatalogoRepository<Marca> MarcaRepository { get; }
        public IEquipoRepository<Equipo> EquipoRepository { get; }
        public ICatalogoRepository<Motor> MotorRepository { get; }
        public ICatalogoRepository<Periodo> PeriodoRepository { get; }
        public ICatalogoRepository<Ubicacion> UbicacionRepository { get; }
        public ICatalogoRepository<Estatus> EstatusRepository { get; }
        public ICatalogoRepository<TipoEquipo> TipoEquipoRepository { get; }
        public ICatalogoRepository<TipoMantenimiento> TipoMtoRepository { get; }
        public IVarianteRepository<VarianteDTO> VarianteRepository { get; }
        public ICatalogoRepository<UnidadMedida> UnidadMedidaRepository { get; }
        public IProveedorRepository<Proveedor> ProveedorRepository { get; }
        public IMtoRepository MtoRepository { get; }

        public UnitOfWork(Context context,
            ICatalogoRepository<Marca> marcaRepository,
            ICatalogoRepository<Motor> motorRepository,
            ICatalogoRepository<Periodo> periodoRepository,
            ICatalogoRepository<Ubicacion> ubicacionRepository,
            ICatalogoRepository<Estatus> estatusRepository,
            ICatalogoRepository<TipoMantenimiento> tipoMtoRepository,
            ICatalogoRepository<TipoEquipo> tipoEquipoRepository,
            ICatalogoRepository<UnidadMedida> unidadMedidaRepository,
            IEquipoRepository<Equipo> equipoRepository,
            IVarianteRepository<VarianteDTO> varianteRepository,
            IProveedorRepository<Proveedor> proveedorRepository,
            IMtoRepository mtoRepository)
        {
            _context = context;
            MarcaRepository = marcaRepository;
            MotorRepository = motorRepository;
            PeriodoRepository = periodoRepository;
            UbicacionRepository = ubicacionRepository;
            EstatusRepository = estatusRepository;
            TipoMtoRepository = tipoMtoRepository;
            EquipoRepository = equipoRepository;
            VarianteRepository = varianteRepository;
            TipoEquipoRepository = tipoEquipoRepository;
            UnidadMedidaRepository = unidadMedidaRepository;
            ProveedorRepository = proveedorRepository;
            MtoRepository = mtoRepository;
        }
        public async Task Save()
            => await _context.SaveChangesAsync();
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
