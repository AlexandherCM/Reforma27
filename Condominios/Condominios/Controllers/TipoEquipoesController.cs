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
    public class TipoEquipoesController : Controller
    {
        private readonly Context _context;

        public TipoEquipoesController(Context context)
        {
            _context = context;
        }

        // GET: TipoEquipoes
        public async Task<IActionResult> Index()
        {
              return _context.TipoEquipo != null ? 
                          View(await _context.TipoEquipo.ToListAsync()) :
                          Problem("Entity set 'Context.TipoEquipo'  is null.");
        }

        // GET: TipoEquipoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoEquipo == null)
            {
                return NotFound();
            }

            var tipoEquipo = await _context.TipoEquipo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tipoEquipo == null)
            {
                return NotFound();
            }

            return View(tipoEquipo);
        }

        // GET: TipoEquipoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoEquipoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nombre,Estado")] TipoEquipo tipoEquipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoEquipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEquipo);
        }

        // GET: TipoEquipoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoEquipo == null)
            {
                return NotFound();
            }

            var tipoEquipo = await _context.TipoEquipo.FindAsync(id);
            if (tipoEquipo == null)
            {
                return NotFound();
            }
            return View(tipoEquipo);
        }

        // POST: TipoEquipoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Estado")] TipoEquipo tipoEquipo)
        {
            if (id != tipoEquipo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoEquipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoEquipoExists(tipoEquipo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEquipo);
        }

        // GET: TipoEquipoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoEquipo == null)
            {
                return NotFound();
            }

            var tipoEquipo = await _context.TipoEquipo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tipoEquipo == null)
            {
                return NotFound();
            }

            return View(tipoEquipo);
        }

        // POST: TipoEquipoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoEquipo == null)
            {
                return Problem("Entity set 'Context.TipoEquipo'  is null.");
            }
            var tipoEquipo = await _context.TipoEquipo.FindAsync(id);
            if (tipoEquipo != null)
            {
                _context.TipoEquipo.Remove(tipoEquipo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoEquipoExists(int id)
        {
          return (_context.TipoEquipo?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
