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
            }
        }

        public async Task ActualizarEstado(CatalogoViewModel viewModel)
        {
            switch (viewModel.Entidad)
            {
                case "Marca":
                    await _uniOfWork.Save();
                    return;

                case "Motor":
                    await _uniOfWork.Save();
                    return;

                case "Periodo":
                    _uniOfWork.PeriodoRepository.UpdateEstateById(viewModel.ID);
                    await _uniOfWork.Save();
                    return;

                case "Ubicacion":
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

            return _viewModel;
        }
    }
}
