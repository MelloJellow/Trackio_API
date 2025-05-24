using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trackio_API.Controllers.Entities;
using Trackio_API.Data;
using Trackio_API.Services;

namespace Trackio_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private readonly DataContext _context;
        private readonly JwtService _jwt;

        public UserController(DataContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);

        }//EndController

        //get single user
        [HttpGet("{id}")]
        public async Task<ActionResult<List<User>>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return NotFound("user not found.");

            return Ok(user);

        }//EndController

        //Post Request - add user
        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser([FromBody] User user)
        {
            //Hash password before saving
            user.Password = HashPassword(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);

        }//EndController

        //Put Method
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> EditUser(int id, [FromBody]User updatedUser)
        {
        
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return NotFound("user not found.");

            //Update fields
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Location = updatedUser.Location;

            //Only hash pass if modified
            if (user.Password != updatedUser.Password)
                user.Password = HashPassword(updatedUser.Password);

            await _context.SaveChangesAsync();
            return Ok(user);
    }
        //Delete User
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id, [FromBody] User updatedUser)
        {

            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return NotFound("user not found.");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }



        //Login Endpoints
        [HttpPost("login")]
        [Produces("application/json")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            var hashedInput = HashPassword(request.Password);
            if (user.Password != hashedInput)
                return Unauthorized("Invalid email or password");

            var token = _jwt.GenerateToken(user);
            return Ok(token);
        }
    }
}
