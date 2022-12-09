using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildingHealth.Core.Models;
using BuildingHealth.DAL;

namespace BuildingHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchitectsController : ControllerBase
    {
        private readonly BuildingHealthDBContext _context;

        public ArchitectsController(BuildingHealthDBContext context)
        {
            _context = context;
        }

        // GET: api/Architects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Architect>>> GetArchitects()
        {
            return await _context.Architects
                .Include(a => a.IdNavigation)
                .ToListAsync();
        }

        // GET: api/Architects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Architect>> GetArchitect(int id)
        {
            var architect = await _context.Architects
                .Include(a => a.IdNavigation)
                .FirstAsync(a => a.Id == id);

            if (architect == null)
            {
                return NotFound();
            }

            return architect;
        }

        // PUT: api/Architects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArchitect(int id, Architect architect)
        {
            if (id != architect.Id)
            {
                return BadRequest();
            }

            _context.Entry(architect).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArchitectExists(id))
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

        // POST: api/Architects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Architect>> PostArchitect(Architect architect)
        {
            _context.Architects.Add(architect);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArchitect", new { id = architect.Id }, architect);
        }

        // DELETE: api/Architects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArchitect(int id)
        {
            var architect = await _context.Architects.FindAsync(id);
            if (architect == null)
            {
                return NotFound();
            }

            _context.Architects.Remove(architect);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArchitectExists(int id)
        {
            return _context.Architects.Any(e => e.Id == id);
        }
    }
}
