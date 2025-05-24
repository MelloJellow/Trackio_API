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
    }
}
