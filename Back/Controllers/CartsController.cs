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
    public class CartsController : ControllerBase
    {
        private readonly AppContext _context;

        public CartsController(AppContext context)
        {
            _context = context;
        }

        // GET: api/Carts/5
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Cart>> GetCart()
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

            var cart = await _context.Carts
                                     .Include(c => c.CartProducts)
                                     .FirstOrDefaultAsync(c => c.UserId == user.Id && c.IsActive);
            if (cart == null)
            {
                var newCart = new Cart
                {
                    UserId = user.Id,
                    IsActive = true,
                };

                _context.Carts.Add(newCart);
                await _context.SaveChangesAsync();
                cart = await _context.Carts
                                     .Include(c => c.CartProducts)
                                     .FirstOrDefaultAsync(c => c.Id == newCart.Id);
            }

            return Ok(cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddToCart([FromBody] AddToCartRequestViewModel request)
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

            var cart = await _context.Carts
                                     .Include(c => c.CartProducts)
                                     .FirstOrDefaultAsync(c => c.UserId == user.Id && c.IsActive);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = user.Id,
                    IsActive = true,
                    CartProducts = new List<CartProduct>()
                };

                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var existingCartProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == request.ProductId);
            if (existingCartProduct != null)
            {
                existingCartProduct.Quantity += request.Quantity;
            }
            else
            {
                var newCartProduct = new CartProduct
                {
                    CartId = cart.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };

                cart.CartProducts.Add(newCartProduct);
                _context.CartProducts.Add(newCartProduct);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult> ChangeQuantity([FromBody] AddToCartRequestViewModel model)
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

            var cart = await _context.Carts
                                     .Include(c => c.CartProducts)
                                     .FirstOrDefaultAsync(c => c.UserId == user.Id && c.IsActive);
            var existingCartProduct = cart.CartProducts.FirstOrDefault(cp => cp.ProductId == model.ProductId);
            if (model.Quantity == 0)
            {
                _context.Remove(existingCartProduct);
            } else
            {
                existingCartProduct.Quantity = model.Quantity;
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> ClearCart()
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

            var cart = await _context.Carts
                                     .Include(c => c.CartProducts)
                                     .FirstOrDefaultAsync(c => c.UserId == user.Id && c.IsActive);
            if (cart == null)
            {
                return NotFound("Active cart not found.");
            }

            _context.CartProducts.RemoveRange(cart.CartProducts);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
