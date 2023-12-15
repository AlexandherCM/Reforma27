using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602


namespace Condominios.Data.Repositories.Catalogos
{
    public class TipoMtoRepository : Catalogo, ICatalogoRepository<TipoMantenimiento>
    {
        public TipoMtoRepository(Context context) : base(context) { }

        public async Task<List<TipoMantenimiento>> GetList()
            => await context.TipoMantenimiento.ToListAsync();

        public void Add(CatalogoViewModel viewModel)
        {
            TipoMantenimiento tipoMto = new()
            {
                Nombre = viewModel.CatalogoGralViewModel.Nombre,
                Estado = true
            };

            context.TipoMantenimiento.Add(tipoMto);
        }

        public void Delete(TipoMantenimiento entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstateById(int id)
        {
            var TipoMantenimiento = context.Find<TipoMantenimiento>(id);
            TipoMantenimiento.Estado = !TipoMantenimiento.Estado;
        }

        public Task<TipoMantenimiento?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CatalogoViewModel viewModel)
        {
            var tipoMantenimiento = context.Find<TipoMantenimiento>(viewModel.ID);
            tipoMantenimiento.Nombre = viewModel.CatalogoGralViewModel.Nombre;
        }
    }
}
