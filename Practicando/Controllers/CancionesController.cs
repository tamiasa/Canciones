using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practicando.Data;
using Practicando.Models;

namespace Practicando.Controllers
{
    public class CancionesController : Controller
    {
        private readonly AppDbContext _context;

        public CancionesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Canciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Canciones.ToListAsync());
        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await _context.Canciones
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (canciones == null)
            {
                return NotFound();
            }

            return View(canciones);
        }

        // GET: Canciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Canciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Autor,Letra,Enlace")] Canciones canciones)
        {
            if (ModelState.IsValid)
            {
                _context.Add(canciones);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(canciones);
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await _context.Canciones.FindAsync(id);
            if (canciones == null)
            {
                return NotFound();
            }
            return View(canciones);
        }

        // POST: Canciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nombre,Autor,Letra,Enlace")] Canciones canciones)
        {
            if (id != canciones.Nombre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(canciones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CancionesExists(canciones.Nombre))
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
            return View(canciones);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canciones = await _context.Canciones
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (canciones == null)
            {
                return NotFound();
            }

            return View(canciones);
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var canciones = await _context.Canciones.FindAsync(id);
            _context.Canciones.Remove(canciones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CancionesExists(string id)
        {
            return _context.Canciones.Any(e => e.Nombre == id);
        }
    }
}
