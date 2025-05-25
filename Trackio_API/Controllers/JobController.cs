using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackio_API.Controllers.Entities;
using Trackio_API.Data;

namespace Trackio_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : Controller
    {
        private readonly DataContext _context;

        public JobController(DataContext context)
        {
            _context = context;
        }

        //Get: api/job
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jobs>>> GetJobs()
        {
            return await _context.Jobs.ToListAsync();
        }

        //Post: api/job
        [HttpPost]
        public async Task<ActionResult<Jobs>> PostJob(Jobs job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJobs), new { id = job.Id }, job);
        }

        //Put: api/job/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, Jobs job)
        {
            if (id != job.Id)
            {
                return BadRequest("Job ID isn't valid");
            }

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Jobs.Any(j => j.Id == id)) 
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        //Delete: api/job/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) 
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();


        }

        //Get single job: api/job/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Jobs>> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }
    }
}
