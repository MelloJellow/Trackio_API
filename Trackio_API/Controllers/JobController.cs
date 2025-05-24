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
    }
}
