using Condominios.Data;
using Condominios.Models;
using Condominios.Models.ViewModels.Catalogos;

namespace Condominios.Services
{
    public class CatalogoService
    {
        private readonly IUnitOfWork _uniOfWork;
        private readonly CatalogoViewModel _viewModel = new();
        public CatalogoService(IUnitOfWork uniOfWork)
        {
            _uniOfWork = uniOfWork;
        }
        public async Task InsertarEntidad(CatalogoViewModel viewModel)
        {
            switch (viewModel.Entidad)
            {
                case "Marca":
                    _uniOfWork.MarcaRepository.Add(viewModel);
                    await _uniOfWork.Save();
                    return;

                case "Motor":
                    _uniOfWork.MotorRepository.Add(viewModel);
                    await _uniOfWork.Save();
                    return;

                case "Periodo":
                    _uniOfWork.PeriodoRepository.Add(viewModel);
                    await _uniOfWork.Save();
                    return;

                case "Ubicacion":
                    _uniOfWork.UbicacionRepository.Add(viewModel);
                    await _uniOfWork.Save();
                    return;

                case "Estatus":
                    _uniOfWork.EstatusRepository.Add(viewModel);
                    await _uniOfWork.Save();
                    return;

                case "TipoMantenimiento":
                    _uniOfWork.TipoMtoRepository.Add(viewModel);
                    await _uniOfWork.Save();
                    return;

                case "UnidadMedida":
                    _uniOfWork.UnidadMedidaRepository.Add(viewModel);
                    await _uniOfWork.Save();
                    return;

                case "TipoEquipo":
                    _uniOfWork.TipoEquipoRepository.Add(viewModel);
                    await _uniOfWork.Save();
                    return;
            }
        }

        public async Task ActualizarEstado(CatalogoViewModel viewModel)
        {
            switch (viewModel.Entidad)
            {
                case "Marca":
                    _uniOfWork.MarcaRepository.UpdateEstateById(viewModel.ID);
                    await _uniOfWork.Save();
                    return;

                case "Motor":
                    _uniOfWork.MotorRepository.UpdateEstateById(viewModel.ID);
                    await _uniOfWork.Save();
                    return;

                case "Periodo":
                    _uniOfWork.PeriodoRepository.UpdateEstateById(viewModel.ID);
                    await _uniOfWork.Save();
                    return;

                case "Ubicacion":
                    _uniOfWork.UbicacionRepository.UpdateEstateById(viewModel.ID);
                    await _uniOfWork.Save();
                    return;

                case "Estatus":
                    _uniOfWork.EstatusRepository.UpdateEstateById(viewModel.ID);
                    await _uniOfWork.Save();
                    return;

                case "TipoMantenimiento":
                    _uniOfWork.TipoMtoRepository.UpdateEstateById(viewModel.ID);
                    await _uniOfWork.Save();
                    return;

                case "UnidadMedida":
                    _uniOfWork.UnidadMedidaRepository.UpdateEstateById(viewModel.ID);
                    await _uniOfWork.Save();
                    return;

                case "TipoEquipo":
                    _uniOfWork.TipoEquipoRepository.UpdateEstateById(viewModel.ID);
                    await _uniOfWork.Save();
                    return;
            }
        }

        public async Task<CatalogoViewModel> ObtenerCatalogos()
        {
            _viewModel.Marcas = await _uniOfWork.MarcaRepository.GetList();
            _viewModel.Motores = await _uniOfWork.MotorRepository.GetList();
            _viewModel.Periodos = await _uniOfWork.PeriodoRepository.GetList();
            _viewModel.Ubicaciones = await _uniOfWork.UbicacionRepository.GetList();
            _viewModel.Estatus = await _uniOfWork.EstatusRepository.GetList();
            _viewModel.TipoMantenimientos = await _uniOfWork.TipoMtoRepository.GetList();
            _viewModel.unidadMedidas = await _uniOfWork.UnidadMedidaRepository.GetList();
            _viewModel.TipoEquipos = await _uniOfWork.TipoEquipoRepository.GetList();
            return _viewModel;
        }
    }
}
