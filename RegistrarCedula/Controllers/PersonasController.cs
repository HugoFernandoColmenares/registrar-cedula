using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrarCedula.Data;
using RegistrarCedula.Shared;

namespace RegistrarCedula.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly RegistrarCedulaContext _context;

        public PersonasController(RegistrarCedulaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Persona>> Get()
        {
            return await _context.Persona.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(string id)
        {
            var persona = await _context.Persona.FirstOrDefaultAsync(p => p.Id == id);
            if (persona != null)
            {
                return Ok(persona);  // Devuelve la persona encontrada.
            }
            else
            {
                return NotFound();  // Devuelve un resultado not found si no se encuentra la persona.
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Persona persona)
        {
            var existePersona = await _context.Persona.FirstOrDefaultAsync(p => p.Id == persona.Id && p.Document == persona.Document);
            var maxRecords = await _context.Persona.CountAsync();

            if (existePersona == null)
            {
                persona.Id = Guid.NewGuid().ToString();
                persona.Code = maxRecords + 1;
                await _context.Persona.AddAsync(persona);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Post", persona.Id, persona);
            }
            else
            {
                // Devuelve un resultado que indique que la persona ya existe, por ejemplo, un código de estado 409 (Conflict).
                return StatusCode(409, "La persona ya existe en la base de datos.");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Persona persona)
        {
            // Carga la entidad desde la base de datos
            var existePersona = await _context.Persona.FindAsync(persona.Id);

            if (existePersona != null)
            {
                // Copia los valores de la entidad actual a la entidad cargada desde la base de datos
                _context.Entry(existePersona).CurrentValues.SetValues(persona);
                await _context.SaveChangesAsync();
                return NoContent();  // Devuelve un resultado exitoso sin contenido.
            }
            else
            {
                return NotFound();  // Devuelve un resultado not found si la persona no existe.
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var persona = await _context.Persona.FirstOrDefaultAsync(p => p.Id == id);
            if (persona != null)
            {
                _context.Persona.Remove(persona);  // Elimina la persona en lugar de actualizarla.
                await _context.SaveChangesAsync();
                return NoContent();  // Devuelve un resultado exitoso sin contenido.
            }
            else
            {
                return NotFound();  // Devuelve un resultado not found si la persona no existe.
            }
        }
    }
}
