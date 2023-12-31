﻿using System;
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
    public class ClientesController : ControllerBase
    {
        private readonly MulticoolDBContext _context;

        public ClientesController(MulticoolDBContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
          if (_context.Clientes == null)
          {
              return NotFound();
          }
            return await _context.Clientes.ToListAsync();
        }

        [HttpGet("GetClienteListByNombre")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClienteListByNombre(string pNombre)
        {
            var cliList = await _context.Clientes.Where(c => c.NombreCli == pNombre).ToListAsync();
            if (cliList == null)
            {
                return NotFound();
            }
            return cliList;
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
          if (_context.Clientes == null)
          {
              return NotFound();
          }
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutCliente/{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Idcli)
            {
                return BadRequest();
            }

            Cliente CliNuevo = new()
            {
                Idcli = cliente.Idcli,
                NombreCli = cliente.NombreCli,
                ApellidoCli = cliente.ApellidoCli,
                CedulaCli = cliente.CedulaCli,
                DireccionCli = cliente.DireccionCli,
                EstadoCli = cliente.EstadoCli,
                Pedidos = null
            };

            _context.Entry(CliNuevo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostCliente")]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
          if (_context.Clientes == null)
          {
              return Problem("Entity set 'MulticoolDBContext.Clientes'  is null.");
          }
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.Idcli }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("DeleteCliente/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return (_context.Clientes?.Any(e => e.Idcli == id)).GetValueOrDefault();
        }
    }
}
