using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.Entities;
using Condominios.Models.ViewModels.CtrolProveedores;
using Condominios.Models.ViewModels.CtrolVarianteEquipo;
using Microsoft.EntityFrameworkCore;

namespace Condominios.Data.Repositories.Catalogos
{
    public class ProveedorRepository : IProveedorRepository<Proveedor>
    {
        private readonly Context _context;
        public ProveedorRepository(Context context)
        {
            _context = context;
        }
            
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

            _context.Add(proveedor);
        }

        public async Task<List<Proveedor>> GetList()
        {
            var proveedor = await _context.Proveedor.ToListAsync();
            return proveedor;
        }

        public async Task<Proveedor?> GetById(int id)
        {
            var proveedor = await _context.Proveedor.FirstOrDefaultAsync(c => c.ID == id);
            return proveedor;
        }

        public void Update(ProveedoresViewModel model)
        {
            var proveedor = _context.Find<Proveedor>(model.ID);

            proveedor.Nombre = model.Nombre;
            proveedor.Telefono = model.Numero;
            proveedor.Direccion = model.Direccion;
            proveedor.Correo = model.Correo;
        }

        public async Task<Proveedor?> UpdateID(int id)
        {
            var proveedor = await _context.Proveedor.FirstOrDefaultAsync(c => c.ID == id);
            proveedor.Estado = !proveedor.Estado;
            return proveedor;
        }
    }
}
