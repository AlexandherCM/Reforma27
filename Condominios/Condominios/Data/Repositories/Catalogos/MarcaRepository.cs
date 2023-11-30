using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;

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
        public void Update(Marca entity)
        {
            throw new NotImplementedException();
        }

        public Marca GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstateById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
