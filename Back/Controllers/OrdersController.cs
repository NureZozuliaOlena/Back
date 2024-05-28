using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Back;
using Back.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Back.ViewModels;

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppContext _context;

        public OrdersController(AppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(c => c.Cart)
                .ThenInclude(c => c.CartProducts)
                .ThenInclude(c => c.Product).FirstOrDefaultAsync(c => c.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<Order>>> GetUserOrders()
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

            var userOrders = await _context.Orders
                .Include(o => o.Cart)
                .ThenInclude(c => c.User)
                .Where(o => o.Cart.UserId == user.Id)
                .ToListAsync();
            return Ok(userOrders);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> MakeOrder([FromBody] CreateOrderViewModel model)
        {
            var order = new Order()
            {
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Area = model.Area,
                HouseNumber = model.HouseNumber,
                Street = model.Street,
                Status = model.Status,
                CartId = model.CartId
            };

            await _context.AddAsync(order);
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == model.CartId);
            cart.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok(order);
        }

        [HttpPatch]
        public async Task<ActionResult> ChangeOrderStatus([FromBody] ChangeOrderStatusViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data.");
            }

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == model.OrderId);
            order.UpdatedDate = DateTime.Now;
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            order.Status = model.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Orders.Any(e => e.Id == model.OrderId))
                {
                    return NotFound("Order not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
