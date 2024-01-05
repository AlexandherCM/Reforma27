using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.Catalogos
{
    public class MotorRepository :Catalogo, ICatalogoRepository<Motor>
    {
        private AlertaEstado _alertaEstado = new();
        public MotorRepository(Context context) : base(context) { }

        public async Task<List<Motor>> GetList()
            => await context.Motor.ToListAsync();

        public async Task<List<Motor>> GetActiveList()
            => await context.Motor.Where(c => c.Estado).ToListAsync();

        public void Delete(Motor entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstateById(int id)
        {
            var Motor = context.Find<Motor>(id);
            Motor.Estado = !Motor.Estado;
        }

        public Task<Motor?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AlertaEstado> add(CatalogoViewModel viewModel)
        {
            if(context.Motor.Any(m => m.Nombre == viewModel.CatalogoGralViewModel.Nombre))
            {
                _alertaEstado.Leyenda = "Ya existe un motor con ese nombre";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            Motor motor = new()
            {
                Nombre = viewModel.CatalogoGralViewModel.Nombre,
                Estado = true
            };
            context.Motor.Add(motor);
            _alertaEstado.Leyenda = "Motor registrado";
            _alertaEstado.Estado = true;
            return _alertaEstado;
        }

        public async Task<AlertaEstado> Update(CatalogoViewModel viewModel)
        {
            var motor = context.Find<Motor>(viewModel.ID);

            if (context.Motor.Any(m => m.Nombre == viewModel.CatalogoGralViewModel.Nombre && m.ID != viewModel.ID))
            {
                _alertaEstado.Leyenda = "Ya existe un motor con ese nombre";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            motor.Nombre = viewModel.CatalogoGralViewModel.Nombre;
            _alertaEstado.Leyenda = "Motor actualizado correctamente";
            _alertaEstado.Estado = true;
            return _alertaEstado;

        }
    }
}
