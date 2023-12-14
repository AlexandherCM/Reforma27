using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.Catalogos
{
    public class MarcaRepository : Catalogo, ICatalogoRepository<Marca>
    {
        public MarcaRepository(Context context) : base(context) { }

        public async Task<List<Marca>> GetList()
            => await context.Marca.ToListAsync();

        public void Add(CatalogoViewModel viewModel)
        {
            Marca marca = new()
            {
                Nombre = viewModel.CatalogoGralViewModel.Nombre,
                Estado = true
            };

            context.Marca.Add(marca);
        }
        public void Delete(Marca entity)
        {
            throw new NotImplementedException();
        }
        public void Update(CatalogoViewModel viewModel)
        {
            var marca = context.Find<Marca>(viewModel.ID);
            marca.Nombre = viewModel.CatalogoGralViewModel.Nombre;
        }


        public void UpdateEstateById(int id)
        {
            var Marca = context.Find<Marca>(id);
            Marca.Estado = !Marca.Estado;
        }

        public Task<Marca?> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
