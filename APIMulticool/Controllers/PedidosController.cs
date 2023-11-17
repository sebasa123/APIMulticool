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
    public class PedidosController : ControllerBase
    {
        private readonly MulticoolDBContext _context;

        public PedidosController(MulticoolDBContext context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
          if (_context.Pedidos == null)
          {
              return NotFound();
          }
            return await _context.Pedidos.ToListAsync();
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
          if (_context.Pedidos == null)
          {
              return NotFound();
          }
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        [HttpGet("GetPedidoListByCliente")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidoListByCliente(int pCliente)
        {
            var pedidoList = await _context.Pedidos.Where(p => p.Fkcli == pCliente).ToListAsync();
            if (pedidoList == null)
            {
                return NotFound();
            }
            return pedidoList;
        }


        // PUT: api/Pedidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.Idped)
            {
                return BadRequest();
            }

            Pedido PedNuevo = new()
            {
                Idped = pedido.Idped,
                DecripcionPed = pedido.DecripcionPed,
                FechaPed = pedido.FechaPed,
                EstadoPed = pedido.EstadoPed,
                Fkcli = pedido.Fkcli,
                Fkprod = pedido.Fkprod,
                Fkrep = pedido.Fkrep,
                Fkus = pedido.Fkus,
                FkcliNavigation = null,
                FkprodNavigation = null,
                FkrepNavigation = null,
                FkusNavigation = null
            };

            _context.Entry(PedNuevo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // POST: api/Pedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
          if (_context.Pedidos == null)
          {
              return Problem("Entity set 'MulticoolDBContext.Pedidos'  is null.");
          }
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPedido", new { id = pedido.Idped }, pedido);
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return (_context.Pedidos?.Any(e => e.Idped == id)).GetValueOrDefault();
        }
    }
}
