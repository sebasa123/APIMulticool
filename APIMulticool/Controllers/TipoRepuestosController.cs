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
    [ApiKey]
    public class TipoRepuestosController : ControllerBase
    {
        private readonly MulticoolDBContext _context;

        public TipoRepuestosController(MulticoolDBContext context)
        {
            _context = context;
        }

        // GET: api/TipoRepuestos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoRepuesto>>> GetTipoRepuestos()
        {
          if (_context.TipoRepuestos == null)
          {
              return NotFound();
          }
            return await _context.TipoRepuestos.ToListAsync();
        }

        // GET: api/TipoRepuestos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoRepuesto>> GetTipoRepuesto(int id)
        {
          if (_context.TipoRepuestos == null)
          {
              return NotFound();
          }
            var tipoRepuesto = await _context.TipoRepuestos.FindAsync(id);

            if (tipoRepuesto == null)
            {
                return NotFound();
            }

            return tipoRepuesto;
        }

        // PUT: api/TipoRepuestos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoRepuesto(int id, TipoRepuesto tipoRepuesto)
        {
            if (id != tipoRepuesto.Idtr)
            {
                return BadRequest();
            }

            TipoRepuesto TipoRepNuevo = new()
            {
                Idtr = tipoRepuesto.Idtr,
                DescripcionTr = tipoRepuesto.DescripcionTr
            };

            _context.Entry(TipoRepNuevo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoRepuestoExists(id))
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

        // POST: api/TipoRepuestos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoRepuesto>> PostTipoRepuesto(TipoRepuesto tipoRepuesto)
        {
          if (_context.TipoRepuestos == null)
          {
              return Problem("Entity set 'MulticoolDBContext.TipoRepuestos'  is null.");
          }
            _context.TipoRepuestos.Add(tipoRepuesto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoRepuesto", new { id = tipoRepuesto.Idtr }, tipoRepuesto);
        }

        // DELETE: api/TipoRepuestos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoRepuesto(int id)
        {
            if (_context.TipoRepuestos == null)
            {
                return NotFound();
            }
            var tipoRepuesto = await _context.TipoRepuestos.FindAsync(id);
            if (tipoRepuesto == null)
            {
                return NotFound();
            }

            _context.TipoRepuestos.Remove(tipoRepuesto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoRepuestoExists(int id)
        {
            return (_context.TipoRepuestos?.Any(e => e.Idtr == id)).GetValueOrDefault();
        }
    }
}
