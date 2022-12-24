using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{ 
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var result = await _context.Users.ToListAsync();

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var result = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result == null)
                return NotFound();

            return result;
        }
    }
}