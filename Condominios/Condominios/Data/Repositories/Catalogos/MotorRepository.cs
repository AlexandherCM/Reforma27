using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.Catalogos
{
    public class MotorRepository :Catalogo, ICatalogoRepository<Motor>
    {
        public MotorRepository(Context context) : base(context) { }

        public async Task<List<Motor>> GetList()
            => await context.Motor.ToListAsync();

        public void Add(CatalogoViewModel viewModel)
        {
            Motor motor = new()
            {
                Nombre = viewModel.CatalogoGralViewModel.Nombre,
                Estado = true
            };

            context.Motor.Add(motor);
        }
        public void Delete(Motor entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Motor entity)
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
    }
}
