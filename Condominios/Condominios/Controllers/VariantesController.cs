using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Condominios.Models;
using Condominios.Models.Entities;

namespace Condominios.Controllers
{
    public class VariantesController : Controller
    {
        private readonly Context _context;

        public VariantesController(Context context)
        {
            _context = context;
        }

        // GET: Variantes
        public async Task<IActionResult> Index()
        {
            var context = _context.Variante.Include(v => v.Funcion).Include(v => v.Marca).Include(v => v.Motor).Include(v => v.Periodo).Include(v => v.TipoEquipo);
            return View(await context.ToListAsync());
        }

        // GET: Variantes/Create
        public IActionResult Create()
        {
            ViewData["FuncionID"] = new SelectList(_context.Funcion, "ID", "Nombre");
            ViewData["MarcaID"] = new SelectList(_context.Marca, "ID", "Nombre");
            ViewData["MotorID"] = new SelectList(_context.Motor, "ID", "Nombre");
            ViewData["PeriodoID"] = new SelectList(_context.Periodo, "ID", "Nombre");
            ViewData["TipoEquipoID"] = new SelectList(_context.TipoEquipo, "ID", "Nombre");
            return View();
        }

        // POST: Variantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MarcaID,MotorID,PeriodoID,FuncionID,TipoEquipoID,Capacidad,Estado")] Variante variante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(variante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuncionID"] = new SelectList(_context.Funcion, "ID", "Nombre", variante.FuncionID);
            ViewData["MarcaID"] = new SelectList(_context.Marca, "ID", "Nombre", variante.MarcaID);
            ViewData["MotorID"] = new SelectList(_context.Motor, "ID", "Nombre", variante.MotorID);
            ViewData["PeriodoID"] = new SelectList(_context.Periodo, "ID", "Nombre", variante.PeriodoID);
            ViewData["TipoEquipoID"] = new SelectList(_context.TipoEquipo, "ID", "Nombre", variante.TipoEquipoID);
            return View(variante);
        }

    }
}
