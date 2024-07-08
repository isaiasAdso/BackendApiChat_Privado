using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webSocketChat.Context;
using webSocketChat.Models;

namespace webSocketChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public UsuariosController(AppDbContext context, IConfiguration configuration, ILogger<UsuariosController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarios(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);

            if (usuarios == null)
            {
                return NotFound();
            }
            return usuarios;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(int id, Usuarios usuarios)
        {
            if (id != usuarios.IdUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
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
        public async Task<ActionResult<Usuarios>> PostUsuarios(Usuarios usuarios)
        {
            _context.Usuarios.Add(usuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarios", new { id = usuarios.IdUsuario }, usuarios);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarios(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        [HttpPost("Login")]
        public async Task<ActionResult<Usuarios>> Login(LoginRequest loginRequest)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Telefono == loginRequest.Telefono && u.PasswordHash == loginRequest.PasswordHash);

            if (usuario == null)
            {
                return Unauthorized();
            }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var Claims = new[]
            {

                new Claim("Id", usuario.IdUsuario.ToString()),
                new Claim("nombre", usuario.Nombre.ToString()),
                new Claim("Telefono", usuario.Telefono.ToString()),
                new Claim("Estado", usuario.Estado.ToString()),// Utilizamos ClaimTypes predefinidos para el documento del usuario
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

            var singin = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                Claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: singin
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


            return Ok(new
            {
                success = true,
                message = "exito",
                Token = tokenString
            });
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
