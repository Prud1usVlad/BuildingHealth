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
    public class SensorsResponsesController : ControllerBase
    {
        private readonly BuildingHealthDBContext _context;

        public SensorsResponsesController(BuildingHealthDBContext context)
        {
            _context = context;
        }

        // GET: api/SensorsResponses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorsResponse>>> GetSensorsResponses()
        {
            return await _context.SensorsResponses
                .Include(r => r.MainCostructionStates)
                .ToListAsync();
        }

        // GET: api/SensorsResponses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorsResponse>> GetSensorsResponse(int id)
        {
            var sensorsResponse = await _context.SensorsResponses
                .Include(r => r.MainCostructionStates)
                .FirstAsync(r => r.Id == id);

            if (sensorsResponse == null)
            {
                return NotFound();
            }

            return sensorsResponse;
        }

        // PUT: api/SensorsResponses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensorsResponse(int id, SensorsResponse sensorsResponse)
        {
            if (id != sensorsResponse.Id)
            {
                return BadRequest();
            }

            _context.Entry(sensorsResponse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorsResponseExists(id))
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

        // POST: api/SensorsResponses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SensorsResponse>> PostSensorsResponse(SensorsResponse sensorsResponse)
        {
            sensorsResponse.Date = DateTime.Now;
            _context.SensorsResponses.Add(sensorsResponse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSensorsResponse", new { id = sensorsResponse.Id }, sensorsResponse);
        }

        // DELETE: api/SensorsResponses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensorsResponse(int id)
        {
            var sensorsResponse = await _context.SensorsResponses.FindAsync(id);
            if (sensorsResponse == null)
            {
                return NotFound();
            }

            _context.SensorsResponses.Remove(sensorsResponse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorsResponseExists(int id)
        {
            return _context.SensorsResponses.Any(e => e.Id == id);
        }
    }
}
