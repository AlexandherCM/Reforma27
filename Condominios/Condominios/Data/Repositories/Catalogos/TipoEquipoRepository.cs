using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.Catalogos
{
    public class TipoEquipoRepository : Catalogo, ICatalogoRepository<TipoEquipo>
    {
        private AlertaEstado _alertaEstado = new();
        public TipoEquipoRepository(Context context) : base(context) { }

        public async Task<AlertaEstado> add(CatalogoViewModel viewModel)
        {
            if (context.TipoEquipo.Any(te => te.Nombre == viewModel.CatalogoGralViewModel.Nombre))
            {
                _alertaEstado.Leyenda = "Ya existe un equipo con ese nombre";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            TipoEquipo tipoEquipo = new()
            {
                Nombre = viewModel.CatalogoGralViewModel.Nombre,
                Estado = true
            };

            context.TipoEquipo.Add(tipoEquipo);
            _alertaEstado.Leyenda = "Equipo registrado";
            _alertaEstado.Estado = true;
            return _alertaEstado;

        }

        public void Delete(TipoEquipo entity)
        {
            throw new NotImplementedException();
        }

        public Task<TipoEquipo?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TipoEquipo>> GetList()
            => await context.TipoEquipo.ToListAsync();

        public void UpdateEstateById(int id)
        {
            var tipoEquipo = context.Find<TipoEquipo>(id);
            tipoEquipo.Estado = !tipoEquipo.Estado;
        }

        public async Task<AlertaEstado> Update(CatalogoViewModel viewModel)
        {
            var tipoEquipo = context.Find<TipoEquipo>(viewModel.ID);

            if (context.TipoEquipo.Any(m => m.Nombre == viewModel.CatalogoGralViewModel.Nombre && m.ID != viewModel.ID))
            {
                _alertaEstado.Leyenda = "Ya existe un equipo con ese nombre";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            tipoEquipo.Nombre = viewModel.CatalogoGralViewModel.Nombre;
            _alertaEstado.Leyenda = "Equipo actualizado correctamente";
            _alertaEstado.Estado = true;
            return _alertaEstado;

        }
    }
}
