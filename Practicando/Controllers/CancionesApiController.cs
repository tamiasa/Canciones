using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practicando.Data;
using Practicando.Models;

namespace Practicando.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancionesApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CancionesApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CancionesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Canciones>>> GetCanciones()
        {
            return await _context.Canciones.ToListAsync();
        }

        // GET: api/CancionesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Canciones>> GetCanciones(string id)
        {
            var canciones = await _context.Canciones.FindAsync(id);

            if (canciones == null)
            {
                return NotFound();
            }

            return canciones;
        }

        // PUT: api/CancionesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanciones(string id, Canciones canciones)
        {
            if (id != canciones.Nombre)
            {
                return BadRequest();
            }

            _context.Entry(canciones).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CancionesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CancionesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Canciones>> PostCanciones(Canciones canciones)
        {
            _context.Canciones.Add(canciones);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CancionesExists(canciones.Nombre))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCanciones", new { id = canciones.Nombre }, canciones);
        }

        // DELETE: api/CancionesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCanciones(string id)
        {
            var canciones = await _context.Canciones.FindAsync(id);
            if (canciones == null)
            {
                return NotFound();
            }

            _context.Canciones.Remove(canciones);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CancionesExists(string id)
        {
            return _context.Canciones.Any(e => e.Nombre == id);
        }
    }
}
