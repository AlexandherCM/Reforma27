using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.Catalogos
{
    public class UbicacionRepository : Catalogo, ICatalogoRepository<Ubicacion>
    {
        private AlertaEstado _alertaEstado = new();
        public UbicacionRepository(Context context) : base(context) { }

        public async Task<List<Ubicacion>> GetList()
            => await context.Ubicacion.ToListAsync();

        public async Task<List<Ubicacion>> GetActiveList()
            => await context.Ubicacion.Where(c => c.Estado).ToListAsync();


        public void Delete(Ubicacion entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstateById(int id)
        {
            var ubicacion = context.Find<Ubicacion>(id);
            ubicacion.Estado = !ubicacion.Estado;
        }

        public Task<Ubicacion?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AlertaEstado> add(CatalogoViewModel viewModel)
        {
            if (context.Ubicacion.Any(u => u.Nombre == viewModel.CatalogoGralViewModel.Nombre))
            {
                _alertaEstado.Leyenda = "¡Ya existe una ubicacion con ese nombre!";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            Ubicacion ubicacion = new()
            {
                Nombre = viewModel.CatalogoGralViewModel.Nombre,
                Estado = true
            };
            context.Ubicacion.Add(ubicacion);
            _alertaEstado.Leyenda = "Ubicacion registrada.";
            _alertaEstado.Estado = true;
            return _alertaEstado;
        }

        public async Task<AlertaEstado> Update(CatalogoViewModel viewModel)
        {
            var ubicacion = context.Find<Ubicacion>(viewModel.ID);

            if (context.Ubicacion.Any(m => m.Nombre == viewModel.CatalogoGralViewModel.Nombre && m.ID != viewModel.ID))
            {
                _alertaEstado.Leyenda = "¡Ya existe una ubicacion con ese nombre!";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            ubicacion.Nombre = viewModel.CatalogoGralViewModel.Nombre;
            _alertaEstado.Leyenda = "Ubicacion actualizada correctamente.";
            _alertaEstado.Estado = true;
            return _alertaEstado;

        }
    }
}
