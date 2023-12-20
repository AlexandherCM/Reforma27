using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.Services.Classes;
using Condominios.Models.ViewModels.Catalogos;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8602

namespace Condominios.Data.Repositories.Catalogos
{
    public class PeriodoRepository : Catalogo, ICatalogoRepository<Periodo>
    {
        private AlertaEstado _alertaEstado = new();
        public PeriodoRepository(Context context) : base(context) { }

        public async Task<List<Periodo>> GetList()
            => await context.Periodo.ToListAsync();

        public void Add(CatalogoViewModel viewModel)
        {
            Periodo periodo = new()
            {
                Nombre = viewModel.PeriodoViewModel.Nombre,
                Meses = viewModel.PeriodoViewModel.Meses(),
                Estado = true
            };

            context.Periodo.Add(periodo);
        }

        public async Task<AlertaEstado> add(CatalogoViewModel viewModel)
        {
            if (context.Periodo.Any(p => p.Nombre == viewModel.PeriodoViewModel.Nombre))
            {
                _alertaEstado.Leyenda = "Ya existe un periodo con ese nombre";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }else if (context.Periodo.Any(p => p.Meses == viewModel.PeriodoViewModel.Meses()))
            {
                _alertaEstado.Leyenda = "Ya existe un periodo con ese numero de meses";
                _alertaEstado.Estado = false;
                return _alertaEstado;
            }

            Periodo periodo = new()
            {
                Nombre = viewModel.PeriodoViewModel.Nombre,
                Meses = viewModel.PeriodoViewModel.Meses(),
                Estado = true
            };

            context.Periodo.Add(periodo);
            _alertaEstado.Leyenda = "Periodo registrado";
            _alertaEstado.Estado = true;
            return _alertaEstado;
        }

        public void Delete(Periodo entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateEstateById(int id)
        {
            var periodo = context.Find<Periodo>(id);
            periodo.Estado = !periodo.Estado;
        }

        public Task<Periodo?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CatalogoViewModel viewModel)
        {
            var periodo = context.Find<Periodo>(viewModel.ID);
            periodo.Nombre = viewModel.PeriodoViewModel.Nombre;
            periodo.Meses = viewModel.PeriodoViewModel.Meses();
        }
    }
}
