using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602


namespace Condominios.Data.Repositories.Catalogos
{
    public class UnidadMedidaRepository : Catalogo, ICatalogoRepository<UnidadMedida>
    {
        public UnidadMedidaRepository(Context context) : base(context) { }

        public void Add(CatalogoViewModel viewModel)
        {
            UnidadMedida unidadMedida = new()
            {
                Nombre = viewModel.CatalogoGralViewModel.Nombre,
                Estado = true
            };

            context.UnidadMedida.Add(unidadMedida);
        }

        public void Delete(UnidadMedida entity)
        {
            throw new NotImplementedException();
        }

        public async Task<UnidadMedida?> GetById(int id)
        {
            var NombreUnidad = await context.UnidadMedida.FirstOrDefaultAsync(c => c.ID == id);
            if (NombreUnidad != null)
            {
                NombreUnidad.Nombre = NombreUnidad.Nombre ?? string.Empty;
            }
            return NombreUnidad;
        }

        public async Task<List<UnidadMedida>> GetList()
          => await context.UnidadMedida.ToListAsync();

        public void Update(CatalogoViewModel viewModel)
        {
            var unidadMedida = context.Find<UnidadMedida>(viewModel.ID);
            unidadMedida.Nombre = viewModel.CatalogoGralViewModel.Nombre;
        }

        public void UpdateEstateById(int id)
        {
            var unidadMedida = context.Find<UnidadMedida>(id);
            unidadMedida.Estado = !unidadMedida.Estado;
        }
    }
}
