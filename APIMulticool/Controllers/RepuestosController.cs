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
    public class RepuestosController : ControllerBase
    {
        private readonly MulticoolDBContext _context;

        public RepuestosController(MulticoolDBContext context)
        {
            _context = context;
        }

        // GET: api/Repuestos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Repuesto>>> GetRepuestos()
        {
          if (_context.Repuestos == null)
          {
              return NotFound();
          }
            return await _context.Repuestos.ToListAsync();
        }

        [HttpGet("GetRepuestoListByNombre")]
        public async Task<ActionResult<IEnumerable<Repuesto>>> GetRepuestoListByNombre(string pNombre)
        {
            var repList = await _context.Repuestos.Where(r => r.DescripcionRep == pNombre).ToListAsync();
            if (repList == null)
            {
                return NotFound();
            }
            return repList;
        }

        // GET: api/Repuestos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Repuesto>> GetRepuesto(int id)
        {
          if (_context.Repuestos == null)
          {
              return NotFound();
          }
            var repuesto = await _context.Repuestos.FindAsync(id);

            if (repuesto == null)
            {
                return NotFound();
            }

            return repuesto;
        }

        // PUT: api/Repuestos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepuesto(int id, Repuesto repuesto)
        {
            if (id != repuesto.Idrep)
            {
                return BadRequest();
            }

            Repuesto RepNuevo = new()
            {
                Idrep = repuesto.Idrep,
                CompletoRep = repuesto.CompletoRep,
                DescripcionRep = repuesto.DescripcionRep,
                FktipoRep = repuesto.FktipoRep,
                Fkherramientas = repuesto.Fkherramientas,
                Pedidos = null
            };

            _context.Entry(RepNuevo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepuestoExists(id))
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

        // POST: api/Repuestos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Repuesto>> PostRepuesto(Repuesto repuesto)
        {
          if (_context.Repuestos == null)
          {
              return Problem("Entity set 'MulticoolDBContext.Repuestos'  is null.");
          }
            _context.Repuestos.Add(repuesto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRepuesto", new { id = repuesto.Idrep }, repuesto);
        }

        // DELETE: api/Repuestos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepuesto(int id)
        {
            if (_context.Repuestos == null)
            {
                return NotFound();
            }
            var repuesto = await _context.Repuestos.FindAsync(id);
            if (repuesto == null)
            {
                return NotFound();
            }

            _context.Repuestos.Remove(repuesto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RepuestoExists(int id)
        {
            return (_context.Repuestos?.Any(e => e.Idrep == id)).GetValueOrDefault();
        }
    }
}
