using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602


namespace Condominios.Data.Repositories.Catalogos
{
    public class TipoMtoRepository : Catalogo, ICatalogoRepository<TipoMantenimiento>
    {
        private AlertaEstado _alertaEstado = new();
        public TipoMtoRepository(Context context) : base(context) { }

        public async Task<List<TipoMantenimiento>> GetList()
            => await context.TipoMantenimiento.ToListAsync();

        public async Task<List<TipoMantenimiento>> GetActiveList()
            => await context.TipoMantenimiento.Where(c => c.Estado).ToListAsync();

        public void Delete(TipoMantenimiento entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstateById(int id)
        {
            var TipoMantenimiento = context.Find<TipoMantenimiento>(id);
            TipoMantenimiento.Estado = !TipoMantenimiento.Estado;
        }

        public Task<TipoMantenimiento?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AlertaEstado> add(CatalogoViewModel viewModel)
        {
            if(context.TipoMantenimiento.Any(tm => tm.Nombre == viewModel.CatalogoGralViewModel.Nombre))
            {
                _alertaEstado.Leyenda = "¡Ya existe un tipo de mantenimiento con ese nombre!";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            TipoMantenimiento tipoMantenimiento = new()
            {
                Nombre = viewModel.CatalogoGralViewModel.Nombre,
                Estado = true
            };

            context.TipoMantenimiento.Add(tipoMantenimiento);
            _alertaEstado.Leyenda = "Tipo de mantenimiento registrado.";
            _alertaEstado.Estado= true;
            return _alertaEstado;
        }

        public async Task<AlertaEstado> Update(CatalogoViewModel viewModel)
        {
            var TipoMantenimiento = context.Find<TipoMantenimiento>(viewModel.ID);

            if (context.TipoMantenimiento.Any(m => m.Nombre == viewModel.CatalogoGralViewModel.Nombre && m.ID != viewModel.ID))
            {
                _alertaEstado.Leyenda = "¡Ya existe un tipo de mantenimiento con ese nombre!";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            TipoMantenimiento.Nombre = viewModel.CatalogoGralViewModel.Nombre;
            _alertaEstado.Leyenda = "¡Tipo de mantenimiento actualizado correctamente!";
            _alertaEstado.Estado = true;
            return _alertaEstado;

        }
    }
}
