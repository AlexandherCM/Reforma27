using Condominios.Data;
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
    }
}
