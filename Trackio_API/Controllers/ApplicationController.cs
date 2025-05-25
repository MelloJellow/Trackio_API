using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackio_API.Controllers.Entities;
using Trackio_API.Data;

namespace Trackio_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly DataContext _context;

        public ApplicationController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        {
            return await _context.Applications
                .Include(a => a.User)
                .Include(a => a.Job)
                .ToListAsync();
        }

        //Get: api/application/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(int id)
        {
            var application = await _context.Applications
                .Include(a => a.User)
                .Include(a => a.Job)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
            {
                return NotFound();
            }

            return application;
        }

        //Post: api/application
        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication(Application app)
        {
            if (!Enum.IsDefined(typeof(ApplicationStatus), app.Status))
            {
                return BadRequest("Invalid status value");
            }

            _context.Applications.Add(app);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetApplication), new { id = app.Id }, app);
        }

        //Put: api/application/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApplication(int id, Application updateApp)
        {
            if (id != updateApp.Id)
            {
                return BadRequest("Application Id doesnt match");
            }

            if (!Enum.IsDefined(typeof(ApplicationStatus), updateApp.Status))
            {
                return BadRequest("Invalid status type");
            }

            _context.Entry(updateApp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Applications.Any(e => e.Id == id)) 
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        //Delete: api/application/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApp(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application!);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Frontend helper function 
        [HttpGet("statuses")]
        public ActionResult<IEnumerable<string>> GetStatuses()
        {
            var statuses = Enum.GetNames(typeof(ApplicationStatus));
            return Ok(statuses);
        }
    }
}
