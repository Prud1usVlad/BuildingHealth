using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuildingHealth.Core.Models;
using BuildingHealth.DAL;
using Microsoft.AspNetCore.Authorization;

namespace BuildingHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BuildersController : ControllerBase
    {
        private readonly BuildingHealthDBContext _context;

        public BuildersController(BuildingHealthDBContext context)
        {
            _context = context;
        }

        // GET: api/Builders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Builder>>> GetBuilders()
        {
            return await _context.Builders
                .Include(b => b.IdNavigation)
                .ToListAsync();
        }

        // GET: api/Builders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Builder>> GetBuilder(int id)
        {
            try
            {
                var builder = await _context.Builders
                .Include(b => b.IdNavigation)
                .FirstAsync(b => b.Id == id);

                if (builder == null)
                {
                    return NotFound();
                }

                return builder;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/Builders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilder(int id, Builder builder)
        {
            if (id != builder.Id)
            {
                return BadRequest();
            }

            _context.Entry(builder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuilderExists(id))
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

        // POST: api/Builders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Builder>> PostBuilder(Builder builder)
        {
            _context.Builders.Add(builder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuilder", new { id = builder.Id }, builder);
        }

        // DELETE: api/Builders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilder(int id)
        {
            var builder = await _context.Builders.FindAsync(id);
            if (builder == null)
            {
                return NotFound();
            }

            _context.Builders.Remove(builder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuilderExists(int id)
        {
            return _context.Builders.Any(e => e.Id == id);
        }
    }
}
