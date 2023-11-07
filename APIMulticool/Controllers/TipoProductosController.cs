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
    public class TipoProductosController : ControllerBase
    {
        private readonly MulticoolDBContext _context;

        public TipoProductosController(MulticoolDBContext context)
        {
            _context = context;
        }

        // GET: api/TipoProductos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProducto>>> GetTipoProductos()
        {
          if (_context.TipoProductos == null)
          {
              return NotFound();
          }
            return await _context.TipoProductos.ToListAsync();
        }

        // GET: api/TipoProductos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoProducto>> GetTipoProducto(int id)
        {
          if (_context.TipoProductos == null)
          {
              return NotFound();
          }
            var tipoProducto = await _context.TipoProductos.FindAsync(id);

            if (tipoProducto == null)
            {
                return NotFound();
            }

            return tipoProducto;
        }

        // PUT: api/TipoProductos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoProducto(int id, TipoProducto tipoProducto)
        {
            if (id != tipoProducto.Idtp)
            {
                return BadRequest();
            }

            TipoProducto TipoProdNuevo = new()
            {
                Idtp = tipoProducto.Idtp,
                NombreTp = tipoProducto.NombreTp
            };

            _context.Entry(TipoProdNuevo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoProductoExists(id))
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

        // POST: api/TipoProductos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoProducto>> PostTipoProducto(TipoProducto tipoProducto)
        {
          if (_context.TipoProductos == null)
          {
              return Problem("Entity set 'MulticoolDBContext.TipoProductos'  is null.");
          }
            _context.TipoProductos.Add(tipoProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoProducto", new { id = tipoProducto.Idtp }, tipoProducto);
        }

        // DELETE: api/TipoProductos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoProducto(int id)
        {
            if (_context.TipoProductos == null)
            {
                return NotFound();
            }
            var tipoProducto = await _context.TipoProductos.FindAsync(id);
            if (tipoProducto == null)
            {
                return NotFound();
            }

            _context.TipoProductos.Remove(tipoProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoProductoExists(int id)
        {
            return (_context.TipoProductos?.Any(e => e.Idtp == id)).GetValueOrDefault();
        }
    }
}
