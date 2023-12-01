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

            variante.variantes = await _context.Variante.Include(v => v.Funcion).Include(v => v.Marca)
                                                  .Include(v => v.Motor)
                                                  .Include(v => v.Periodo)
                                                  .Include(v => v.TipoEquipo).ToListAsync();

            ViewData["FuncionID"] = new SelectList(_context.Funcion, "ID", "Nombre");
            ViewData["MarcaID"] = new SelectList(_context.Marca, "ID", "Nombre");
            ViewData["MotorID"] = new SelectList(_context.Motor, "ID", "Nombre");
            ViewData["PeriodoID"] = new SelectList(_context.Periodo, "ID", "Nombre");
            ViewData["TipoEquipoID"] = new SelectList(_context.TipoEquipo, "ID", "Nombre");

            return View(variante);
        }

        // POST: Variantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VarianteViewModel variante)
        {
            int resultado;
            Variante varianteNew;

            // Intentar convertir la cadena a un entero
            if (int.TryParse(variante.LaFuncion, out resultado))
            {
                varianteNew = new Variante
                {
                    ID = variante.ID,
                    MarcaID = variante.MarcaID,
                    MotorID = variante.MotorID,
                    PeriodoID = variante.PeriodoID, // Corregir aquí
                    FuncionID = resultado, // Corregir aquí
                    TipoEquipoID = variante.TipoEquipoID,
                    Capacidad = variante.Capacidad,
                    Estado = true
                };
            }
            else
            {
                Funcion funcion = new Funcion
                {
                    Nombre = variante.LaFuncion.ToString(),
                    Estado = true
                };

                varianteNew = new Variante
                {
                    ID = variante.ID,
                    MarcaID = variante.MarcaID,
                    MotorID = variante.MotorID,
                    PeriodoID = variante.PeriodoID,
                    TipoEquipoID = variante.TipoEquipoID,
                    Capacidad = variante.Capacidad,
                    Funcion = funcion, 
                    Estado = true
                };
            }

            _context.Add(varianteNew);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
