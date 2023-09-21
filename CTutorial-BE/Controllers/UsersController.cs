using CTutorial_BE.Context;
using CTutorial_BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CTutorial_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public UsersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Users users)
        {
            if (users == null)

                return BadRequest();
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x =>
            x.Email == users.Email && x.Password == users.Password);
            if (user == null)

                return NotFound(new { Message = "User not found!" });


            return Ok(new
            {
                Message = "Login successfully!"
            });

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Users users)
        {
            if (users == null)
                return BadRequest();
            await _appDbContext.Users.AddAsync(users);
            await _appDbContext.SaveChangesAsync();
            return Ok(new { Message = "Signed successfully" });
        }
    }
}
