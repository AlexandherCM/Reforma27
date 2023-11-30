using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace Condominios.Data.Repositories.Catalogos
{
    public class EstatusRepository : Catalogo, ICatalogoRepository<Estatus>
    {
        public EstatusRepository(Context context) : base(context) { }

        public async Task<List<Estatus>> GetList()
            => await context.Estatus.ToListAsync();

        public void Add(CatalogoViewModel viewModel)
        {
            Estatus estatus = new()
            {
                Nombre = viewModel.CatalogoGralViewModel.Nombre,
                Estado = true
            };

            context.Estatus.Add(estatus);
        }

        public void Delete(Estatus entity)
        {
            throw new NotImplementedException();
        }

        public Estatus GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Estatus entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstateById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
