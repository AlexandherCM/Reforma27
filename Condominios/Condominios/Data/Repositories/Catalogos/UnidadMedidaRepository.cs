using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602


namespace Condominios.Data.Repositories.Catalogos
{
    public class UnidadMedidaRepository : Catalogo, ICatalogoRepository<UnidadMedida>
    {
        private AlertaEstado _alertaEstado = new();
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

        public async Task<AlertaEstado> add(CatalogoViewModel viewModel)
        {
            if (context.UnidadMedida.Any(te => te.Nombre == viewModel.CatalogoGralViewModel.Nombre))
            {
                _alertaEstado.Leyenda = "¡Ya existe una unidad de medida con ese nombre!";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            UnidadMedida unidadMedida = new()
            {
                Nombre = viewModel.CatalogoGralViewModel.Nombre,
                Estado = true
            };

            context.UnidadMedida.Add(unidadMedida);
            _alertaEstado.Leyenda = "Medida registrada.";
            _alertaEstado.Estado = true;
            return _alertaEstado;
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

        public async Task<List<UnidadMedida>> GetActiveList()
            => await context.UnidadMedida.Where(c => c.Estado).ToListAsync();

        public void UpdateEstateById(int id)
        {
            var unidadMedida = context.Find<UnidadMedida>(id);
            unidadMedida.Estado = !unidadMedida.Estado;
        }

        public async Task<AlertaEstado> Update(CatalogoViewModel viewModel)
        {
            var unidadMedida = context.Find<UnidadMedida>(viewModel.ID);

            if (context.UnidadMedida.Any(m => m.Nombre == viewModel.CatalogoGralViewModel.Nombre && m.ID != viewModel.ID))
            {
                _alertaEstado.Leyenda = "¡Ya existe una unidad de medida con ese nombre!";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            unidadMedida.Nombre = viewModel.CatalogoGralViewModel.Nombre;
            _alertaEstado.Leyenda = "Unidad de medida actualizada correctamente.";
            _alertaEstado.Estado = true;
            return _alertaEstado;

        }
    }
}
