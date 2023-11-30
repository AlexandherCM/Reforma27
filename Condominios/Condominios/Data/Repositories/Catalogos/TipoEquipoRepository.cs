using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace Condominios.Data.Repositories.Catalogos
{
    public class TipoEquipoRepository : Catalogo, ICatalogoRepository<TipoEquipo>
    {
        public TipoEquipoRepository(Context context) : base(context) { }

        public void Add(CatalogoViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(TipoEquipo entity)
        {
            throw new NotImplementedException();
        }

        public TipoEquipo GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TipoEquipo>> GetList()
            => await context.TipoEquipo.ToListAsync();

        public void Update(TipoEquipo entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstateById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
