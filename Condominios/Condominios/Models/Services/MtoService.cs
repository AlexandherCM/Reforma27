using Condominios.Data;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.CtrolEquipo;

namespace Condominios.Models.Services
{
    public class MtoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MtoService(IUnitOfWork uniOfWork)
        {
            _unitOfWork = uniOfWork;
        }

        public async Task<MtoProgramado> GetMtoProgramado(int ID)
            => await _unitOfWork.MtoRepository.GetMtoProgramado(ID);
    }
}
