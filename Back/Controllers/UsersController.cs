using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Back;
using Back.Models;
using Back.ViewModels;
using System.IO;
using Back.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppContext _context;
        private readonly IDataRepository _dataRepository;
        private readonly IUserRepository _userRepository;

        public UsersController(AppContext context, IDataRepository dataRepository, IUserRepository userRepository)
        {
            _context = context;
            _dataRepository = dataRepository;
            _userRepository = userRepository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromForm] ChangeUserViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("User ID mismatch.");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var emailExists = await _context.UserBases.AnyAsync(u => u.Email == model.Email && u.Id != id);
            if (emailExists)
            {
                return Conflict("The email is already in use by another user.");
            }

            user.Name = model.Name;
            user.Phone = model.Phone;
            user.Email = model.Email;

            if (model.Image != null && model.Image.Length > 0)
            {
               user.Image = _dataRepository.FileToBytes(model.Image);
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
