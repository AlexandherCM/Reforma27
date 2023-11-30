using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;

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

        public TipoMantenimiento GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TipoMantenimiento entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstateById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
