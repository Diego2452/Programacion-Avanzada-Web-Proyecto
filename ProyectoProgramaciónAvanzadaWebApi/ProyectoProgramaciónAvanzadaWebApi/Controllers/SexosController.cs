using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoProgramacionAvanzadaWebApi.Data;
using ProyectoProgramacionAvanzadaWebApi.Models;

namespace ProyectoProgramacionAvanzadaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SexosController : ControllerBase
    {
        private readonly LuxuryCarsContext _context;

        public SexosController(LuxuryCarsContext context)
        {
            _context = context;
        }

        // GET: api/Sexos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sexo>>> GetSexo()
        {
          if (_context.Sexo == null)
          {
              return NotFound();
          }
            return await _context.Sexo.ToListAsync();
        }

        // GET: api/Sexos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sexo>> GetSexo(int id)
        {
          if (_context.Sexo == null)
          {
              return NotFound();
          }
            var sexo = await _context.Sexo.FindAsync(id);

            if (sexo == null)
            {
                return NotFound();
            }

            return sexo;
        }

        // PUT: api/Sexos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSexo(int id, Sexo sexo)
        {
            if (id != sexo.IdSexo)
            {
                return BadRequest();
            }

            _context.Entry(sexo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SexoExists(id))
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

        // POST: api/Sexos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sexo>> PostSexo(Sexo sexo)
        {
          if (_context.Sexo == null)
          {
              return Problem("Entity set 'LuxuryCarsContext.Sexo'  is null.");
          }
            _context.Sexo.Add(sexo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSexo", new { id = sexo.IdSexo }, sexo);
        }

        // DELETE: api/Sexos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSexo(int id)
        {
            if (_context.Sexo == null)
            {
                return NotFound();
            }
            var sexo = await _context.Sexo.FindAsync(id);
            if (sexo == null)
            {
                return NotFound();
            }

            _context.Sexo.Remove(sexo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SexoExists(int id)
        {
            return (_context.Sexo?.Any(e => e.IdSexo == id)).GetValueOrDefault();
        }
    }
}
