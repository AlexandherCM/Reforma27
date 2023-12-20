using Condominios.Data;
using Condominios.Models;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Catalogos;

namespace Condominios.Models.Services
{
    public class CatalogoService
    {
        private readonly IUnitOfWork _uniOfWork;
        private readonly CatalogoViewModel _viewModel = new();
        private AlertaEstado _alertaEstado = new();
        public CatalogoService(IUnitOfWork uniOfWork)
        {
            _uniOfWork = uniOfWork;
        }
        public async Task<AlertaEstado> InsertarEntidad(CatalogoViewModel viewModel)
        {
            switch (viewModel.Entidad)
            {
                case "Marca":
                    _alertaEstado = await _uniOfWork.MarcaRepository.add(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;

                case "Motor":
                    _alertaEstado = await _uniOfWork.MotorRepository.add(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;

                case "Periodo":
                    _alertaEstado = await _uniOfWork.PeriodoRepository.add(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;

                case "Ubicacion":
                    _alertaEstado = await _uniOfWork.UbicacionRepository.add(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;

                case "Estatus":
                    _alertaEstado = await _uniOfWork.EstatusRepository.add(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;

                case "TipoMantenimiento":
                    _alertaEstado = await _uniOfWork.TipoMtoRepository.add(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;

                case "UnidadMedida":
                    _alertaEstado = await _uniOfWork.UnidadMedidaRepository.add(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;

                case "TipoEquipo":
                    _alertaEstado = await _uniOfWork.TipoEquipoRepository.add(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;
            }

            return _alertaEstado;
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

        public async Task<AlertaEstado> Update(CatalogoViewModel viewModel)
        {
            switch (viewModel.Entidad)
            {
                case "Marca":
                    _alertaEstado = await _uniOfWork.MarcaRepository.Update(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;
                case "Ubicacion":
                    _alertaEstado = await _uniOfWork.UbicacionRepository.Update(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;
                case "Motor":
                    _alertaEstado = await _uniOfWork.MotorRepository.Update(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;
                case "TipoMantenimiento":
                    _alertaEstado = await _uniOfWork.TipoMtoRepository.Update(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;
                case "Estatus":
                    _alertaEstado = await _uniOfWork.EstatusRepository.Update(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;
                case "TipoEquipo":
                    _alertaEstado = await _uniOfWork.TipoEquipoRepository.Update(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;
                case "UnidadMedida":
                    _alertaEstado = await _uniOfWork.UnidadMedidaRepository.Update(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;
                case "Periodo":
                    _alertaEstado = await _uniOfWork.PeriodoRepository.Update(viewModel);
                    if (_alertaEstado.Estado)
                    {
                        await _uniOfWork.Save();
                    }
                    break;
            }
            return _alertaEstado;
            
        }
    }
}
