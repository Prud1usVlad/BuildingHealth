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
    public class BuildingProjectsController : ControllerBase
    {
        private readonly BuildingHealthDBContext _context;

        public BuildingProjectsController(BuildingHealthDBContext context)
        {
            _context = context;
        }

        // GET: api/BuildingProjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuildingProject>>> GetBuildingProjects()
        {
            return await _context.BuildingProjects
                .Include(p => p.Architect)
                .ToListAsync();
        }

        // GET: api/BuildingProjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingProject>> GetBuildingProject(int id)
        {
            var buildingProject = await _context.BuildingProjects
                .Include(p => p.Architect)
                .FirstAsync(p => p.Id == id);

            if (buildingProject == null)
            {
                return NotFound();
            }

            return buildingProject;
        }

        // PUT: api/BuildingProjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuildingProject(int id, BuildingProject buildingProject)
        {
            if (id != buildingProject.Id)
            {
                return BadRequest();
            }

            _context.Entry(buildingProject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingProjectExists(id))
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

        // POST: api/BuildingProjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BuildingProject>> PostBuildingProject(BuildingProject buildingProject)
        {
            _context.BuildingProjects.Add(buildingProject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuildingProject", new { id = buildingProject.Id }, buildingProject);
        }

        // DELETE: api/BuildingProjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuildingProject(int id)
        {
            var buildingProject = await _context.BuildingProjects.FindAsync(id);
            if (buildingProject == null)
            {
                return NotFound();
            }

            _context.BuildingProjects.Remove(buildingProject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingProjectExists(int id)
        {
            return _context.BuildingProjects.Any(e => e.Id == id);
        }
    }
}
