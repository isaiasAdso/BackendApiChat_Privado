using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webSocketChat.Context;
using webSocketChat.Models;

namespace webSocketChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Contactos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contactos>>> GetContactos()
        {
            return await _context.Contactos.ToListAsync();
        }

        // GET: api/Contactos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contactos>> GetContactos(int id)
        {
            var contactos = await _context.Contactos.FindAsync(id);

            if (contactos == null)
            {
                return NotFound();
            }

            return contactos;
        }

        // PUT: api/Contactos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactos(int id, Contactos contactos)
        {
            if (id != contactos.IdContacto)
            {
                return BadRequest();
            }

            _context.Entry(contactos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactosExists(id))
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

        // POST: api/Contactos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contactos>> PostContactos(Contactos contactos)
        {
            _context.Contactos.Add(contactos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactos", new { id = contactos.IdContacto }, contactos);
        }

        // DELETE: api/Contactos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactos(int id)
        {
            var contactos = await _context.Contactos.FindAsync(id);
            if (contactos == null)
            {
                return NotFound();
            }

            _context.Contactos.Remove(contactos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactosExists(int id)
        {
            return _context.Contactos.Any(e => e.IdContacto == id);
        }
    }
}
