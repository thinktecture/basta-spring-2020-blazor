using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorConfTool.Server.Model;

namespace BlazorConfTool.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly ConferencesDbContext _context;

        public DemoController(ConferencesDbContext context)
        {
            _context = context;
        }

        // GET: api/Demo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conference>>> GetConferences()
        {
            return await _context.Conferences.ToListAsync();
        }

        // GET: api/Demo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conference>> GetConference(Guid id)
        {
            var conference = await _context.Conferences.FindAsync(id);

            if (conference == null)
            {
                return NotFound();
            }

            return conference;
        }

        // PUT: api/Demo/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConference(Guid id, Conference conference)
        {
            if (id != conference.ID)
            {
                return BadRequest();
            }

            _context.Entry(conference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConferenceExists(id))
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

        // POST: api/Demo
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Conference>> PostConference(Conference conference)
        {
            _context.Conferences.Add(conference);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConference", new { id = conference.ID }, conference);
        }

        // DELETE: api/Demo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Conference>> DeleteConference(Guid id)
        {
            var conference = await _context.Conferences.FindAsync(id);
            if (conference == null)
            {
                return NotFound();
            }

            _context.Conferences.Remove(conference);
            await _context.SaveChangesAsync();

            return conference;
        }

        private bool ConferenceExists(Guid id)
        {
            return _context.Conferences.Any(e => e.ID == id);
        }
    }
}
