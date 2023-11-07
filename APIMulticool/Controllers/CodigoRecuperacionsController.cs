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
    public class CodigoRecuperacionsController : ControllerBase
    {
        private readonly MulticoolDBContext _context;

        public CodigoRecuperacionsController(MulticoolDBContext context)
        {
            _context = context;
        }

        // GET: api/CodigoRecuperacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodigoRecuperacion>>> GetCodigoRecuperacions()
        {
          if (_context.CodigoRecuperacions == null)
          {
              return NotFound();
          }
            return await _context.CodigoRecuperacions.ToListAsync();
        }

        // GET: api/CodigoRecuperacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CodigoRecuperacion>> GetCodigoRecuperacion(int id)
        {
          if (_context.CodigoRecuperacions == null)
          {
              return NotFound();
          }
            var codigoRecuperacion = await _context.CodigoRecuperacions.FindAsync(id);

            if (codigoRecuperacion == null)
            {
                return NotFound();
            }

            return codigoRecuperacion;
        }

        // GET: api/RecoveryCodes/ValidateCode
        [HttpGet("ValidateCode")]
        public async Task<ActionResult<CodigoRecuperacion>> ValidateCode(string pEmail, string pCodigo)
        {
            if (_context.CodigoRecuperacions == null)
            {
                return NotFound();
            }
            var recoveryCode = await _context.CodigoRecuperacions.
                SingleOrDefaultAsync(e => e.Email == pEmail
                && e.CodigoRec == pCodigo &&
                e.EstadoCr == false);

            if (recoveryCode == null)
            {
                return NotFound();
            }

            return recoveryCode;
        }

        // PUT: api/CodigoRecuperacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCodigoRecuperacion(int id, CodigoRecuperacion codigoRecuperacion)
        {
            if (id != codigoRecuperacion.Idcr)
            {
                return BadRequest();
            }

            _context.Entry(codigoRecuperacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CodigoRecuperacionExists(id))
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

        // POST: api/CodigoRecuperacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CodigoRecuperacion>> PostCodigoRecuperacion(CodigoRecuperacion codigoRecuperacion)
        {
          if (_context.CodigoRecuperacions == null)
          {
              return Problem("Entity set 'MulticoolDBContext.CodigoRecuperacions'  is null.");
          }
            _context.CodigoRecuperacions.Add(codigoRecuperacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCodigoRecuperacion", new { id = codigoRecuperacion.Idcr }, codigoRecuperacion);
        }

        // DELETE: api/CodigoRecuperacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCodigoRecuperacion(int id)
        {
            if (_context.CodigoRecuperacions == null)
            {
                return NotFound();
            }
            var codigoRecuperacion = await _context.CodigoRecuperacions.FindAsync(id);
            if (codigoRecuperacion == null)
            {
                return NotFound();
            }

            _context.CodigoRecuperacions.Remove(codigoRecuperacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CodigoRecuperacionExists(int id)
        {
            return (_context.CodigoRecuperacions?.Any(e => e.Idcr == id)).GetValueOrDefault();
        }
    }
}
