using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.Catalogos
{
    public class PeriodoRepository : Catalogo, ICatalogoRepository<Periodo>
    {
        public PeriodoRepository(Context context) : base(context) { }

        public async Task<List<Periodo>> GetList()
            => await context.Periodo.ToListAsync();

        public void Add(CatalogoViewModel viewModel)
        {
            Periodo periodo = new()
            {
                Nombre = viewModel.PeriodoViewModel.Nombre,
                Meses = viewModel.PeriodoViewModel.Meses(),
                Estado = true
            };

            context.Periodo.Add(periodo);
        }

        public void Delete(Periodo entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstateById(int id)
        {
            var periodo = context.Find<Periodo>(id);
            periodo.Estado = !periodo.Estado;
        }

        public Task<Periodo?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CatalogoViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
