using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIMulticool.Models;
using APIMulticool.Attributes;

namespace APIMulticool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey]
    public class HerramientasController : ControllerBase
    {
        private readonly MulticoolDBContext _context;

        public HerramientasController(MulticoolDBContext context)
        {
            _context = context;
        }

        // GET: api/Herramientas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Herramientum>>> GetHerramienta()
        {
          if (_context.Herramienta == null)
          {
              return NotFound();
          }
            return await _context.Herramienta.ToListAsync();
        }

        [HttpGet("GetHerramientaListByNombre")]
        public async Task<ActionResult<IEnumerable<Herramientum>>> GetHerramientaListByNombre(string pNombre)
        {
            var herList = await _context.Herramienta.Where(h => h.NombreHer == pNombre).ToListAsync();
            if (herList == null)
            {
                return NotFound();
            }
            return herList;
        }

        // GET: api/Herramientas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Herramientum>> GetHerramientum(int id)
        {
          if (_context.Herramienta == null)
          {
              return NotFound();
          }
            var herramientum = await _context.Herramienta.FindAsync(id);

            if (herramientum == null)
            {
                return NotFound();
            }

            return herramientum;
        }

        // PUT: api/Herramientas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHerramientum(int id, Herramientum herramientum)
        {
            if (id != herramientum.Idher)
            {
                return BadRequest();
            }

            Herramientum HerNueva = new()
            {
                Idher = herramientum.Idher,
                NombreHer = herramientum.NombreHer,
                NumeroHer = herramientum.NumeroHer,
                EstadoHer = herramientum.EstadoHer,
                Repuestos = null
            };

            _context.Entry(HerNueva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HerramientumExists(id))
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

        // POST: api/Herramientas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Herramientum>> PostHerramientum(Herramientum herramientum)
        {
          if (_context.Herramienta == null)
          {
              return Problem("Entity set 'MulticoolDBContext.Herramienta'  is null.");
          }
            _context.Herramienta.Add(herramientum);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHerramientum", new { id = herramientum.Idher }, herramientum);
        }

        // DELETE: api/Herramientas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHerramientum(int id)
        {
            if (_context.Herramienta == null)
            {
                return NotFound();
            }
            var herramientum = await _context.Herramienta.FindAsync(id);
            if (herramientum == null)
            {
                return NotFound();
            }

            _context.Herramienta.Remove(herramientum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HerramientumExists(int id)
        {
            return (_context.Herramienta?.Any(e => e.Idher == id)).GetValueOrDefault();
        }
    }
}
