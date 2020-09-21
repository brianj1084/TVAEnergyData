using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TVAEnergyData.Data;
using TVAEnergyData.Domain;

namespace TVAEnergyData.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EIAPointsController : ControllerBase
    {
        private readonly TVAEnergyDataContext _context;

        public EIAPointsController(TVAEnergyDataContext context)
        {
            _context = context;
        }

        // GET: api/EIAPoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EIAPoint>>> GetEIAPoints()
        {
            return await _context.EIAPoints.ToListAsync();
        }

        // GET: api/EIAPoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EIAPoint>> GetEIAPoint(int id)
        {
            var eiaPoint = await _context.EIAPoints.FindAsync(id);

            if (eiaPoint == null)
            {
                return NotFound();
            }

            return eiaPoint;
        }

        // PUT: api/EIAPoints/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEIAPoint(int id, EIAPoint eiaPoint)
        {
            if (id != eiaPoint.Id)
            {
                return BadRequest();
            }

            _context.Entry(eiaPoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EIAPointExists(id))
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

        // POST: api/EIAPoints
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EIAPoint>> PostEIAPoint(EIAPoint eiaPoint)
        {
            _context.EIAPoints.Add(eiaPoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEIAPoint", new { id = eiaPoint.Id }, eiaPoint);
        }

        // DELETE: api/EIAPoints/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EIAPoint>> DeleteEIAPoint(int id)
        {
            var eiaPoint = await _context.EIAPoints.FindAsync(id);
            if (eiaPoint == null)
            {
                return NotFound();
            }

            _context.EIAPoints.Remove(eiaPoint);
            await _context.SaveChangesAsync();

            return eiaPoint;
        }

        private bool EIAPointExists(int id)
        {
            return _context.EIAPoints.Any(e => e.Id == id);
        }
    }
}
