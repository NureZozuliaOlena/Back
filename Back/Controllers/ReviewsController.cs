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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly AppContext _context;

        public ReviewsController(AppContext context)
        {
            _context = context;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> WriteReview([FromBody] CreateReviewViewModel model)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail == null)
            {
                return Unauthorized("User email not found.");
            }

            var user = await _context.UserBases.FirstOrDefaultAsync(x => x.Email == userEmail);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var review = new Review()
            {
                Grade = model.Grade,
                Text = model.Text,
                UserId = user.Id,
                ProductId = model.ProductId
            };

            await _context.AddAsync(review);
            await _context.SaveChangesAsync();
            return Ok(review);
        }
    }
}
