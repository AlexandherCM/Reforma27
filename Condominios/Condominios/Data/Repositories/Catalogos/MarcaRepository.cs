using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Catalogos;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.Catalogos
{
    public class MarcaRepository : Catalogo, ICatalogoRepository<Marca>
    {
        private AlertaEstado _alertaEstado = new();
        public MarcaRepository(Context context) : base(context) { }

        public async Task<List<Marca>> GetList()
            => await context.Marca.ToListAsync();


        public async Task<AlertaEstado> add(CatalogoViewModel viewModel)
        {
            if (context.Marca.Any(e => e.Nombre == viewModel.CatalogoGralViewModel.Nombre))
            {
                _alertaEstado.Leyenda = "Ya existe una marca con ese nombre";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            Marca marca = new()
            {
                Nombre = viewModel.CatalogoGralViewModel.Nombre,
                Estado = true,
            };

            context.Marca.Add(marca);
            _alertaEstado.Leyenda = "Marca registrada";
            _alertaEstado.Estado = true;
            return _alertaEstado;
        }

        public void Delete(Marca entity)
        {
            throw new NotImplementedException();
        }

        public async Task<AlertaEstado> Update(CatalogoViewModel viewModel)
        {
            var marca = context.Find<Marca>(viewModel.ID);

            if (context.Marca.Any(m => m.Nombre == viewModel.CatalogoGralViewModel.Nombre && m.ID != viewModel.ID))
            {
                _alertaEstado.Leyenda = "Ya existe una marca con ese nombre";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            marca.Nombre = viewModel.CatalogoGralViewModel.Nombre;
            _alertaEstado.Leyenda = "Marca actualizada correctamente";
            _alertaEstado.Estado = true;
            return _alertaEstado;

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
