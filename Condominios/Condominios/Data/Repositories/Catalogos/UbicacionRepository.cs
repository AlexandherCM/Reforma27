using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.Catalogos
{
    public class UbicacionRepository : Catalogo, ICatalogoRepository<Ubicacion>
    {
        public UbicacionRepository(Context context) : base(context) { }

        public async Task<List<Ubicacion>> GetList()
            => await context.Ubicacion.ToListAsync();

        public void Add(CatalogoViewModel viewModel)
        {
            Ubicacion ubicacion = new()
            {
                Nombre = viewModel.CatalogoGralViewModel.Nombre,
                Estado = true
            };

            context.Ubicacion.Add(ubicacion);
        }

        public void Delete(Ubicacion entity)
        {
            throw new NotImplementedException();
        }

        public Ubicacion GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Ubicacion entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstateById(int id)
        {
            var ubicacion = context.Find<Ubicacion>(id);
            ubicacion.Estado = !ubicacion.Estado;
        }
    }
}
