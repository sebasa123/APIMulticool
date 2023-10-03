using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIMulticool.Models;
using APIMulticool.Tools;
using APIMulticool.ModelsDTO;

namespace APIMulticool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly MulticoolDBContext _context;
        public Encrypt MyEncrypt { get; set; }

        public UsuariosController(MulticoolDBContext context)
        {
            _context = context;
            MyEncrypt = new Encrypt();
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
          if (_context.Usuarios == null)
          {
              return NotFound();
          }
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpGet("ValidateUserLogin")]
        public async Task<ActionResult<Usuario>> ValidateUserLogin(string pNombre, string pContra)
        {
            string ContraEncrypt = MyEncrypt.EncriptarEnUnSentido(pContra);
            var user = await _context.Usuarios.SingleOrDefaultAsync(e =>
                e.NombreUs == pNombre && e.ContraUs == ContraEncrypt);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // GET: api/Users/GetUserData
        [HttpGet("GetUserData")]
        public ActionResult<IEnumerable<UsuarioDTO>> GetUserData(string nombre)
        {
            var query = (from u in _context.Usuarios
                         where u.NombreUs == nombre
                         select new
                         {
                             idusuario = u.Idus,
                             nombreusuario = u.NombreUs,
                             contrausuario = u.ContraUs,
                             tipousuario = u.FktipoUsuario,
                             estadousuario = u.EstadoUs
                         }).ToList();
            List<UsuarioDTO> list = new List<UsuarioDTO>();
            foreach (var item in query)
            {
                UsuarioDTO NewItem = new UsuarioDTO()
                {
                    Idus = item.idusuario,
                    NombreUs = item.nombreusuario,
                    ContraUs = item.contrausuario,
                    FktipoUsuario = item.tipousuario,
                    EstadoUs = item.estadousuario
                };
                list.Add(NewItem);
            }
            if (list == null)
            {
                return NotFound();
            }
            return list;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Idus)
            {
                return BadRequest();
            }

            string contra = "";
            if (usuario.ContraUs.Length <= 60)
            {
                contra = MyEncrypt.EncriptarEnUnSentido(usuario.ContraUs);
            }
            else
            {
                contra = usuario.ContraUs;
            }

            Usuario UsuarioNuevo = new()
            {
                Idus = usuario.Idus,
                NombreUs = usuario.NombreUs,
                ContraUs = usuario.ContraUs,
                FktipoUsuario = usuario.FktipoUsuario,
                EstadoUs = usuario.EstadoUs
            };

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
          if (_context.Usuarios == null)
          {
              return Problem("Entity set 'MulticoolDBContext.Usuarios'  is null.");
          }
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Idus }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.Idus == id)).GetValueOrDefault();
        }
    }
}
