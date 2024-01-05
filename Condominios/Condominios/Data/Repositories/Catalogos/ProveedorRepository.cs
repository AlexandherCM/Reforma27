using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.CtrolProveedores;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;
using Microsoft.EntityFrameworkCore;

namespace Condominios.Data.Repositories.Catalogos
{
    public class ProveedorRepository : Catalogo ,IProveedorRepository<Proveedor>
    {
        public ProveedorRepository(Context context) : base(context) { }

        public void Add(ProveedoresViewModel model)
        {
            Proveedor proveedor = new()
            {
                Nombre = model.Nombre,
                Telefono = model.Numero,
                Direccion = model.Direccion,
                Correo = model.Correo,
                Estado = true
            };

            context.Add(proveedor);
        }

        public async Task<List<Proveedor>> GetList()
            => await context.Proveedor.ToListAsync();
        
        public async Task<List<Proveedor>> GetActiveList()
            => await context.Proveedor.Where(c => c.Estado).ToListAsync();

        public async Task<Proveedor?> GetById(int id)
        {
            var proveedor = await context.Proveedor.FirstOrDefaultAsync(c => c.ID == id);
            return proveedor;
        }

        public void Update(ProveedoresViewModel model)
        {
            var proveedor = context.Find<Proveedor>(model.ID);

            proveedor.Nombre = model.Nombre;
            proveedor.Telefono = model.Numero;
            proveedor.Direccion = model.Direccion;
            proveedor.Correo = model.Correo;
        }

        public async Task<Proveedor?> UpdateID(int id)
        {
            var proveedor = await context.Proveedor.FirstOrDefaultAsync(c => c.ID == id);
            proveedor.Estado = !proveedor.Estado;
            return proveedor;
        }
    }
}
