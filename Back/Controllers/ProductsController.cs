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
using Back.Interfaces;
using Back.Enums;
using System.Globalization;

namespace Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppContext _context;
        private readonly IDataRepository _dataRepository;

        public ProductsController(AppContext context, IDataRepository dataRepository)
        {
            _context = context;
            _dataRepository = dataRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] CategoryEnum category, [FromQuery] ProductSort sort)
        {
            IQueryable<Product> query = _context.Products;
            if (category != CategoryEnum.Other)
            {
                query = query.Where(p => p.Category == category);
            }

            switch (sort)
            {
                case ProductSort.Name:
                    query = query.OrderBy(p => p.Name);
                    break;
                case ProductSort.CostAsc:
                    query = query.OrderBy(p => p.Cost);
                    break;
                case ProductSort.CostDesc:
                    query = query.OrderByDescending(p => p.Cost);
                    break;
                default:
                    return BadRequest("Invalid sort parameter.");
            }

            var products = await query.ToListAsync();
            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromForm] UpdateProductViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var dbProduct = _context.Products.FirstOrDefault(x => x.Id == id);
            dbProduct.Name = model.Name;
            dbProduct.Description = model.Description;
            dbProduct.Cost = model.Cost;
            dbProduct.Category = model.Category;

            if (model.Image != null)
            {
                dbProduct.Image = _dataRepository.FileToBytes(model.Image);
            }

            _context.Entry(dbProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromForm] CreateProductViewModel model)
        {
            if (model.Image == null)
            {
                return NoContent();
            }

            var product = model.ToModel();
            product.Image = _dataRepository.FileToBytes(model.Image);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
