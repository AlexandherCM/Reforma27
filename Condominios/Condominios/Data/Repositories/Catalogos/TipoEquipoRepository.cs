using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.Catalogos
{
    public class TipoEquipoRepository : Catalogo, ICatalogoRepository<TipoEquipo>
    {
        public TipoEquipoRepository(Context context) : base(context) { }

        public void Add(CatalogoViewModel viewModel)
        {
            TipoEquipo tipoEquipo = new()
            {
                Nombre = viewModel.CatalogoGralViewModel.Nombre,
                Estado = true
            };

            context.TipoEquipo.Add(tipoEquipo);
        }

        public void Delete(TipoEquipo entity)
        {
            throw new NotImplementedException();
        }

        public Task<TipoEquipo?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TipoEquipo>> GetList()
            => await context.TipoEquipo.ToListAsync();

        public void Update(CatalogoViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstateById(int id)
        {
            var tipoEquipo = context.Find<TipoEquipo>(id);
            tipoEquipo.Estado = !tipoEquipo.Estado;
        }
    }
}
