using Condominios.Models.Entities;
using Condominios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Condominios.Models.ViewModels;
using System.ComponentModel;

namespace Condominios.Controllers
{
    public class TiposProveedoresController : Controller
    {

        private readonly Context _context;

        public TiposProveedoresController(Context context)
        {
            _context = context;
        }

        // GET: Variantes
        public async Task<IActionResult> Index()
        {
            VarianteViewModel variante = new();

            variante.variantes = await _context.Variante.Include(v => v.Marca)
                                                        .Include(v => v.Motor) 
                                                        .Include(v => v.Periodo) 
                                                        .Include(v => v.TipoEquipo).ToListAsync();

            ViewData["MarcaID"] = new SelectList(_context.Marca, "ID", "Nombre");
            ViewData["MotorID"] = new SelectList(_context.Motor, "ID", "Nombre");
            ViewData["PeriodoID"] = new SelectList(_context.Periodo, "ID", "Nombre");
            ViewData["TipoEquipoID"] = new SelectList(_context.TipoEquipo, "ID", "Nombre");

            return View(variante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VarianteViewModel variante)
        {

            return RedirectToAction(nameof(Index));
        }
    }
}
