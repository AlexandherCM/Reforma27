using Condominios.Data.Interfaces;
using Condominios.Data.Interfaces.IRepositories;
using Condominios.Models;
using Condominios.Models.DTOs;
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
                Empresa = model.Empresa,
                Servicio = model.Servicio,
                Contacto = model.Contacto,
                Telefono = model.Numero,
                Direccion = model.Direccion,
                Correo = model.Correo,
                Estado = true
            };

            context.Add(proveedor);
        }

        public async Task<List<Proveedor>> GetList()
            => await context.Proveedor.ToListAsync();
        
        public async Task<List<ProveedoresDTO>> GetFormatList()
            => Clone(await GetList());

        public async Task<List<ProveedoresDTO>> GetFormatActiveList() 
        {
            var list = await GetList();
            return Clone(list.Where(c => c.Estado).ToList());
        }

        private static List<ProveedoresDTO> Clone(List<Proveedor> proveedores)
        {
            List<ProveedoresDTO> listaDTO = (from p in proveedores
                                            select new ProveedoresDTO
                                            {
                                                ID = p.ID,
                                                Nombre = $"Empresa: {p.Empresa} / Servicio: {p.Servicio} / Contacto: {p.Contacto}"
                                            }).ToList();

            return listaDTO;
        }

        public async Task<Proveedor?> GetById(int id)
        {
            var proveedor = await context.Proveedor.FirstOrDefaultAsync(c => c.ID == id);
            return proveedor;
        }

        public void Update(ProveedoresViewModel model)
        {
            Proveedor proveedor = context.Find<Proveedor>(model.ID)?? new();

            Proveedor newProveedor = new()
            {
                ID = proveedor.ID,
                Empresa = model.Empresa,
                Servicio = model.Servicio,
                Contacto = model.Contacto,
                Telefono = model.Numero,
                Direccion = model.Direccion,
                Correo = model.Correo,
                Estado = proveedor.Estado
            };

            context.Entry(proveedor).CurrentValues.SetValues(newProveedor);
        }

        public async Task<Proveedor?> UpdateID(int id)
        {
            var proveedor = await context.Proveedor.FirstOrDefaultAsync(c => c.ID == id);
            proveedor.Estado = !proveedor.Estado;
            return proveedor;
        }
    }
}
